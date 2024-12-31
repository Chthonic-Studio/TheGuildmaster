# The Guildmaster

## Overview
The Guildmaster is a game developed by Chthonic Studio. This repository contains all the source code and assets for the game.

## Table of Contents
- [Overview](#overview)
- [Project Structure](#project-structure)
- [Character System](#character-system)
- [Leveling System](#leveling-system)
- [AI](#ai)
- [Dependencies](#dependencies)
- [Building and Running](#building-and-running)
- [Contributing](#contributing)
- [License](#license)

## Overview

### The Guildmaster Game Design One Pager 

**Platform**: PC

**Target Audience**: Fans of Story Generators (RimWorld, Prison Architect, Kenshi).

**Game Mantra**: Guildmaster Simulator game about managing an adventurers' guild in a medieval fantasy world

**Design Pillars**:
- **Deep Guild Management**: Take control of every facet of your Adventurer's Guild. Recruit promising heroes, equip them for success, and assign them to conquer the Tower's monstrous floors. Strategize their approach, manage resources, and guide them through hundreds of branching events that shape your guild's destiny. But time is limited: can you unite your guild and overcome the Tower's threats before the world's fate is sealed?
- **Strategic Guidance, not Direct Control**: While you can't micromanage adventurers in the field, your leadership is crucial. Set their mentality (aggressive, cautious, etc.), select their equipment, and provide tactical direction. Witness the impact of your choices as your adventurers navigate perilous quests and conquer the Tower floor-by-floor.
- **A Living World**: Experience decades of simulated time, filled with triumphs, tragedies, and hilarious mishaps. The guild, town, and even your adventurers' lives progress realistically, creating a rich tapestry of stories that will stay with you long after the game ends.

**Premise**
In this unique medieval fantasy experience, lead the charge as a seasoned Elf Guildmaster. Though your centuries of experience lie beyond direct combat, your strategic mind remains sharp. Wield a new kind of power – leadership – to manage your Adventurer's Guild. Recruit promising heroes, hone their skills, and guide them on critical missions to conquer the Tower, a monstrous keep threatening the world's very existence. But time is not your friend – can you unite your guild and strategically overcome this looming threat before it's too late?

**Gameplay**
The player controls almost all aspects of the management of the Adventurer’s Guild: Recruitment of new adventurers, management of the Guild’s arks, control over the guild employees’ tasks, deciding the course of action over hundreds of possible events, guidance for the adventurers, management of the quests and the teams that take them, and command over the strategic efforts to conquer the Tower, a massive structure that threatens to end the world if not conquered before 500 in-game years passes. 

You can send adventurers on automatic missions, but you can also decide to guide them during important quests or Tower floors. During a mission, you can’t give direct orders to the adventurers, but you can assign them a mentality, select their gear, and provide general direction over where to go. 

The main goal of the game is to build a Guild strong enough to take on the Tower, one floor at a time, while also experiencing dozens of years of simulated history, giving space to hilarious stories and some total tearjerkers. The AI of the adventurers, guild employees, and even the citizens of the town have been implemented as realistic as possible, in a way that allows for a high degree of autonomy. 

**Features**
- Simulator-style gameplay
- Autobattler-style missions where you can only direct your units with general orders
- A complex AI that simulates real behavior over centuries of game history
- A fun but deep storytelling behind the backstory of the world
- Hundreds of events to keep the player engaged during the course of gameplay


## Project Structure
The project is organized into the following directories:

- **Assets/**: Contains all the game assets such as models, textures, and audio files.
- **Scripts/**: Contains all the C# scripts for game logic.
  - **Characters/**: Scripts related to character management and behavior.
  - **Leveling/**: Scripts related to the leveling system.
  - **Debugging/**: Scripts for debugging and testing of features of the game.
- **Shaders/**: Contains ShaderLab and HLSL files for custom shaders.
- **Prefabs/**: Contains prefab assets for game objects.
- **Scenes/**: Contains the Unity scenes for different levels or parts of the game.
- **SO/**: Contains all the Structured Objects used for all elements of the game.
- **UI/**: Scripts for user interface elements.

## Character System
The character system handles all aspects of character creation, management, and behavior in the game.

### Main Components
- **Characters/**: Folder containing all the scripts for characters and npcs, including name generation, variable selection, spawning, managers, housing, etc.
  - **/CharacterManager.cs**: Handles the instantiation and management of characters in the game.
  - **/CharacterProfile.cs**: Defines all variables of each character. This script is attached to the Character prefab.
  - **Housing/**: All elements related to assignment of housing ingame.
  - **NPCs/**: All elements related to handling, spawning and managing NPCs ingame.
 - **Sprite & Animation/**: Folder containing the scripts for sprite selection during unit spawn
 - **Stats/**: Folder containing the framework for the stats used ingame.

## Leveling System
The leveling system manages the experience points and leveling up mechanics for characters. 

### Main Components
- **/LevelingSystem.cs**: The main class for managing experience points and leveling mechanics. *NOT CREATED YET*
- **/ExperienceManager.cs**: Handles the distribution and tracking of experience points. *NOT CREATED YET*

## AI
The AI of the characters, npcs and enemies.

### Main Components
- **AStarPathfinding**: Found in folder The Guildmaster/Assets/AstarPathfindingProject

#### Character AI
Characters have a separate AI system while in town, to handle behavior while out of missions. We use a Utility AI system in the game.

##### Components
- **/CharacterTownAI.cs**: All calculations of town AI for characters
- **/UtilityAI.cs**: Framework of each Utility
- **/TownActions/**: Folder holding all possible actions that characters can do while in town, meant to be called from the AI of each character.

## Dependencies


## Building and Running


## Contributing


## License 
