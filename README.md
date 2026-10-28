# JoinFix

A [BepInEx 5](https://github.com/BepInEx/BepInEx) plugin for **Stick Fight: The Game** that fixes the "No connection to the host could be made, please check firewall" error when joining a lobby.

The game gives up after 5 seconds if the host hasn't accepted you yet, and setting up the Steam connection (especially through Steam's relays) often takes longer. Pausing while joining only "fixed" it because those timers count game time. This mod waits up to 30 seconds instead (config: `JoinTimeout`). Only the joining player needs it.

## Build

Requires the .NET SDK and a copy of the game with BepInEx 5 installed (none of the game files are included):

```sh
dotnet build -c Release -p:GameDir=/path/to/StickFightTheGame/
```

The built DLL is copied into that install's `BepInEx/plugins` folder.
