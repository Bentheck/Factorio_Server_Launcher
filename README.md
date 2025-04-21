# Factorio Server Launcher

A simple Windows tool to host your own Factorio server **without** having to use SteamCMD.  

Factorio Server Launcher uses your Factorio website credentials to download and install the standalone server binaries, then provides a graphical interface to manage server settings and control.

---

## Features

- 🚀 **Easy setup** – no SteamCMD required  
- 🔑 **Credential-based download** – uses your Factorio account to fetch server files  
- 🖥️ **Windows Forms UI** – configure map, difficulty, mods, admins, and more  
- 🔄 **Auto-update** – checks and installs new Factorio versions  
- ⏰ **Scheduled restarts & saves** – automated countdowns and warnings (only when a there is a new update) 

---

## Prerequisites

- [.NET 8.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0) **or** [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- Windows 10 or later  
- A valid Factorio account (to download the server files) 

---

## Usage

1. **Download** latest release
2. **Run** `Facto.exe`.  
3. **Log in** using your Factorio.com email & API token.  
4. The launcher will:
   - Download and install/update Factorio headless server.
   - Launch the server with your selected save file.
5. **Navigate** through the tabs to:
   - **Settings:** map seed, game difficulty, player limits, autosaves.
   - **Control:** start/stop server, manual saves, restart countdown.
   - **Logs:** view real-time server console output.

---

## Troubleshooting

- **Download failures** – verify your Factorio credentials and network access.  
- **Port conflicts** – default server port is `34197`; change in `server-settings.json`.  
- **Log files** – view `logs/launcher.log` and `logs/factorio-current.log` for errors.  
- **Missing .NET runtime** – ensure the .NET 8.0 Desktop Runtime is installed.

---

## Contributing

1. Fork the repo  
2. Create a branch `feature/your-feature`  
3. Commit your changes `git commit -m "Add feature"`  
4. Push `git push origin feature/your-feature`  
5. Open a Pull Request  

---

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for details.
