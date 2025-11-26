# Project Overview

This repository contains the scripts and a progress document describing my progress, challenges encountered, and learnings.

To simplify testing, the game supports 2 players.  
It’s possible to add more players by simply changing the `minPlayersToStart` variable in `Scripts/SessionManagement/LobbyManager.cs`.

## Scripts Folder Structure:

### `Scripts/Enemy/`
Contains scripts that define the enemy AI's behavior, using the **Behavior Design package** (behavior tree) for decision-making. This folder also includes scripts that handle the enemy’s chainsaw hitting the player, resulting in the player's death, as well as scripts related to the enemy’s spawning position.

- **EnemyAIController**: The central controller that manages the AI's behavior using actions and conditions.
- **Actions/**: Scripts that define the actions the enemy can perform (e.g., attacking, following the player).
- **Conditions/**: Scripts that define the conditions that trigger actions (e.g., detecting the player, being within range).


### `Scripts/SessionManagement/`  
Contains scripts related to managing multiplayer sessions. This includes connecting multiple players to the level once there are enough players, and checking if a player loses connection, which results in them being returned to the menu.

### `Scripts/NormcoreModels/`  
Contains scripts that manage the syncing of elements, like enemy animations and the player’s flashlight state, across all players. It ensures that things like the enemy’s current animation or whether the flashlight is on or off stay consistent for all players during the game.

### `Scripts/Player/`  
Contains scripts related to toggling the flashlight (turning it on or off by pressing over the head) and ensuring that when the player is destroyed, the key is detached to prevent it from being destroyed with the player.

### `KeyAndHole/`  
Contains scripts that handle the key's interaction with the player and the keyhole. These include scripts that attach the key to the player’s 'belly' position, so it doesn’t have to be carried constantly, as well as scripts that manage the UI, indicating where the key should be placed. Additionally, the folder includes scripts that control the interaction between the key and the keyhole, allowing the hatch to be opened and the player to escape.
