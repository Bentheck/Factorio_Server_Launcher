using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Text;

namespace Facto
{
    public partial class Launcher : Form
    {
        // Configuration file path
        private string configFilePath = @"config.txt";

        // Paths and settings
        private string factorioPath = "";
        private string saveFile = "";
        private string serverSettingsFilePath = "server-settings.json";
        private ServerSettings serverSettings;

        // Configuration entries
        private Dictionary<string, string> configEntries = new Dictionary<string, string>();

        // URLs for version checking and updates
        private static readonly string latestReleasesUrl = "https://factorio.com/api/latest-releases";
        private static readonly string updateApiDomain = "https://updater.factorio.com";

        // Factorio process
        private Process factorioProcess;

        // Timers
        private System.Windows.Forms.Timer versionCheckTimer;
        private System.Windows.Forms.Timer restartTimer;

        // Restart and version checking variables
        private int countdownTime = 300; // 5 minutes in seconds
        private bool isRestarting = false;
        private bool isCheckingVersion = false;
        private int warningInterval = 30; // seconds between warnings
        private int saveTime = 10; // seconds before server save
        private int checkVersionInterval = 5; // seconds for checking installed version

        // Flags for closing and saving state
        private bool closeConfirmed = false;
        private bool alreadySaved = false; // Prevent multiple saves
        private bool isLaunching = false;

        public Launcher()
        {
            InitializeComponent();
            LoadConfig();
            LoadServerSettings();
            InstalledVersion();
            _ = LatestVersion();
            versionCheckTimer = new System.Windows.Forms.Timer();
            versionCheckTimer.Tick += VersionCheckTimer_Tick;
            SetTimerInterval();
            versionCheckTimer.Start();

            // Initialize the restart timer
            restartTimer = new System.Windows.Forms.Timer();
            restartTimer.Tick += RestartTimer_Tick;

            // Add the FormClosing event handler
            this.FormClosing += Form1_FormClosing;

            // Check if we should start the Factorio server on launch
            if (chkStartOnLaunch.Checked)
            {
                CheckAndLaunchFactorio();
            }

            // Add event handler for chckSpaceAge checkbox
            chckSpaceAge.CheckedChanged += chckSpaceAge_CheckedChanged;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeConfirmed)
            {
                e.Cancel = true; // Cancel the default close action

                // Start the closing process
                CloseApplication();
            }
        }

        private async void CloseApplication()
        {
            if (!closeConfirmed)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    closeConfirmed = true; // Prevent re-entry

                    // Disable the form to prevent further interaction
                    this.Enabled = false;

                    // Detach event handlers to prevent them from being called after form is disposed
                    if (factorioProcess != null)
                    {
                        factorioProcess.OutputDataReceived -= FactorioProcess_OutputDataReceived;
                        factorioProcess.ErrorDataReceived -= FactorioProcess_ErrorDataReceived;
                        factorioProcess.Exited -= FactorioProcess_Exited;
                    }

                    // Now handle Factorio process termination
                    await CloseFactorio();

                    // Dispose of the Factorio process
                    factorioProcess?.Dispose();
                    factorioProcess = null;

                    // Force garbage collection
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    // Now close the form
                    this.Invoke((Action)(() =>
                    {
                        this.closeConfirmed = true;
                        this.Close();
                    }));
                }
                else
                {
                    // User chose not to exit, so reset the flag
                    closeConfirmed = false;
                }
            }
        }

        private async Task<bool> CloseFactorio()
        {
            try
            {
                if (!alreadySaved && factorioProcess != null && !factorioProcess.HasExited)
                {
                    alreadySaved = true; // Prevent re-entry

                    // Send the /server-save command to the Factorio server
                    SendCommandToFactorio("Server will close in 5 seconds");
                    SendCommandToFactorio("/server-save");

                    // Wait for the save operation to complete
                    await Task.Delay(5000); // Adjust the delay as necessary

                    // Terminate the Factorio process
                    factorioProcess.Kill();

                    // Wait for the process to exit
                    factorioProcess.WaitForExit();

                    return true; // Indicate success
                }
                return false; // Indicate failure or no action needed
            }
            catch (Exception ex)
            {
                // Optionally log the exception if needed
                // Since the form may be closed, avoid showing message boxes
                return false; // Indicate failure
            }
        }

        /// <summary>
        /// Loads the configuration settings from the config file.
        /// If the config file does not exist, it creates a default one.
        /// </summary>
        private void LoadConfig()
        {
            if (!File.Exists(configFilePath))
            {
                CreateDefaultConfig();
            }

            string[] lines = File.ReadAllLines(configFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { '=' }, 2);
                if (parts.Length == 2)
                {
                    configEntries[parts[0]] = parts[1];
                }
            }

            if (configEntries.ContainsKey("factorioPath"))
            {
                factorioPath = configEntries["factorioPath"];
                txtLocation.Text = factorioPath;
            }

            if (configEntries.ContainsKey("saveFile"))
            {
                saveFile = configEntries["saveFile"];
                if (string.IsNullOrEmpty(saveFile))
                {
                    // Set default save file path within the app folder
                    string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                    saveFile = Path.Combine(appFolder, "saves", "save.zip");
                    SaveSetting("saveFile", saveFile); // Save back to config
                }
                txtSave.Text = saveFile;
            }
            else
            {
                // If saveFile is not in config, set default
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                saveFile = Path.Combine(appFolder, "saves", "save.zip");
                SaveSetting("saveFile", saveFile); // Save back to config
                txtSave.Text = saveFile;
            }

            if (configEntries.ContainsKey("chkStartOnLaunch"))
            {
                bool.TryParse(configEntries["chkStartOnLaunch"], out bool isChecked);
                chkStartOnLaunch.Checked = isChecked;
            }

            if (configEntries.ContainsKey("numCheckCurrent"))
            {
                if (int.TryParse(configEntries["numCheckCurrent"], out int interval))
                {
                    numCheckCurrent.Value = interval;
                }
            }

            // Load Update API settings
            if (configEntries.ContainsKey("UpdateAPIusername"))
            {
                txtUsername.Text = configEntries["UpdateAPIusername"];
            }
            if (configEntries.ContainsKey("UpdateAPItoken"))
            {
                txtToken.Text = configEntries["UpdateAPItoken"];
            }

            // Load chckSpaceAge state
            if (configEntries.ContainsKey("chckSpaceAge"))
            {
                bool.TryParse(configEntries["chckSpaceAge"], out bool isChecked);
                chckSpaceAge.Checked = isChecked;
            }
        }

        /// <summary>
        /// Creates a default configuration file with empty settings.
        /// </summary>
        private void CreateDefaultConfig()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string defaultFactorioPath = Path.Combine(appFolder, "bin", "x64", "factorio.exe");
            string defaultSaveFile = Path.Combine(appFolder, "saves", "save.zip");
            using (StreamWriter writer = new StreamWriter(configFilePath))
            {
                writer.WriteLine($"factorioPath={defaultFactorioPath}");
                writer.WriteLine($"saveFile={defaultSaveFile}");
                writer.WriteLine("chkStartOnLaunch=False");
                writer.WriteLine("numCheckCurrent=15");
                writer.WriteLine("UpdateAPIusername=");
                writer.WriteLine("UpdateAPItoken=");
                writer.WriteLine("chckSpaceAge=False");
            }
        }

        /// <summary>
        /// Saves the current configuration settings to the config file.
        /// </summary>
        private void SaveConfig()
        {
            List<string> lines = new List<string>();
            foreach (var entry in configEntries)
            {
                lines.Add($"{entry.Key}={entry.Value}");
            }
            File.WriteAllLines(configFilePath, lines);
        }

        /// <summary>
        /// Saves a specific setting to the configuration dictionary and updates the config file.
        /// </summary>
        /// <param name="key">The key of the setting to save.</param>
        /// <param name="value">The value of the setting to save.</param>
        private void SaveSetting(string key, string value)
        {
            configEntries[key] = value;
            SaveConfig();
        }

        /// <summary>
        /// Retrieves and displays the installed version of Factorio.
        /// </summary>
        private void InstalledVersion()
        {
            if (!string.IsNullOrEmpty(factorioPath) && File.Exists(factorioPath))
            {
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(factorioPath);
                string fullVersion = fileVersionInfo.FileVersion;
                string[] versionParts = fullVersion.Split('.');
                if (versionParts.Length >= 3)
                {
                    string trimmedVersion = string.Join(".", versionParts.Take(3));
                    txtInstalled.Text = trimmedVersion;
                }
            }
            else
            {
                txtInstalled.Text = "Factorio not found";
            }
        }

        /// <summary>
        /// Asynchronously fetches the latest stable version of Factorio from the API and displays it.
        /// </summary>
        private async Task LatestVersion()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(latestReleasesUrl);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(jsonResponse);
                    string headlessVersion = jsonObject["stable"]["headless"].ToString();
                    if (!string.IsNullOrEmpty(headlessVersion))
                    {
                        txtCurrent.Text = headlessVersion;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching the latest version: " + ex.Message);
            }
        }

        /// <summary>
        /// Asynchronously fetches the latest version and checks if it differs from the installed version.
        /// If they differ, it initiates a restart countdown.
        /// </summary>
        private async Task FetchLatestVersion()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(latestReleasesUrl);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(jsonResponse);
                    string headlessVersion = jsonObject["stable"]["headless"].ToString();
                    if (!string.IsNullOrEmpty(headlessVersion))
                    {
                        txtCurrent.Text = headlessVersion;

                        // Compare installed and current version
                        if (txtInstalled.Text != headlessVersion)
                        {
                            // Start the server restart countdown
                            StartRestartCountdown();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching the latest version: " + ex.Message);
            }
        }

        /// <summary>
        /// Initiates a countdown for restarting the server and sends a warning message to players.
        /// </summary>
        private void StartRestartCountdown()
        {
            if (isRestarting) return; // Prevent multiple countdowns

            isRestarting = true;
            countdownTime = 300; // Reset countdown time
            restartTimer.Interval = 1000; // 1 second intervals
            restartTimer.Start();

            SendCommandToFactorio($"Server will restart in {countdownTime} seconds.");
        }

        /// <summary>
        /// Timer tick event handler that manages the countdown for restarting the server.
        /// Sends messages at specified intervals and performs a server save before restarting.
        /// </summary>
        private void RestartTimer_Tick(object sender, EventArgs e)
        {
            countdownTime--;

            if (countdownTime % warningInterval == 0 || countdownTime <= 10) // Notify every 30 seconds or last 10 seconds
            {
                SendCommandToFactorio($"Server will restart in {countdownTime} seconds.");
            }

            if (countdownTime == saveTime)
            {
                SendCommandToFactorio("/server-save");
            }

            if (countdownTime <= 0)
            {
                restartTimer.Stop();
                KillFactorioProcess();
            }
        }

        /// <summary>
        /// Kills the Factorio process if it is running and initiates the version checking process.
        /// </summary>
        private void KillFactorioProcess()
        {
            if (factorioProcess != null && !factorioProcess.HasExited)
            {
                factorioProcess.Kill();
                factorioProcess.WaitForExit(); // Ensure process has fully exited
            }

            // Dispose of the Factorio process and set it to null
            factorioProcess?.Dispose();
            factorioProcess = null;

            isRestarting = false;

            var updateTask = UpdateFactorioViaWebsite();
            updateTask.ContinueWith(t =>
            {
                if (t.Result)
                {
                    StartCheckingInstalledVersion();
                }
            });
        }

        /// <summary>
        /// Initiates a periodic check for the installed version of Factorio until it matches the current version.
        /// </summary>
        private void StartCheckingInstalledVersion()
        {
            if (isCheckingVersion) return; // Prevent multiple checks

            isCheckingVersion = true;
            Task.Run(async () =>
            {
                while (isCheckingVersion)
                {
                    await Task.Delay(checkVersionInterval * 1000);
                    InstalledVersion(); // Check the installed version

                    // Check if installed version matches current version
                    if (txtInstalled.Text == txtCurrent.Text)
                    {
                        // Ensure Factorio is not already running
                        if (factorioProcess == null || factorioProcess.HasExited)
                        {
                            Invoke((Action)(() =>
                            {
                                LaunchFactorio(); // Launch the server if versions match
                            }));
                        }
                        isCheckingVersion = false; // Stop checking
                    }
                }
            });
        }

        /// <summary>
        /// Sets the interval for the version check timer based on user settings.
        /// </summary>
        private void SetTimerInterval()
        {
            int interval = (int)numCheckCurrent.Value * 60 * 1000;
            versionCheckTimer.Interval = interval;
        }

        /// <summary>
        /// Timer tick event handler that checks for the latest version of Factorio.
        /// </summary>
        private void VersionCheckTimer_Tick(object sender, EventArgs e)
        {
            _ = FetchLatestVersion();
        }

        /// <summary>
        /// Handles changes to the location text box and updates the Factorio path accordingly.
        /// </summary>
        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            string newPath = txtLocation.Text;
            if (!string.IsNullOrEmpty(newPath))
            {
                factorioPath = newPath;
                SaveSetting("factorioPath", newPath);
                InstalledVersion();
            }
        }

        /// <summary>
        /// Handles changes to the save file text box and updates the save file path accordingly.
        /// </summary>
        private void txtSave_TextChanged(object sender, EventArgs e)
        {
            string newSaveFilePath = txtSave.Text;
            if (!string.IsNullOrEmpty(newSaveFilePath))
            {
                saveFile = newSaveFilePath;
                SaveSetting("saveFile", newSaveFilePath);
            }
        }

        /// <summary>
        /// Opens a folder dialog to select the installation directory and updates the location text box.
        /// </summary>
        private void btnLocation_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Select Factorio Installation Directory";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowserDialog.SelectedPath;

                    // Set the factorioPath to where factorio.exe will be after installation
                    factorioPath = Path.Combine(selectedPath, "bin", "x64", "factorio.exe");

                    txtLocation.Text = factorioPath;
                    SaveSetting("factorioPath", factorioPath);
                    InstalledVersion();
                }
            }
        }

        /// <summary>
        /// Opens a file dialog to select a save file and updates the save file path.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Zip Files|*.zip";
                openFileDialog.Title = "Select a Save File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Store the full path to the save file
                    saveFile = openFileDialog.FileName;
                    txtSave.Text = saveFile;
                    SaveSetting("saveFile", saveFile);
                }
            }
        }

        /// <summary>
        /// Saves the current state of the 'Start on Launch' checkbox to the configuration.
        /// </summary>
        private void chkStartOnLaunch_CheckedChanged(object sender, EventArgs e)
        {
            SaveSetting("chkStartOnLaunch", chkStartOnLaunch.Checked.ToString());
        }

        /// <summary>
        /// Handles the Enter key press in the command text box to send the command to the server.
        /// </summary>
        private void TxtCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string command = txtCommand.Text;
                SendCommand(command);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Sends a command to the Factorio server through the factorio process.
        /// </summary>
        /// <param name="command">The command to send to the server.</param>
        private void SendCommand(string command)
        {
            if (!string.IsNullOrEmpty(command) && factorioProcess != null && !factorioProcess.HasExited)
            {
                factorioProcess.StandardInput.WriteLine(command);
                txtCommand.Clear();
            }
        }

        /// <summary>
        /// Updates the version check timer interval when the numeric value changes.
        /// </summary>
        private void numCheckCurrent_ValueChanged(object sender, EventArgs e)
        {
            SetTimerInterval();
        }

        /// <summary>
        /// Launches the Factorio server with the specified save file if the path and version are valid.
        /// </summary>
        private void LaunchFactorio()
        {
            if (isLaunching)
            {
                AppendConsoleText("Factorio launch is already in progress.");
                return;
            }

            var existingProcesses = Process.GetProcessesByName("factorio");
            if (existingProcesses.Length > 0)
            {
                foreach (var process in existingProcesses)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }

            isLaunching = true;

            AppendConsoleText("Attempting to launch Factorio...");

            if (factorioProcess != null && !factorioProcess.HasExited)
            {
                AppendConsoleText("Factorio is already running. Aborting launch.");
                isLaunching = false;
                return;
            }
            if (string.IsNullOrEmpty(factorioPath) || string.IsNullOrEmpty(saveFile))
            {
                MessageBox.Show("Please specify both the Factorio path and the save file.");
                isLaunching = false;
                return;
            }

            if (!File.Exists(factorioPath))
            {
                MessageBox.Show("Factorio executable not found.");
                isLaunching = false;
                return;
            }

            if (!File.Exists(saveFile))
            {
                MessageBox.Show($"Save file not found: {saveFile}");
                isLaunching = false;
                return;
            }

            string factorioDirectory = Path.GetDirectoryName(factorioPath);

            string startServerCommand = factorioPath; // Use the full path to the Factorio executable
            string arguments = $"--start-server \"{saveFile}\" --server-settings \"{Path.GetFullPath(serverSettingsFilePath)}\"";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = startServerCommand,
                Arguments = arguments,
                UseShellExecute = false, // Important: Set to false to redirect streams
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false, // Set to true if you don't want a window
                WorkingDirectory = factorioDirectory
            };

            try
            {
                factorioProcess = new Process();
                factorioProcess.StartInfo = startInfo;
                factorioProcess.EnableRaisingEvents = true;
                factorioProcess.OutputDataReceived += FactorioProcess_OutputDataReceived;
                factorioProcess.ErrorDataReceived += FactorioProcess_ErrorDataReceived;
                factorioProcess.Exited += FactorioProcess_Exited;
                factorioProcess.Start();
                factorioProcess.BeginOutputReadLine();
                factorioProcess.BeginErrorReadLine();

                versionCheckTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error launching Factorio: " + ex.Message);
            }

            isLaunching = false;
        }

        /// <summary>
        /// Handles output received from the Factorio server and displays it in the console.
        /// </summary>
        private void FactorioProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendConsoleText(e.Data);
            }
        }

        /// <summary>
        /// Handles error output received from the Factorio server and displays it in the console.
        /// </summary>
        private void FactorioProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendConsoleText("ERROR: " + e.Data);
            }
        }

        /// <summary>
        /// Handles the event when the Factorio server process exits.
        /// </summary>
        private void FactorioProcess_Exited(object sender, EventArgs e)
        {
            AppendConsoleText("Factorio server has exited.");

            factorioProcess?.Dispose();
            factorioProcess = null;
            isLaunching = false;
        }

        /// <summary>
        /// Handles changes to the username text box and updates the configuration.
        /// </summary>
        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            string newUsername = txtUsername.Text;
            SaveSetting("UpdateAPIusername", newUsername);

            // Update server settings username if necessary
            if (serverSettings != null)
            {
                serverSettings.username = newUsername;
            }
        }

        /// <summary>
        /// Handles changes to the token text box and updates the configuration.
        /// </summary>
        private void TxtToken_TextChanged(object sender, EventArgs e)
        {
            string newToken = txtToken.Text;
            SaveSetting("UpdateAPItoken", newToken);

            // Update server settings password if necessary
            if (serverSettings != null)
            {
                serverSettings.password = newToken;
            }
        }

        /// <summary>
        /// Initiates the update process via the Factorio Update API.
        /// </summary>
        private async Task<bool> UpdateFactorioViaWebsite()
        {
            string apiUsername = txtUsername.Text;
            string apiToken = txtToken.Text;

            if (string.IsNullOrEmpty(apiUsername) || string.IsNullOrEmpty(apiToken))
            {
                MessageBox.Show("Please provide both API username and token.");
                return false;
            }

            try
            {
                // Get available versions
                List<PackageList> packages = await GetAvailableVersions(apiUsername, apiToken);
                if (packages == null || packages.Count == 0)
                {
                    MessageBox.Show("No available updates found.");
                    return false;
                }

                // Determine the current installed version
                string currentVersion = txtInstalled.Text;

                // Determine the target version
                string targetVersion = txtCurrent.Text;

                // Determine the package string based on the platform and chckSpaceAge state
                string package = chckSpaceAge.Checked ? "core_expansion-win64" : "core-win64";

                AppendConsoleText(chckSpaceAge.Checked ? "Using Space Age expansion package." : "Using standard Factorio package.");

                // Find the update path from currentVersion to targetVersion
                var packageObj = packages.SelectMany(p => p.Packages)
                                         .FirstOrDefault(pkg => pkg.From == currentVersion && pkg.To == targetVersion && pkg.Platform == package);

                if (packageObj == null)
                {
                    MessageBox.Show("No valid update path found.");
                    return false;
                }

                // Get the download link
                string downloadLink = await GetDownloadLink(apiUsername, apiToken, package, currentVersion, targetVersion);
                if (string.IsNullOrEmpty(downloadLink))
                {
                    MessageBox.Show("Failed to retrieve download link.");
                    return false;
                }

                // Download the update package
                string updatePackagePath = Path.Combine(Path.GetTempPath(), $"{package}-{currentVersion}-{targetVersion}-update.zip");
                await DownloadUpdatePackage(downloadLink, updatePackagePath);

                // Apply the update
                bool applySuccess = await ApplyUpdatePackage(updatePackagePath);

                if (applySuccess)
                {
                    // Confirm the base mod version matches the expected target version
                    bool versionConfirmed = await ConfirmBaseModVersion(txtCurrent.Text);
                    if (!versionConfirmed)
                    {
                        return false; // Exit if update confirmation fails
                    }

                    // Continue with launching Factorio or other actions
                }
                else
                {
                    MessageBox.Show("Failed to apply update.");
                }

                // Optionally, delete the update package after applying
                if (File.Exists(updatePackagePath))
                {
                    await DeleteFileWithRetry(updatePackagePath);
                }

                // Return the result of applying the update
                return applySuccess;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating via website: " + ex.Message);
                return false;
            }
        }

        private string GetBaseModVersion()
        {
            string factorioDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(factorioPath), "..", ".."));
            try
            {
                string infoJsonPath = Path.Combine(factorioDirectory, "data", "base", "info.json");

                if (!File.Exists(infoJsonPath))
                {
                    throw new FileNotFoundException("Base mod info.json not found.");
                }

                var jsonData = File.ReadAllText(infoJsonPath);
                var jsonObject = JObject.Parse(jsonData);
                return jsonObject["version"]?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading base mod version: " + ex.Message);
                return null;
            }
        }

        private async Task<bool> ConfirmBaseModVersion(string targetVersion, int maxRetries = 10, int delay = 1000)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                string baseModVersion = GetBaseModVersion();

                if (baseModVersion == targetVersion)
                {
                    return true; // Update confirmed
                }

                await Task.Delay(delay); // Wait before retrying
            }

            MessageBox.Show("Failed to confirm base mod version. Update may not have completed successfully.");
            return false;
        }

        private async Task DeleteFileWithRetry(string filePath, int maxRetries = 5, int delay = 200)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    break; // Exit the loop if successful
                }
                catch (IOException)
                {
                    await Task.Delay(delay); // Wait before retrying
                }
            }
        }

        /// <summary>
        /// Represents a package in the available versions list.
        /// </summary>
        private class Package
        {
            public string Platform { get; set; }
            public string From { get; set; }
            public string To { get; set; }
        }

        /// <summary>
        /// Represents a list of packages for a specific platform.
        /// </summary>
        private class PackageList
        {
            public string Platform { get; set; }
            public List<Package> Packages { get; set; }
        }

        /// <summary>
        /// Retrieves the list of available update packages from the Update API.
        /// </summary>
        /// <param name="username">API username.</param>
        /// <param name="token">API token.</param>
        /// <returns>List of PackageList objects.</returns>
        private async Task<List<PackageList>> GetAvailableVersions(string username, string token)
        {
            string url = $"{updateApiDomain}/get-available-versions?username={(username)}&token={token}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Failed to get available versions: {response.StatusCode}");
                    return null;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response as a dictionary
                var platformPackagesDict = JsonConvert.DeserializeObject<Dictionary<string, List<Package>>>(jsonResponse);

                // Convert dictionary to list of PackageList objects with correct platform assignment
                List<PackageList> packageLists = platformPackagesDict.Select(kvp => new PackageList
                {
                    Platform = kvp.Key,
                    Packages = kvp.Value.Select(pkg =>
                    {
                        pkg.Platform = kvp.Key;  // Set the platform for each package manually
                        return pkg;
                    }).ToList()
                }).ToList();

                return packageLists;
            }
        }

        /// <summary>
        /// Retrieves the download link for a specific update package from the Update API.
        /// </summary>
        /// <param name="username">API username.</param>
        /// <param name="token">API token.</param>
        /// <param name="package">Package string.</param>
        /// <param name="fromVersion">Current version.</param>
        /// <param name="toVersion">Target version.</param>
        /// <returns>Download link as string.</returns>
        private async Task<string> GetDownloadLink(string username, string token, string package, string fromVersion, string toVersion)
        {
            string url = $"{updateApiDomain}/get-download-link?username={Uri.EscapeDataString(username)}&token={Uri.EscapeDataString(token)}&package={Uri.EscapeDataString(package)}&from={Uri.EscapeDataString(fromVersion)}&to={Uri.EscapeDataString(toVersion)}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Failed to get download link: {response.StatusCode}");
                    return null;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(jsonResponse);
                if (jsonArray.Count > 0)
                {
                    return jsonArray[0].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Downloads the update package from the specified URL to the given path.
        /// Includes progress reporting.
        /// </summary>
        /// <param name="url">Download URL.</param>
        /// <param name="destinationPath">Destination file path.</param>
        private async Task DownloadUpdatePackage(string url, string destinationPath)
        {
            AppendConsoleText("Starting download...");

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;
                var canReportProgress = totalBytes != -1;

                using (var fs = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    var totalRead = 0L;
                    var buffer = new byte[8192];
                    var isMoreToRead = true;
                    var lastReportedProgress = 0;

                    do
                    {
                        var read = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                        if (read == 0)
                        {
                            isMoreToRead = false;
                            ReportProgress(100); // Report 100% when done
                            continue;
                        }

                        await fs.WriteAsync(buffer, 0, read);

                        totalRead += read;

                        if (canReportProgress)
                        {
                            var progress = (int)((totalRead * 100) / totalBytes);
                            if (progress != lastReportedProgress)
                            {
                                lastReportedProgress = progress;
                                ReportProgress(progress);
                            }
                        }
                    }
                    while (isMoreToRead);
                }
            }

            AppendConsoleText($"Downloaded package to {destinationPath}");
        }

        /// <summary>
        /// Reports progress percentage to the console.
        /// </summary>
        /// <param name="progress">Progress percentage.</param>
        private void ReportProgress(int progress)
        {
            AppendConsoleText($"{progress}%");
        }

        /// <summary>
        /// Applies the downloaded update package by starting Factorio with the --apply-update argument.
        /// Includes progress reporting.
        /// </summary>
        /// <param name="updatePackagePath">Path to the update package.</param>
        private Task<bool> ApplyUpdatePackage(string updatePackagePath)
        {
            var tcs = new TaskCompletionSource<bool>();
            int lastReportedProgress = -1;

            if (!File.Exists(updatePackagePath))
            {
                MessageBox.Show("Update package not found.");
                tcs.SetResult(false);
                return tcs.Task;
            }

            string factorioDirectory = Path.GetDirectoryName(factorioPath);
            string arguments = $"--apply-update \"{updatePackagePath}\"";

            AppendConsoleText("Applying update...");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = factorioPath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
                WorkingDirectory = factorioDirectory
            };

            try
            {
                Process applyProcess = new Process();
                applyProcess.StartInfo = startInfo;
                applyProcess.EnableRaisingEvents = true;
                applyProcess.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        // Look for progress information in the output
                        if (e.Data.Contains("Progress"))
                        {
                            // Parse the progress percentage
                            var progressText = e.Data.Substring(e.Data.IndexOf("Progress") + 8).Trim();
                            if (int.TryParse(progressText.Replace("%", "").Trim(), out int progress))
                            {
                                if (progress != lastReportedProgress)
                                {
                                    lastReportedProgress = progress;
                                    ReportProgress(progress);
                                }
                            }
                        }
                        else
                        {
                            ApplyProcess_OutputDataReceived(sender, e);
                        }
                    }
                };
                applyProcess.ErrorDataReceived += ApplyProcess_ErrorDataReceived;
                applyProcess.Exited += (sender, e) =>
                {
                    // Force garbage collection after the process exits
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    tcs.SetResult(true);
                    ApplyProcess_Exited(sender, e);
                };
                applyProcess.Start();
                applyProcess.BeginOutputReadLine();
                applyProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying update: " + ex.Message);
                tcs.SetResult(false);
            }
            return tcs.Task;
        }

        /// <summary>
        /// Handles output received from the apply update process and displays it in the console.
        /// </summary>
        private void ApplyProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendConsoleText("Update: " + e.Data);
            }
        }

        /// <summary>
        /// Handles error output received from the apply update process and displays it in the console.
        /// </summary>
        private void ApplyProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendConsoleText("Update ERROR: " + e.Data);
            }
        }

        /// <summary>
        /// Handles the event when the apply update process exits.
        /// </summary>
        private async void ApplyProcess_Exited(object sender, EventArgs e)
        {
            AppendConsoleText("Update process has exited.");
        }

        /// <summary>
        /// Checks if the installed version matches the current version and launches the server if it does.
        /// </summary>
        private void CheckAndLaunchFactorio()
        {
            if (txtInstalled.Text != txtCurrent.Text)
            {
                StartCheckingInstalledVersion(); // Start checking for the installed version
            }
            else
            {
                LaunchFactorio();
            }
        }

        /// <summary>
        /// Starts the Factorio server based on the current configuration settings.
        /// If Factorio is not installed, it will download and install it.
        /// </summary>
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(factorioPath) || !File.Exists(factorioPath))
            {
                // Factorio is not installed or path is invalid
                DialogResult result = MessageBox.Show("Factorio is not installed. Would you like to download and install it now?", "Factorio Not Found", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    bool installSuccess = await DownloadAndInstallFactorio();
                    if (!installSuccess)
                    {
                        MessageBox.Show("Failed to download and install Factorio.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please specify a valid Factorio path.");
                    return;
                }
            }

            if (string.IsNullOrEmpty(saveFile) || !File.Exists(saveFile))
            {
                MessageBox.Show("Please specify a valid save file.");
                return;
            }

            if (txtUsername.Text == "" || txtToken.Text == "")
            {
                MessageBox.Show("Please enter your Factorio.com identifiers");
                return;
            }

            InstalledVersion(); // Update installed version

            if (txtInstalled.Text != txtCurrent.Text)
            {
                bool updateSuccess = await UpdateFactorioViaWebsite();

                if (updateSuccess)
                {
                    // After updating, refresh the installed version
                    InstalledVersion();
                    if (txtInstalled.Text == txtCurrent.Text)
                    {
                        LaunchFactorio();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update Factorio to the latest version.");
                    }
                }
                else
                {
                    MessageBox.Show("Failed to update Factorio.");
                }
            }
            else
            {
                LaunchFactorio();
            }
        }

        /// <summary>
        /// Downloads and installs Factorio into the directory specified in factorioPath.
        /// Adjusts the extraction to avoid creating an extra folder level.
        /// Includes progress reporting.
        /// </summary>
        /// <returns>True if installation is successful; otherwise, false.</returns>
        private async Task<bool> DownloadAndInstallFactorio()
        {
            string apiUsername = txtUsername.Text;
            string apiToken = txtToken.Text;
            string targetVersion = txtCurrent.Text;

            if (string.IsNullOrEmpty(apiUsername) || string.IsNullOrEmpty(apiToken))
            {
                MessageBox.Show("Please provide both API username and token.");
                return false;
            }

            if (string.IsNullOrEmpty(factorioPath))
            {
                MessageBox.Show("Please specify the installation path for Factorio in the Factorio Location field.");
                return false;
            }

            try
            {
                // Determine the channel based on chckSpaceAge state
                string channel = chckSpaceAge.Checked ? "expansion" : "alpha";
                string downloadType = "win64-manual";
                string downloadUrl = $"https://www.factorio.com/get-download/{targetVersion}/{channel}/{downloadType}?username={Uri.EscapeDataString(apiUsername)}&token={Uri.EscapeDataString(apiToken)}";

                AppendConsoleText(chckSpaceAge.Checked ? "Downloading Space Age expansion version..." : "Downloading standard Factorio version...");

                // Download the installer zip
                string installerZipPath = Path.Combine(Path.GetTempPath(), $"factorio_{targetVersion}_win64.zip");

                AppendConsoleText("Starting Factorio download...");

                await DownloadUpdatePackage(downloadUrl, installerZipPath);

                // Determine installation directory from factorioPath
                string factorioExecutablePath = factorioPath;
                string installationDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(factorioExecutablePath)));

                AppendConsoleText($"Extracting Factorio to {installationDirectory}...");
                AppendConsoleText("Starting extraction...");

                // Create the installation directory if it doesn't exist
                if (!Directory.Exists(installationDirectory))
                {
                    Directory.CreateDirectory(installationDirectory);
                }

                // Extract the zip file into the installation directory, skipping the top-level folder
                using (ZipArchive archive = ZipFile.OpenRead(installerZipPath))
                {
                    // Determine the top-level folder in the zip archive
                    string rootFolder = archive.Entries[0].FullName.Split('/')[0] + "/";
                    long totalEntries = archive.Entries.Count;
                    long processedEntries = 0;
                    int lastReportedProgress = -1;

                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        // Adjust the entry name to remove the root folder
                        string entryName = entry.FullName.Substring(rootFolder.Length);
                        if (string.IsNullOrEmpty(entryName))
                        {
                            processedEntries++;
                            continue; // Skip the root folder entry
                        }

                        string destinationPath = Path.Combine(installationDirectory, entryName);

                        // Ensure the directory exists
                        string destinationDir = Path.GetDirectoryName(destinationPath);
                        if (!Directory.Exists(destinationDir))
                        {
                            Directory.CreateDirectory(destinationDir);
                        }

                        if (string.IsNullOrEmpty(entry.Name))
                        {
                            // It's a directory
                            processedEntries++;
                            continue;
                        }
                        else
                        {
                            // It's a file
                            entry.ExtractToFile(destinationPath, true);
                            processedEntries++;
                        }

                        // Report progress
                        int progress = (int)((processedEntries * 100) / totalEntries);
                        if (progress != lastReportedProgress)
                        {
                            lastReportedProgress = progress;
                            ReportProgress(progress);
                        }
                    }
                }

                // Update factorioPath to point to the newly installed factorio.exe
                factorioPath = Path.Combine(installationDirectory, "bin", "x64", "factorio.exe");
                SaveSetting("factorioPath", factorioPath);
                txtLocation.Text = factorioPath;

                // Create the 'saves' directory within the application directory
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string savesDirectory = Path.Combine(appFolder, "saves");
                if (!Directory.Exists(savesDirectory))
                {
                    Directory.CreateDirectory(savesDirectory);
                    AppendConsoleText($"Created 'saves' directory at {savesDirectory}");
                }

                // If saveFile is not set, set the default save file path
                if (string.IsNullOrEmpty(saveFile))
                {
                    saveFile = Path.Combine(savesDirectory, "save.zip");
                    SaveSetting("saveFile", saveFile);
                    txtSave.Text = saveFile;
                }

                // Clean up the installer zip
                if (File.Exists(installerZipPath))
                {
                    File.Delete(installerZipPath);
                }

                AppendConsoleText($"Factorio {targetVersion} installed successfully at {installationDirectory}");
                InstalledVersion();

                // Force garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error downloading or installing Factorio: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Sends a command to the Factorio server through the factorio process.
        /// </summary>
        /// <param name="command">The command to send.</param>
        private void SendCommandToFactorio(string command)
        {
            if (factorioProcess != null && !factorioProcess.HasExited)
            {
                try
                {
                    factorioProcess.StandardInput.WriteLine(command);
                }
                catch (Exception ex)
                {
                    // Handle exceptions if necessary
                }
            }
        }

        /// <summary>
        /// Loads the server settings from the JSON file or creates it if it doesn't exist.
        /// </summary>
        private void LoadServerSettings()
        {
            if (!File.Exists(serverSettingsFilePath))
            {
                // Create default settings
                serverSettings = new ServerSettings();
                SaveServerSettings(); // Save to file
            }
            else
            {
                // Load settings from file
                string json = File.ReadAllText(serverSettingsFilePath);
                serverSettings = JsonConvert.DeserializeObject<ServerSettings>(json);
            }

            // Populate form fields with loaded settings
            PopulateServerSettingsToForm();
        }

        /// <summary>
        /// Saves the server settings to the JSON file.
        /// </summary>
        private void SaveServerSettings()
        {
            string json = JsonConvert.SerializeObject(serverSettings, Formatting.Indented);
            File.WriteAllText(serverSettingsFilePath, json);
        }

        /// <summary>
        /// Populates the form fields with the server settings.
        /// </summary>
        private void PopulateServerSettingsToForm()
        {
            txtServerName.Text = serverSettings.name;
            txtServerDescription.Text = serverSettings.description;
            txtServerTags.Text = string.Join(", ", serverSettings.tags);
            txtServerMaxPlayers.Text = serverSettings.max_players.ToString();
            chckServerPublic.Checked = serverSettings.visibility.@public;
            chckServerLan.Checked = serverSettings.visibility.lan;
            txtServerUsername.Text = serverSettings.username;
            txtServerPassword.Text = serverSettings.password;
            txtServerGamePassword.Text = serverSettings.game_password;
            chckServerUserVerif.Checked = serverSettings.require_user_verification;
            txtServerMaxUploadRate.Text = serverSettings.max_upload_in_kilobytes_per_second.ToString();
            txtServerMaxUploadSlots.Text = serverSettings.max_upload_slots.ToString();
            txtServerMinTicksLatency.Text = serverSettings.minimum_latency_in_ticks.ToString();
            txtServerMaxHeartbeats.Text = serverSettings.max_heartbeats_per_second.ToString();
            chckIgnorePlayerLimit.Checked = serverSettings.ignore_player_limit_for_returning_players;

            // Radio buttons for allow_commands
            if (serverSettings.allow_commands == "admins-only")
            {
                rdoServerCommandsAdmins.Checked = true;
            }
            else if (serverSettings.allow_commands == "true")
            {
                rdoServerCommandsTrue.Checked = true;
            }
            else if (serverSettings.allow_commands == "false")
            {
                rdoServerCommandsFalse.Checked = true;
            }

            txtServerAutosaveIntervals.Text = serverSettings.autosave_interval.ToString();
            txtServerAutosaveSlots.Text = serverSettings.autosave_slots.ToString();
            txtServerAutoKick.Text = serverSettings.afk_autokick_interval.ToString();
            chckServerAutoPauseWhenEmpty.Checked = serverSettings.auto_pause;
            chckServerAutopausOnJoin.Checked = serverSettings.auto_pause_when_players_connect;
            chckServerOnlyAdminsPause.Checked = serverSettings.only_admins_can_pause_the_game;
            chckServerAutoSaveOnServer.Checked = serverSettings.autosave_only_on_server;
            chckServerNonBlockingSaves.Checked = serverSettings.non_blocking_saving;
        }

        // Event handlers for server settings form fields

        private void TxtServerName_TextChanged(object sender, EventArgs e)
        {
            serverSettings.name = txtServerName.Text;
        }

        private void TxtServerDescription_TextChanged(object sender, EventArgs e)
        {
            serverSettings.description = txtServerDescription.Text;
        }

        private void TxtServerTags_TextChanged(object sender, EventArgs e)
        {
            serverSettings.tags = txtServerTags.Text.Split(',').Select(tag => tag.Trim()).ToList();
        }

        private void TxtServerMaxPlayers_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerMaxPlayers.Text, out int maxPlayers))
            {
                serverSettings.max_players = maxPlayers;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Max Players.");
            }
        }

        private void ChckServerPublic_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.visibility.@public = chckServerPublic.Checked;
        }

        private void ChckServerLan_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.visibility.lan = chckServerLan.Checked;
        }

        private void TxtServerUsername_TextChanged(object sender, EventArgs e)
        {
            serverSettings.username = txtServerUsername.Text;
        }

        private void TxtServerPassword_TextChanged(object sender, EventArgs e)
        {
            serverSettings.password = txtServerPassword.Text;
        }

        private void TxtServerGamePassword_TextChanged(object sender, EventArgs e)
        {
            serverSettings.game_password = txtServerGamePassword.Text;
        }

        private void ChckServerUserVerif_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.require_user_verification = chckServerUserVerif.Checked;
        }

        private void TxtServerMaxUploadRate_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerMaxUploadRate.Text, out int maxUpload))
            {
                serverSettings.max_upload_in_kilobytes_per_second = maxUpload;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Max Upload Rate.");
            }
        }

        private void TxtServerMaxUploadSlots_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerMaxUploadSlots.Text, out int maxUploadSlots))
            {
                serverSettings.max_upload_slots = maxUploadSlots;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Max Upload Slots.");
            }
        }

        private void TxtServerMinTicksLatency_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerMinTicksLatency.Text, out int minLatency))
            {
                serverSettings.minimum_latency_in_ticks = minLatency;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Minimum Latency.");
            }
        }

        private void TxtServerMaxHeartbeats_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerMaxHeartbeats.Text, out int maxHeartbeats))
            {
                serverSettings.max_heartbeats_per_second = maxHeartbeats;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Max Heartbeats.");
            }
        }

        private void ChckIgnorePlayerLimit_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.ignore_player_limit_for_returning_players = chckIgnorePlayerLimit.Checked;
        }

        private void RdoServerCommandsAdmins_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoServerCommandsAdmins.Checked)
            {
                serverSettings.allow_commands = "admins-only";
            }
        }

        private void RdoServerCommandsTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoServerCommandsTrue.Checked)
            {
                serverSettings.allow_commands = "true";
            }
        }

        private void RdoServerCommandsFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoServerCommandsFalse.Checked)
            {
                serverSettings.allow_commands = "false";
            }
        }

        private void TxtServerAutosaveIntervals_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerAutosaveIntervals.Text, out int autosaveInterval))
            {
                serverSettings.autosave_interval = autosaveInterval;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Autosave Interval.");
            }
        }

        private void TxtServerAutosaveSlots_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerAutosaveSlots.Text, out int autosaveSlots))
            {
                serverSettings.autosave_slots = autosaveSlots;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for Autosave Slots.");
            }
        }

        private void TxtServerAutoKick_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtServerAutoKick.Text, out int autoKickInterval))
            {
                serverSettings.afk_autokick_interval = autoKickInterval;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for AFK Autokick Interval.");
            }
        }

        private void ChckServerAutoPauseWhenEmpty_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.auto_pause = chckServerAutoPauseWhenEmpty.Checked;
        }

        private void ChckServerAutopausOnJoin_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.auto_pause_when_players_connect = chckServerAutopausOnJoin.Checked;
        }

        private void ChckServerOnlyAdminsPause_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.only_admins_can_pause_the_game = chckServerOnlyAdminsPause.Checked;
        }

        private void ChckServerAutoSaveOnServer_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.autosave_only_on_server = chckServerAutoSaveOnServer.Checked;
        }

        private void ChckServerNonBlockingSaves_CheckedChanged(object sender, EventArgs e)
        {
            serverSettings.non_blocking_saving = chckServerNonBlockingSaves.Checked;
        }

        private void btnServerSaveSettings_Click(object sender, EventArgs e)
        {
            SaveServerSettings();
            MessageBox.Show("Server settings have been saved.");
        }

        private void btnServerBackupSettings_Click(object sender, EventArgs e)
        {
            try
            {
                string backupFilePath = Path.Combine(Path.GetDirectoryName(serverSettingsFilePath), "server-settings.bkp");
                File.Copy(serverSettingsFilePath, backupFilePath, true);
                MessageBox.Show("Server settings have been backed up.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error backing up server settings: " + ex.Message);
            }
        }

        private void btnServerRestoreBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string backupFilePath = Path.Combine(Path.GetDirectoryName(serverSettingsFilePath), "server-settings.bkp");
                if (File.Exists(backupFilePath))
                {
                    File.Copy(backupFilePath, serverSettingsFilePath, true);
                    LoadServerSettings(); // Reload settings
                    MessageBox.Show("Server settings have been restored from backup.");
                }
                else
                {
                    MessageBox.Show("Backup file not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error restoring server settings: " + ex.Message);
            }
        }

        /// <summary>
        /// Appends text to the console and auto-scrolls to the bottom.
        /// </summary>
        /// <param name="text">Text to append.</param>
        private void AppendConsoleText(string text)
        {
            if (txtConsole.InvokeRequired)
            {
                txtConsole.Invoke(new Action(() =>
                {
                    txtConsole.AppendText(text + Environment.NewLine);
                    txtConsole.SelectionStart = txtConsole.Text.Length;
                    txtConsole.ScrollToCaret();
                }));
            }
            else
            {
                txtConsole.AppendText(text + Environment.NewLine);
                txtConsole.SelectionStart = txtConsole.Text.Length;
                txtConsole.ScrollToCaret();
            }
        }

        private void chckSpaceAge_CheckedChanged(object sender, EventArgs e)
        {
            SaveSetting("chckSpaceAge", chckSpaceAge.Checked.ToString());
        }

        /// <summary>
        /// New event handler for btnStopServer that stops the Factorio process without closing the application.
        /// </summary>
        private async void btnStop_Click(object sender, EventArgs e)
        {
            if (factorioProcess != null && !factorioProcess.HasExited)
            {
                AppendConsoleText("Stopping Factorio server...");
                await CloseFactorio(); // Reuse the existing method to stop Factorio
                AppendConsoleText("Factorio server has been stopped.");
            }
            else
            {
                AppendConsoleText("Factorio server is not running.");
            }
        }

        /// <summary>
        /// Event handler for the Check button that fetches the latest version.
        /// </summary>
        private async void btnCheck_Click(object sender, EventArgs e)
        {
            AppendConsoleText("Checking for the latest version...");
            await LatestVersion();
            AppendConsoleText("Latest version check completed.");
        }

        /// <summary>
        /// Event handler for the Generate Save button that generates a save file.
        /// </summary>
        private void btnGenerateSave_Click(object sender, EventArgs e)
        {
            // Implement logic to generate a save file
            string mapString = txtMapString.Text;
            GenerateSaveFile(mapString);
        }

        /// <summary>
        /// Event handler for the Send Command button that sends a command to the server.
        /// </summary>
        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            // Implement logic to send a command to the server
            string command = txtCommand.Text;
            SendCommand(command);
            txtCommand.Clear();
        }

        /// <summary>
        /// Implements the logic to generate a save file based on the provided map string.
        /// </summary>
        /// <param name="mapString">The map string to use for generating the save file.</param>
        private void GenerateSaveFile(string mapExchangeString)
        {
            if (string.IsNullOrEmpty(factorioPath) || !File.Exists(factorioPath))
            {
                MessageBox.Show("Factorio executable not found.");
                return;
            }

            // Determine the save file path
            string saveFilePath = saveFile;
            if (string.IsNullOrEmpty(saveFilePath))
            {
                MessageBox.Show("Please specify a save file path.");
                return;
            }

            string mapGenSettingsPath = Path.Combine(Path.GetDirectoryName(saveFilePath), "map-gen-settings.json");

            if (!string.IsNullOrEmpty(mapExchangeString))
            {
                try
                {
                    // Remove any prefix (e.g., ">>>factorio") using regex
                    mapExchangeString = Regex.Replace(mapExchangeString, @"^>>>[^\s]+", "");

                    // Remove any leading or trailing whitespace
                    mapExchangeString = mapExchangeString.Trim();

                    // Base64-decode the map exchange string
                    byte[] decodedData = Convert.FromBase64String(mapExchangeString);

                    // Decompress the data using zlib (Deflate), skipping the version byte
                    string jsonContent = DecompressMapExchangeString(decodedData);

                    // Reformat the JSON for readability (optional)
                    var parsedJson = Newtonsoft.Json.Linq.JObject.Parse(jsonContent);
                    jsonContent = parsedJson.ToString(Newtonsoft.Json.Formatting.Indented);

                    // Save the JSON content to map-gen-settings.json
                    File.WriteAllText(mapGenSettingsPath, jsonContent);

                    AppendConsoleText($"Map generation settings saved to {mapGenSettingsPath}");
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Invalid map exchange string format: " + ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error decoding map exchange string: " + ex.Message);
                    return;
                }
            }
            else
            {
                // If no map exchange string is provided, use default settings or handle accordingly
                AppendConsoleText("No map exchange string provided. Using default map generation settings.");
            }

            // Build the arguments
            string arguments = $"--create \"{saveFilePath}\"";

            if (!string.IsNullOrEmpty(mapExchangeString))
            {
                arguments += $" --map-gen-settings \"{mapGenSettingsPath}\"";
            }

            string factorioDirectory = Path.GetDirectoryName(factorioPath);

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = factorioPath,
                Arguments = arguments,
                UseShellExecute = false, // Important: Set to false to redirect streams
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true, // Set to true if you don't want a window
                WorkingDirectory = factorioDirectory
            };

            try
            {
                Process createProcess = new Process();
                createProcess.StartInfo = startInfo;
                createProcess.EnableRaisingEvents = true;
                createProcess.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        AppendConsoleText(e.Data);
                    }
                };
                createProcess.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        AppendConsoleText("ERROR: " + e.Data);
                    }
                };
                createProcess.Exited += (sender, e) =>
                {
                    AppendConsoleText("Save file creation process has exited.");
                    createProcess.Dispose();

                    // Optionally delete the map-gen-settings.json file after creation
                    if (File.Exists(mapGenSettingsPath))
                    {
                        File.Delete(mapGenSettingsPath);
                    }
                };
                createProcess.Start();
                createProcess.BeginOutputReadLine();
                createProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating save file: " + ex.Message);
            }
        }

        // Helper method to decompress the map exchange string
        private string DecompressMapExchangeString(byte[] decodedData)
        {
            // Skip the first byte (version byte)
            using (var compressedStream = new MemoryStream(decodedData, 1, decodedData.Length - 1))
            using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            using (var reader = new StreamReader(deflateStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }


        // ServerSettings and Visibility classes
        public class ServerSettings
    {
        public string name { get; set; } = "Name of the game as it will appear in the game listing";
        public string description { get; set; } = "Description of the game that will appear in the listing";
        public List<string> tags { get; set; } = new List<string> { "game", "tags" };
        public int max_players { get; set; } = 0;
        public Visibility visibility { get; set; } = new Visibility();
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        public string token { get; set; } = "";
        public string game_password { get; set; } = "";
        public bool require_user_verification { get; set; } = true;
        public int max_upload_in_kilobytes_per_second { get; set; } = 0;
        public int max_upload_slots { get; set; } = 5;
        public int minimum_latency_in_ticks { get; set; } = 0;
        public int max_heartbeats_per_second { get; set; } = 60;
        public bool ignore_player_limit_for_returning_players { get; set; } = false;
        public string allow_commands { get; set; } = "admins-only";
        public int autosave_interval { get; set; } = 10;
        public int autosave_slots { get; set; } = 5;
        public int afk_autokick_interval { get; set; } = 0;
        public bool auto_pause { get; set; } = true;
        public bool auto_pause_when_players_connect { get; set; } = false;
        public bool only_admins_can_pause_the_game { get; set; } = true;
        public bool autosave_only_on_server { get; set; } = true;
        public bool non_blocking_saving { get; set; } = false;
        public int minimum_segment_size { get; set; } = 25; // NOT MODIFIABLE
        public int minimum_segment_size_peer_count { get; set; } = 20; // NOT MODIFIABLE
        public int maximum_segment_size { get; set; } = 100; // NOT MODIFIABLE
        public int maximum_segment_size_peer_count { get; set; } = 10; // NOT MODIFIABLE
    }

    public class Visibility
    {
        public bool @public { get; set; } = true;
        public bool lan { get; set; } = true;
    }
}
