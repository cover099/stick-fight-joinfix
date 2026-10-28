# JoinFix

A [BepInEx 5](https://github.com/BepInEx/BepInEx) plugin for **Stick Fight: The Game** that fixes the "No connection to the host could be made, please check firewall" error when joining a lobby.

The game gives up after 5 seconds if the host hasn't accepted you yet, and setting up the Steam connection (especially through Steam's relays) often takes longer. Pausing while joining only "fixed" it because those timers count game time. This mod waits up to 30 seconds instead (config: `JoinTimeout`). Only the joining player needs it. Works on Windows and Linux (Proton).

## Install

### Easy install (Windows)

1. Download `JoinFix-windows-installer.zip` from the [latest release](https://github.com/cover099/stick-fight-joinfix/releases/latest) and extract it anywhere.
2. Close the game, then double-click `Install.bat`.

It finds the game through Steam (or asks for the folder), installs BepInEx 5 if it isn't there yet, and puts the mod in place. If Windows shows "Windows protected your PC", click **More info** → **Run anyway**.

### Manual install

1. Install [BepInEx 5](https://github.com/BepInEx/BepInEx/releases) (`BepInEx_win_x64`) into the game folder.
2. Copy `JoinFix.dll` into `BepInEx/plugins`.

On Linux, set the game's Steam launch options to `WINEDLLOVERRIDES="winhttp=n,b" %command%` so BepInEx loads under Proton.

## Build

Requires the .NET SDK and a copy of the game with BepInEx 5 installed (none of the game files are included):

```sh
dotnet build -c Release -p:GameDir=/path/to/StickFightTheGame/
```

The built DLL is copied into that install's `BepInEx/plugins` folder.
