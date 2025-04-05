# Prefab Scanner

**A BepInEx mod for Aska**  
Scans and logs all GameObjects (prefabs) in the active scene for debugging and modding purposes. Designed to help modders identify internal names and component structures of scene objects.
### This mod is a tool to help other modders. It is useless for normal players.

## DISCLAIMER

This mod is not endorsed by the developers of ASKA. You use it at your own risk. It is a debugging tool and may cause performance drops or strange behavior if misused.

## Features

- Logs the names and components of every GameObject in the active scene within 5 meters of the player
- Helps identify internal prefab names (e.g., "Harvest_Thatch_Reeds") for patching or replacement
- Lightweight and non-intrusive

## How It Works

When loaded into the world, the mod finds all `GameObject` instances within 5 meters of the player character, including 0 meters (i.e. objects on/in the player itself), and prints their names and component types to the console. This is especially useful for reverse-engineering in-game objects like resources, decorations, or interactables.

## Use Cases

- Identifying names of harvestable plants or objects
- Discovering internal components like `BiomeObject`, `Pickable`, or `NetworkInteractable`
- Locating hidden prefabs for patching or removal
- Diagnosing which objects are dynamically created or streamed in later

## Known Issues

- Scanning large scenes will produce a lot of console output—expect some lag on weaker systems
- Some dynamically created objects may not appear if scanning occurs too early
- This is a dev utility, not a gameplay mod—do not leave it enabled during normal play

## Requirements

- BepInEx 6 (BepInExPack for IL2CPP)
- ASKA (latest Early Access version)

## Installation

1. Go to https://builds.bepinex.dev/projects/bepinex_be  
2. Download the **BepInEx Unity (IL2CPP)** build for your OS. 
    * Note: It MUST be the IL2CPP version, NOT Mono.
3. Extract BepInEx into your ASKA directory (where `ASKA.exe` lives)  
4. Run the game once to generate BepInEx folders  
5. Extract `maoPrefabScanner.zip` into `BepInEx\plugins`  
6. Launch the game and load into a world  
7. Stand near the thing you want to scan, press F7 and  wait a few seconds
8. Alt-Tab to the BepInEx console 
9. Ctrl+A, Ctrl+C, Ctrl+V, Ctrl+F
10. ???????
11. Profit!

## License

Licensed under [Creative Commons Attribution 4.0 International](https://creativecommons.org/licenses/by/4.0/).  
**TL;DR:** *Do whatever you want, just give me full credit.*

---

Happy hacking,  
— Maoman
