namespace Facto
{
    partial class Launcher
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            txtInstalled = new TextBox();
            txtCurrent = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtConsole = new RichTextBox();
            txtLocation = new TextBox();
            label3 = new Label();
            btnLocation = new Button();
            txtCommand = new TextBox();
            btnStart = new Button();
            btnCheck = new Button();
            chkStartOnLaunch = new CheckBox();
            label4 = new Label();
            numCheckCurrent = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            txtSave = new TextBox();
            btnSave = new Button();
            gbUpdateTrough = new GroupBox();
            lblToken = new Label();
            label9 = new Label();
            txtToken = new TextBox();
            txtUsername = new TextBox();
            groupBox1 = new GroupBox();
            chckSpaceAge = new CheckBox();
            btnStopServer = new Button();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            btnSendCommand = new Button();
            groupBox7 = new GroupBox();
            label8 = new Label();
            txtMapString = new TextBox();
            label7 = new Label();
            btnGenerateSave = new Button();
            tabPage1 = new TabPage();
            btnServerRestoreBackup = new Button();
            btnServerBackupSettings = new Button();
            btnServerSaveSettings = new Button();
            groupBox6 = new GroupBox();
            chckServerNonBlockingSaves = new CheckBox();
            chckServerAutoSaveOnServer = new CheckBox();
            chckIgnorePlayerLimit = new CheckBox();
            label20 = new Label();
            label19 = new Label();
            txtServerMaxHeartbeats = new TextBox();
            label18 = new Label();
            txtServerMinTicksLatency = new TextBox();
            label17 = new Label();
            txtServerMaxUploadSlots = new TextBox();
            label16 = new Label();
            label15 = new Label();
            txtServerMaxUploadRate = new TextBox();
            label14 = new Label();
            chckServerUserVerif = new CheckBox();
            groupBox4 = new GroupBox();
            label26 = new Label();
            txtServerAutoKick = new TextBox();
            label25 = new Label();
            txtServerAutosaveSlots = new TextBox();
            label23 = new Label();
            label22 = new Label();
            txtServerAutosaveIntervals = new TextBox();
            label21 = new Label();
            chckServerOnlyAdminsPause = new CheckBox();
            txtServerMaxPlayers = new TextBox();
            label24 = new Label();
            chckServerAutopausOnJoin = new CheckBox();
            chckServerAutoPauseWhenEmpty = new CheckBox();
            groupBox5 = new GroupBox();
            rdoServerCommandsAdmins = new RadioButton();
            rdoServerCommandsFalse = new RadioButton();
            rdoServerCommandsTrue = new RadioButton();
            txtServerGamePassword = new TextBox();
            label13 = new Label();
            groupBox3 = new GroupBox();
            gb = new GroupBox();
            lblServerUsername = new Label();
            txtServerPassword = new TextBox();
            lblServerPassword = new Label();
            txtServerUsername = new TextBox();
            chckServerLan = new CheckBox();
            chckServerPublic = new CheckBox();
            groupBox2 = new GroupBox();
            txtServerTags = new TextBox();
            label12 = new Label();
            txtServerDescription = new TextBox();
            label11 = new Label();
            txtServerName = new TextBox();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)numCheckCurrent).BeginInit();
            gbUpdateTrough.SuspendLayout();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox7.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox3.SuspendLayout();
            gb.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // txtInstalled
            // 
            txtInstalled.Location = new Point(6, 37);
            txtInstalled.Name = "txtInstalled";
            txtInstalled.ReadOnly = true;
            txtInstalled.Size = new Size(100, 23);
            txtInstalled.TabIndex = 0;
            // 
            // txtCurrent
            // 
            txtCurrent.Location = new Point(137, 37);
            txtCurrent.Name = "txtCurrent";
            txtCurrent.ReadOnly = true;
            txtCurrent.Size = new Size(100, 23);
            txtCurrent.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 2;
            label1.Text = "Installed";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(137, 19);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 3;
            label2.Text = "Current";
            // 
            // txtConsole
            // 
            txtConsole.BackColor = SystemColors.InfoText;
            txtConsole.ForeColor = SystemColors.Info;
            txtConsole.Location = new Point(347, 6);
            txtConsole.Name = "txtConsole";
            txtConsole.ReadOnly = true;
            txtConsole.Size = new Size(459, 402);
            txtConsole.TabIndex = 5;
            txtConsole.Text = "";
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(6, 310);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(231, 23);
            txtLocation.TabIndex = 6;
            txtLocation.TextChanged += txtLocation_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 292);
            label3.Name = "label3";
            label3.Size = new Size(101, 15);
            label3.TabIndex = 7;
            label3.Text = "Factorio Directory";
            // 
            // btnLocation
            // 
            btnLocation.Location = new Point(243, 310);
            btnLocation.Name = "btnLocation";
            btnLocation.Size = new Size(75, 23);
            btnLocation.TabIndex = 8;
            btnLocation.Text = "Select";
            btnLocation.UseVisualStyleBackColor = true;
            btnLocation.Click += btnLocation_Click;
            // 
            // txtCommand
            // 
            txtCommand.BackColor = SystemColors.InfoText;
            txtCommand.ForeColor = SystemColors.Info;
            txtCommand.Location = new Point(347, 409);
            txtCommand.Name = "txtCommand";
            txtCommand.Size = new Size(348, 23);
            txtCommand.TabIndex = 9;
            txtCommand.KeyPress += TxtCommand_KeyPress;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(6, 383);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(138, 53);
            btnStart.TabIndex = 10;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(243, 36);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(75, 23);
            btnCheck.TabIndex = 11;
            btnCheck.Text = "Check";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += btnCheck_Click;
            // 
            // chkStartOnLaunch
            // 
            chkStartOnLaunch.AutoSize = true;
            chkStartOnLaunch.Location = new Point(6, 88);
            chkStartOnLaunch.Name = "chkStartOnLaunch";
            chkStartOnLaunch.Size = new Size(106, 19);
            chkStartOnLaunch.TabIndex = 12;
            chkStartOnLaunch.Text = "Start on launch";
            chkStartOnLaunch.UseVisualStyleBackColor = true;
            chkStartOnLaunch.CheckedChanged += chkStartOnLaunch_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(137, 66);
            label4.Name = "label4";
            label4.Size = new Size(129, 15);
            label4.TabIndex = 14;
            label4.Text = "Check for update every";
            // 
            // numCheckCurrent
            // 
            numCheckCurrent.Location = new Point(153, 84);
            numCheckCurrent.Name = "numCheckCurrent";
            numCheckCurrent.Size = new Size(47, 23);
            numCheckCurrent.TabIndex = 15;
            numCheckCurrent.Value = new decimal(new int[] { 15, 0, 0, 0 });
            numCheckCurrent.ValueChanged += numCheckCurrent_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(206, 90);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 16;
            label5.Text = "minutes";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 336);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 17;
            label6.Text = "Save file";
            // 
            // txtSave
            // 
            txtSave.Location = new Point(6, 354);
            txtSave.Name = "txtSave";
            txtSave.Size = new Size(231, 23);
            txtSave.TabIndex = 18;
            txtSave.TextChanged += txtSave_TextChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(243, 354);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 19;
            btnSave.Text = "Select";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // gbUpdateTrough
            // 
            gbUpdateTrough.Controls.Add(lblToken);
            gbUpdateTrough.Controls.Add(label9);
            gbUpdateTrough.Controls.Add(txtToken);
            gbUpdateTrough.Controls.Add(txtUsername);
            gbUpdateTrough.Location = new Point(6, 131);
            gbUpdateTrough.Name = "gbUpdateTrough";
            gbUpdateTrough.Size = new Size(328, 70);
            gbUpdateTrough.TabIndex = 24;
            gbUpdateTrough.TabStop = false;
            gbUpdateTrough.Text = "Factorio.com";
            gbUpdateTrough.UseCompatibleTextRendering = true;
            // 
            // lblToken
            // 
            lblToken.AutoSize = true;
            lblToken.Location = new Point(112, 19);
            lblToken.Name = "lblToken";
            lblToken.Size = new Size(38, 15);
            lblToken.TabIndex = 29;
            lblToken.Text = "Token";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 19);
            label9.Name = "label9";
            label9.Size = new Size(60, 15);
            label9.TabIndex = 28;
            label9.Text = "Username";
            // 
            // txtToken
            // 
            txtToken.Location = new Point(112, 37);
            txtToken.Name = "txtToken";
            txtToken.PasswordChar = '*';
            txtToken.Size = new Size(100, 23);
            txtToken.TabIndex = 27;
            txtToken.TextChanged += TxtToken_TextChanged;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(6, 37);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(100, 23);
            txtUsername.TabIndex = 26;
            txtUsername.TextChanged += TxtUsername_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chckSpaceAge);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtInstalled);
            groupBox1.Controls.Add(txtCurrent);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnCheck);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(numCheckCurrent);
            groupBox1.Controls.Add(chkStartOnLaunch);
            groupBox1.Controls.Add(label5);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(328, 127);
            groupBox1.TabIndex = 26;
            groupBox1.TabStop = false;
            groupBox1.Text = "Version:";
            // 
            // chckSpaceAge
            // 
            chckSpaceAge.AutoSize = true;
            chckSpaceAge.Checked = true;
            chckSpaceAge.CheckState = CheckState.Checked;
            chckSpaceAge.Location = new Point(6, 66);
            chckSpaceAge.Name = "chckSpaceAge";
            chckSpaceAge.Size = new Size(81, 19);
            chckSpaceAge.TabIndex = 17;
            chckSpaceAge.Text = "Space Age";
            chckSpaceAge.UseVisualStyleBackColor = true;
            chckSpaceAge.CheckedChanged += chckSpaceAge_CheckedChanged;
            // 
            // btnStopServer
            // 
            btnStopServer.Location = new Point(180, 383);
            btnStopServer.Name = "btnStopServer";
            btnStopServer.Size = new Size(138, 53);
            btnStopServer.TabIndex = 27;
            btnStopServer.Text = "Stop";
            btnStopServer.UseVisualStyleBackColor = true;
            btnStopServer.Click += btnStop_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(12, 6);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(820, 469);
            tabControl1.TabIndex = 29;
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(btnSendCommand);
            tabPage2.Controls.Add(groupBox7);
            tabPage2.Controls.Add(txtConsole);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(btnStopServer);
            tabPage2.Controls.Add(btnSave);
            tabPage2.Controls.Add(gbUpdateTrough);
            tabPage2.Controls.Add(txtSave);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Controls.Add(txtCommand);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(btnStart);
            tabPage2.Controls.Add(txtLocation);
            tabPage2.Controls.Add(btnLocation);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(812, 441);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Server";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSendCommand
            // 
            btnSendCommand.Location = new Point(701, 409);
            btnSendCommand.Name = "btnSendCommand";
            btnSendCommand.Size = new Size(105, 23);
            btnSendCommand.TabIndex = 33;
            btnSendCommand.Text = "Send";
            btnSendCommand.UseVisualStyleBackColor = true;
            btnSendCommand.Click += btnSendCommand_Click;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label8);
            groupBox7.Controls.Add(txtMapString);
            groupBox7.Controls.Add(label7);
            groupBox7.Controls.Add(btnGenerateSave);
            groupBox7.Location = new Point(6, 207);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(328, 82);
            groupBox7.TabIndex = 32;
            groupBox7.TabStop = false;
            groupBox7.Text = "Save File Generation";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 23);
            label8.Name = "label8";
            label8.Size = new Size(119, 15);
            label8.TabIndex = 30;
            label8.Text = "Map Exchange String";
            // 
            // txtMapString
            // 
            txtMapString.Enabled = false;
            txtMapString.Location = new Point(131, 20);
            txtMapString.Name = "txtMapString";
            txtMapString.PlaceholderText = "-IN DEVELOPEMENT- Leave blank for default";
            txtMapString.Size = new Size(187, 23);
            txtMapString.TabIndex = 31;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 53);
            label7.Name = "label7";
            label7.Size = new Size(154, 15);
            label7.TabIndex = 28;
            label7.Text = "Generate save file if needed:";
            // 
            // btnGenerateSave
            // 
            btnGenerateSave.Location = new Point(165, 49);
            btnGenerateSave.Name = "btnGenerateSave";
            btnGenerateSave.Size = new Size(153, 23);
            btnGenerateSave.TabIndex = 29;
            btnGenerateSave.Text = "Generate";
            btnGenerateSave.UseVisualStyleBackColor = true;
            btnGenerateSave.Click += btnGenerateSave_Click;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(btnServerRestoreBackup);
            tabPage1.Controls.Add(btnServerBackupSettings);
            tabPage1.Controls.Add(btnServerSaveSettings);
            tabPage1.Controls.Add(groupBox6);
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(812, 441);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Settings";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnServerRestoreBackup
            // 
            btnServerRestoreBackup.Location = new Point(210, 399);
            btnServerRestoreBackup.Name = "btnServerRestoreBackup";
            btnServerRestoreBackup.Size = new Size(200, 28);
            btnServerRestoreBackup.TabIndex = 11;
            btnServerRestoreBackup.Text = "Restore Settings";
            btnServerRestoreBackup.UseVisualStyleBackColor = true;
            btnServerRestoreBackup.Click += btnServerRestoreBackup_Click;
            // 
            // btnServerBackupSettings
            // 
            btnServerBackupSettings.Location = new Point(6, 399);
            btnServerBackupSettings.Name = "btnServerBackupSettings";
            btnServerBackupSettings.Size = new Size(198, 28);
            btnServerBackupSettings.TabIndex = 10;
            btnServerBackupSettings.Text = "Backup Settings";
            btnServerBackupSettings.UseVisualStyleBackColor = true;
            btnServerBackupSettings.Click += btnServerBackupSettings_Click;
            // 
            // btnServerSaveSettings
            // 
            btnServerSaveSettings.Location = new Point(6, 349);
            btnServerSaveSettings.Name = "btnServerSaveSettings";
            btnServerSaveSettings.Size = new Size(404, 44);
            btnServerSaveSettings.TabIndex = 9;
            btnServerSaveSettings.Text = "Save Settings";
            btnServerSaveSettings.UseVisualStyleBackColor = true;
            btnServerSaveSettings.Click += btnServerSaveSettings_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(chckServerNonBlockingSaves);
            groupBox6.Controls.Add(chckServerAutoSaveOnServer);
            groupBox6.Controls.Add(chckIgnorePlayerLimit);
            groupBox6.Controls.Add(label20);
            groupBox6.Controls.Add(label19);
            groupBox6.Controls.Add(txtServerMaxHeartbeats);
            groupBox6.Controls.Add(label18);
            groupBox6.Controls.Add(txtServerMinTicksLatency);
            groupBox6.Controls.Add(label17);
            groupBox6.Controls.Add(txtServerMaxUploadSlots);
            groupBox6.Controls.Add(label16);
            groupBox6.Controls.Add(label15);
            groupBox6.Controls.Add(txtServerMaxUploadRate);
            groupBox6.Controls.Add(label14);
            groupBox6.Controls.Add(chckServerUserVerif);
            groupBox6.Location = new Point(422, 208);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(384, 230);
            groupBox6.TabIndex = 8;
            groupBox6.TabStop = false;
            groupBox6.Text = "Others (Do not touch unless you know what you are doing)";
            // 
            // chckServerNonBlockingSaves
            // 
            chckServerNonBlockingSaves.AutoSize = true;
            chckServerNonBlockingSaves.Location = new Point(6, 184);
            chckServerNonBlockingSaves.Name = "chckServerNonBlockingSaves";
            chckServerNonBlockingSaves.Size = new Size(212, 19);
            chckServerNonBlockingSaves.TabIndex = 18;
            chckServerNonBlockingSaves.Text = "Non-Blocking Saves (experimental)";
            chckServerNonBlockingSaves.UseVisualStyleBackColor = true;
            chckServerNonBlockingSaves.CheckedChanged += ChckServerNonBlockingSaves_CheckedChanged;
            // 
            // chckServerAutoSaveOnServer
            // 
            chckServerAutoSaveOnServer.AutoSize = true;
            chckServerAutoSaveOnServer.Location = new Point(6, 162);
            chckServerAutoSaveOnServer.Name = "chckServerAutoSaveOnServer";
            chckServerAutoSaveOnServer.Size = new Size(152, 19);
            chckServerAutoSaveOnServer.TabIndex = 17;
            chckServerAutoSaveOnServer.Text = "Autosave on server only";
            chckServerAutoSaveOnServer.UseVisualStyleBackColor = true;
            chckServerAutoSaveOnServer.CheckedChanged += ChckServerAutoSaveOnServer_CheckedChanged;
            // 
            // chckIgnorePlayerLimit
            // 
            chckIgnorePlayerLimit.AutoSize = true;
            chckIgnorePlayerLimit.Location = new Point(6, 139);
            chckIgnorePlayerLimit.Name = "chckIgnorePlayerLimit";
            chckIgnorePlayerLimit.Size = new Size(238, 19);
            chckIgnorePlayerLimit.TabIndex = 12;
            chckIgnorePlayerLimit.Text = "Ignore Player Limit for Returning Players";
            chckIgnorePlayerLimit.UseVisualStyleBackColor = true;
            chckIgnorePlayerLimit.CheckedChanged += ChckIgnorePlayerLimit_CheckedChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(260, 85);
            label20.Name = "label20";
            label20.Size = new Size(93, 15);
            label20.TabIndex = 16;
            label20.Text = "per milliseconds";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(260, 113);
            label19.Name = "label19";
            label19.Size = new Size(70, 15);
            label19.TabIndex = 15;
            label19.Text = "per seconds";
            // 
            // txtServerMaxHeartbeats
            // 
            txtServerMaxHeartbeats.Location = new Point(111, 110);
            txtServerMaxHeartbeats.Name = "txtServerMaxHeartbeats";
            txtServerMaxHeartbeats.Size = new Size(143, 23);
            txtServerMaxHeartbeats.TabIndex = 14;
            txtServerMaxHeartbeats.TextChanged += TxtServerMaxHeartbeats_TextChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(6, 114);
            label18.Name = "label18";
            label18.Size = new Size(90, 15);
            label18.TabIndex = 13;
            label18.Text = "Max Heartbeats";
            // 
            // txtServerMinTicksLatency
            // 
            txtServerMinTicksLatency.Location = new Point(111, 81);
            txtServerMinTicksLatency.Name = "txtServerMinTicksLatency";
            txtServerMinTicksLatency.Size = new Size(143, 23);
            txtServerMinTicksLatency.TabIndex = 12;
            txtServerMinTicksLatency.TextChanged += TxtServerMinTicksLatency_TextChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(6, 85);
            label17.Name = "label17";
            label17.Size = new Size(101, 15);
            label17.TabIndex = 11;
            label17.Text = "Min Ticks Latency";
            // 
            // txtServerMaxUploadSlots
            // 
            txtServerMaxUploadSlots.Location = new Point(111, 53);
            txtServerMaxUploadSlots.Name = "txtServerMaxUploadSlots";
            txtServerMaxUploadSlots.Size = new Size(143, 23);
            txtServerMaxUploadSlots.TabIndex = 10;
            txtServerMaxUploadSlots.TextChanged += TxtServerMaxUploadSlots_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 57);
            label16.Name = "label16";
            label16.Size = new Size(99, 15);
            label16.TabIndex = 9;
            label16.Text = "Max Upload Slots";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(260, 28);
            label15.Name = "label15";
            label15.Size = new Size(32, 15);
            label15.TabIndex = 8;
            label15.Text = "kbps";
            // 
            // txtServerMaxUploadRate
            // 
            txtServerMaxUploadRate.Location = new Point(166, 24);
            txtServerMaxUploadRate.Name = "txtServerMaxUploadRate";
            txtServerMaxUploadRate.Size = new Size(88, 23);
            txtServerMaxUploadRate.TabIndex = 7;
            txtServerMaxUploadRate.TextChanged += TxtServerMaxUploadRate_TextChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 28);
            label14.Name = "label14";
            label14.Size = new Size(154, 15);
            label14.TabIndex = 6;
            label14.Text = "Max Upload (0 = Unlimited)";
            // 
            // chckServerUserVerif
            // 
            chckServerUserVerif.AutoSize = true;
            chckServerUserVerif.Location = new Point(6, 205);
            chckServerUserVerif.Name = "chckServerUserVerif";
            chckServerUserVerif.Size = new Size(154, 19);
            chckServerUserVerif.TabIndex = 10;
            chckServerUserVerif.Text = "Require User Verification";
            chckServerUserVerif.UseVisualStyleBackColor = true;
            chckServerUserVerif.CheckedChanged += ChckServerUserVerif_CheckedChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label26);
            groupBox4.Controls.Add(txtServerAutoKick);
            groupBox4.Controls.Add(label25);
            groupBox4.Controls.Add(txtServerAutosaveSlots);
            groupBox4.Controls.Add(label23);
            groupBox4.Controls.Add(label22);
            groupBox4.Controls.Add(txtServerAutosaveIntervals);
            groupBox4.Controls.Add(label21);
            groupBox4.Controls.Add(chckServerOnlyAdminsPause);
            groupBox4.Controls.Add(txtServerMaxPlayers);
            groupBox4.Controls.Add(label24);
            groupBox4.Controls.Add(chckServerAutopausOnJoin);
            groupBox4.Controls.Add(chckServerAutoPauseWhenEmpty);
            groupBox4.Controls.Add(groupBox5);
            groupBox4.Controls.Add(txtServerGamePassword);
            groupBox4.Controls.Add(label13);
            groupBox4.Location = new Point(6, 6);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(404, 337);
            groupBox4.TabIndex = 7;
            groupBox4.TabStop = false;
            groupBox4.Text = "Game Settings";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(286, 314);
            label26.Name = "label26";
            label26.Size = new Size(50, 15);
            label26.TabIndex = 21;
            label26.Text = "minutes";
            // 
            // txtServerAutoKick
            // 
            txtServerAutoKick.Location = new Point(156, 310);
            txtServerAutoKick.Name = "txtServerAutoKick";
            txtServerAutoKick.Size = new Size(124, 23);
            txtServerAutoKick.TabIndex = 19;
            txtServerAutoKick.TextChanged += TxtServerAutoKick_TextChanged;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(6, 314);
            label25.Name = "label25";
            label25.Size = new Size(144, 15);
            label25.TabIndex = 20;
            label25.Text = "AFK Auto-Kick (0 = never)";
            // 
            // txtServerAutosaveSlots
            // 
            txtServerAutosaveSlots.Location = new Point(115, 281);
            txtServerAutosaveSlots.Name = "txtServerAutosaveSlots";
            txtServerAutosaveSlots.Size = new Size(165, 23);
            txtServerAutosaveSlots.TabIndex = 14;
            txtServerAutosaveSlots.TextChanged += TxtServerAutosaveSlots_TextChanged;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(6, 284);
            label23.Name = "label23";
            label23.Size = new Size(84, 15);
            label23.TabIndex = 15;
            label23.Text = "Autosave Slots";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(286, 255);
            label22.Name = "label22";
            label22.Size = new Size(50, 15);
            label22.TabIndex = 13;
            label22.Text = "minutes";
            // 
            // txtServerAutosaveIntervals
            // 
            txtServerAutosaveIntervals.Location = new Point(115, 252);
            txtServerAutosaveIntervals.Name = "txtServerAutosaveIntervals";
            txtServerAutosaveIntervals.Size = new Size(165, 23);
            txtServerAutosaveIntervals.TabIndex = 8;
            txtServerAutosaveIntervals.TextChanged += TxtServerAutosaveIntervals_TextChanged;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(6, 255);
            label21.Name = "label21";
            label21.Size = new Size(103, 15);
            label21.TabIndex = 9;
            label21.Text = "Autosave Intervals";
            // 
            // chckServerOnlyAdminsPause
            // 
            chckServerOnlyAdminsPause.AutoSize = true;
            chckServerOnlyAdminsPause.Location = new Point(212, 162);
            chckServerOnlyAdminsPause.Name = "chckServerOnlyAdminsPause";
            chckServerOnlyAdminsPause.Size = new Size(149, 19);
            chckServerOnlyAdminsPause.TabIndex = 22;
            chckServerOnlyAdminsPause.Text = "Only admins can pause";
            chckServerOnlyAdminsPause.UseVisualStyleBackColor = true;
            chckServerOnlyAdminsPause.CheckedChanged += ChckServerOnlyAdminsPause_CheckedChanged;
            // 
            // txtServerMaxPlayers
            // 
            txtServerMaxPlayers.Location = new Point(103, 81);
            txtServerMaxPlayers.Name = "txtServerMaxPlayers";
            txtServerMaxPlayers.Size = new Size(177, 23);
            txtServerMaxPlayers.TabIndex = 17;
            txtServerMaxPlayers.Visible = false;
            txtServerMaxPlayers.TextChanged += TxtServerMaxPlayers_TextChanged;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(6, 84);
            label24.Name = "label24";
            label24.Size = new Size(70, 15);
            label24.TabIndex = 18;
            label24.Text = "Max Players";
            label24.Visible = false;
            // 
            // chckServerAutopausOnJoin
            // 
            chckServerAutopausOnJoin.AutoSize = true;
            chckServerAutopausOnJoin.Location = new Point(6, 50);
            chckServerAutopausOnJoin.Name = "chckServerAutopausOnJoin";
            chckServerAutopausOnJoin.Size = new Size(237, 19);
            chckServerAutopausOnJoin.TabIndex = 16;
            chckServerAutopausOnJoin.Text = "Autopause when players are connecting";
            chckServerAutopausOnJoin.UseVisualStyleBackColor = true;
            chckServerAutopausOnJoin.CheckedChanged += ChckServerAutopausOnJoin_CheckedChanged;
            // 
            // chckServerAutoPauseWhenEmpty
            // 
            chckServerAutoPauseWhenEmpty.AutoSize = true;
            chckServerAutoPauseWhenEmpty.Location = new Point(6, 25);
            chckServerAutoPauseWhenEmpty.Name = "chckServerAutoPauseWhenEmpty";
            chckServerAutoPauseWhenEmpty.Size = new Size(233, 19);
            chckServerAutoPauseWhenEmpty.TabIndex = 12;
            chckServerAutoPauseWhenEmpty.Text = "Autopause when no players are present";
            chckServerAutoPauseWhenEmpty.UseVisualStyleBackColor = true;
            chckServerAutoPauseWhenEmpty.CheckedChanged += ChckServerAutoPauseWhenEmpty_CheckedChanged;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rdoServerCommandsAdmins);
            groupBox5.Controls.Add(rdoServerCommandsFalse);
            groupBox5.Controls.Add(rdoServerCommandsTrue);
            groupBox5.Location = new Point(6, 141);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(200, 100);
            groupBox5.TabIndex = 11;
            groupBox5.TabStop = false;
            groupBox5.Text = "Allow Commands";
            // 
            // rdoServerCommandsAdmins
            // 
            rdoServerCommandsAdmins.AutoSize = true;
            rdoServerCommandsAdmins.Location = new Point(6, 22);
            rdoServerCommandsAdmins.Name = "rdoServerCommandsAdmins";
            rdoServerCommandsAdmins.Size = new Size(94, 19);
            rdoServerCommandsAdmins.TabIndex = 2;
            rdoServerCommandsAdmins.TabStop = true;
            rdoServerCommandsAdmins.Text = "Admins Only";
            rdoServerCommandsAdmins.UseVisualStyleBackColor = true;
            rdoServerCommandsAdmins.CheckedChanged += RdoServerCommandsAdmins_CheckedChanged;
            // 
            // rdoServerCommandsFalse
            // 
            rdoServerCommandsFalse.AutoSize = true;
            rdoServerCommandsFalse.Location = new Point(6, 72);
            rdoServerCommandsFalse.Name = "rdoServerCommandsFalse";
            rdoServerCommandsFalse.Size = new Size(68, 19);
            rdoServerCommandsFalse.TabIndex = 1;
            rdoServerCommandsFalse.TabStop = true;
            rdoServerCommandsFalse.Text = "Nobody";
            rdoServerCommandsFalse.UseVisualStyleBackColor = true;
            rdoServerCommandsFalse.CheckedChanged += RdoServerCommandsFalse_CheckedChanged;
            // 
            // rdoServerCommandsTrue
            // 
            rdoServerCommandsTrue.AutoSize = true;
            rdoServerCommandsTrue.Location = new Point(6, 47);
            rdoServerCommandsTrue.Name = "rdoServerCommandsTrue";
            rdoServerCommandsTrue.Size = new Size(73, 19);
            rdoServerCommandsTrue.TabIndex = 0;
            rdoServerCommandsTrue.TabStop = true;
            rdoServerCommandsTrue.Text = "Everyone";
            rdoServerCommandsTrue.UseVisualStyleBackColor = true;
            rdoServerCommandsTrue.CheckedChanged += RdoServerCommandsTrue_CheckedChanged;
            // 
            // txtServerGamePassword
            // 
            txtServerGamePassword.Location = new Point(103, 112);
            txtServerGamePassword.Name = "txtServerGamePassword";
            txtServerGamePassword.Size = new Size(177, 23);
            txtServerGamePassword.TabIndex = 8;
            txtServerGamePassword.Visible = false;
            txtServerGamePassword.TextChanged += TxtServerGamePassword_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 115);
            label13.Name = "label13";
            label13.Size = new Size(91, 15);
            label13.TabIndex = 9;
            label13.Text = "Game Password";
            label13.Visible = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(gb);
            groupBox3.Controls.Add(chckServerLan);
            groupBox3.Controls.Add(chckServerPublic);
            groupBox3.Location = new Point(422, 118);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(384, 87);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Visibility";
            // 
            // gb
            // 
            gb.Controls.Add(lblServerUsername);
            gb.Controls.Add(txtServerPassword);
            gb.Controls.Add(lblServerPassword);
            gb.Controls.Add(txtServerUsername);
            gb.Location = new Point(71, 13);
            gb.Name = "gb";
            gb.Size = new Size(307, 68);
            gb.TabIndex = 8;
            gb.TabStop = false;
            gb.Text = "Factorio.com Login (If Public)";
            // 
            // lblServerUsername
            // 
            lblServerUsername.AutoSize = true;
            lblServerUsername.Location = new Point(15, 19);
            lblServerUsername.Name = "lblServerUsername";
            lblServerUsername.Size = new Size(60, 15);
            lblServerUsername.TabIndex = 6;
            lblServerUsername.Text = "Username";
            // 
            // txtServerPassword
            // 
            txtServerPassword.Location = new Point(78, 40);
            txtServerPassword.Name = "txtServerPassword";
            txtServerPassword.PasswordChar = '*';
            txtServerPassword.Size = new Size(171, 23);
            txtServerPassword.TabIndex = 7;
            txtServerPassword.TextChanged += TxtServerPassword_TextChanged;
            // 
            // lblServerPassword
            // 
            lblServerPassword.AutoSize = true;
            lblServerPassword.Location = new Point(15, 43);
            lblServerPassword.Name = "lblServerPassword";
            lblServerPassword.Size = new Size(57, 15);
            lblServerPassword.TabIndex = 6;
            lblServerPassword.Text = "Password";
            // 
            // txtServerUsername
            // 
            txtServerUsername.Location = new Point(78, 16);
            txtServerUsername.Name = "txtServerUsername";
            txtServerUsername.Size = new Size(171, 23);
            txtServerUsername.TabIndex = 6;
            txtServerUsername.TextChanged += TxtServerUsername_TextChanged;
            // 
            // chckServerLan
            // 
            chckServerLan.AutoSize = true;
            chckServerLan.Location = new Point(6, 47);
            chckServerLan.Name = "chckServerLan";
            chckServerLan.Size = new Size(45, 19);
            chckServerLan.TabIndex = 1;
            chckServerLan.Text = "Lan";
            chckServerLan.UseVisualStyleBackColor = true;
            chckServerLan.CheckedChanged += ChckServerLan_CheckedChanged;
            // 
            // chckServerPublic
            // 
            chckServerPublic.AutoSize = true;
            chckServerPublic.Location = new Point(6, 22);
            chckServerPublic.Name = "chckServerPublic";
            chckServerPublic.Size = new Size(59, 19);
            chckServerPublic.TabIndex = 0;
            chckServerPublic.Text = "Public";
            chckServerPublic.UseVisualStyleBackColor = true;
            chckServerPublic.CheckedChanged += ChckServerPublic_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtServerTags);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(txtServerDescription);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(txtServerName);
            groupBox2.Controls.Add(label10);
            groupBox2.Location = new Point(422, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(384, 109);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "General";
            // 
            // txtServerTags
            // 
            txtServerTags.Location = new Point(111, 76);
            txtServerTags.Name = "txtServerTags";
            txtServerTags.Size = new Size(267, 23);
            txtServerTags.TabIndex = 5;
            txtServerTags.TextChanged += TxtServerTags_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 79);
            label12.Name = "label12";
            label12.Size = new Size(30, 15);
            label12.TabIndex = 4;
            label12.Text = "Tags";
            // 
            // txtServerDescription
            // 
            txtServerDescription.Location = new Point(111, 49);
            txtServerDescription.Name = "txtServerDescription";
            txtServerDescription.Size = new Size(267, 23);
            txtServerDescription.TabIndex = 3;
            txtServerDescription.TextChanged += TxtServerDescription_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 52);
            label11.Name = "label11";
            label11.Size = new Size(102, 15);
            label11.TabIndex = 2;
            label11.Text = "Server Description";
            // 
            // txtServerName
            // 
            txtServerName.Location = new Point(111, 20);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(267, 23);
            txtServerName.TabIndex = 1;
            txtServerName.TextChanged += TxtServerName_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 23);
            label10.Name = "label10";
            label10.Size = new Size(74, 15);
            label10.TabIndex = 0;
            label10.Text = "Server Name";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 487);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Factorio Server Launcher";
            ((System.ComponentModel.ISupportInitialize)numCheckCurrent).EndInit();
            gbUpdateTrough.ResumeLayout(false);
            gbUpdateTrough.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            tabPage1.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            gb.ResumeLayout(false);
            gb.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtInstalled;
        private TextBox txtCurrent;
        private Label label1;
        private Label label2;
        private RichTextBox txtConsole;
        private TextBox txtLocation;
        private Label label3;
        private Button btnLocation;
        private TextBox txtCommand;
        private Button btnStart;
        private Button btnCheck;
        private CheckBox chkStartOnLaunch;
        private Label label4;
        private NumericUpDown numCheckCurrent;
        private Label label5;
        private Label label6;
        private TextBox txtSave;
        private Button btnSave;
        private RadioButton rdoSteamCMD;
        private RadioButton rdoWebsite;
        private GroupBox gbUpdateTrough;
        private Button btnSelectSteam;
        private TextBox txtSteamCMDloc;
        private GroupBox groupBox1;
        private Label lblPassword;
        private Label label9;
        private TextBox txtToken;
        private TextBox txtUsername;
        private Button btnStopServer;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private GroupBox groupBox3;
        private TextBox txtServerUsername;
        private Label lblServerPassword;
        private Label lblServerUsername;
        private CheckBox chckServerLan;
        private CheckBox chckServerPublic;
        private GroupBox groupBox2;
        private TextBox txtServerTags;
        private Label label12;
        private TextBox txtServerDescription;
        private Label label11;
        private TextBox txtServerName;
        private Label label10;
        private TextBox txtServerPassword;
        private GroupBox groupBox4;
        private CheckBox chckServerUserVerif;
        private TextBox txtServerGamePassword;
        private Label label13;
        private GroupBox groupBox5;
        private RadioButton rdoServerCommandsAdmins;
        private RadioButton rdoServerCommandsFalse;
        private RadioButton rdoServerCommandsTrue;
        private GroupBox groupBox6;
        private TextBox txtServerMaxUploadSlots;
        private Label label16;
        private Label label15;
        private TextBox txtServerMaxUploadRate;
        private Label label14;
        private CheckBox chckIgnorePlayerLimit;
        private Label label20;
        private Label label19;
        private TextBox txtServerMaxHeartbeats;
        private Label label18;
        private TextBox txtServerMinTicksLatency;
        private Label label17;
        private CheckBox chckServerNonBlockingSaves;
        private CheckBox chckServerAutoSaveOnServer;
        private CheckBox chckServerAutoPauseWhenEmpty;
        private TextBox txtServerAutosaveSlots;
        private Label label23;
        private Label label22;
        private TextBox txtServerAutosaveIntervals;
        private Label label21;
        private CheckBox chckServerAutopausOnJoin;
        private Button btnServerRestoreBackup;
        private Button btnServerBackupSettings;
        private Button btnServerSaveSettings;
        private TextBox txtServerMaxPlayers;
        private Label label24;
        private TextBox txtServerAutoKick;
        private Label label25;
        private Label label26;
        private CheckBox chckServerOnlyAdminsPause;
        private GroupBox gb;
        private Label lblToken;
        private CheckBox chckSpaceAge;
        private Label label8;
        private Button btnGenerateSave;
        private Label label7;
        private TextBox txtMapString;
        private GroupBox groupBox7;
        private Button btnSendCommand;
    }
}
