# Fatum
Fatum is the biggest and long-term project I have. It is a very minimalist and strange exploration and adventure game that seeks to immerse players in a mystical narrative. You can read more about the project [here](http://https://drive.google.com/file/d/1GJWa4fgjm6HMowvnrY-iCx_DCtIDYLtH/view?usp=sharing "here").

### Content
These are the folders you will find in this repository.

###### Nota:
This README will be much longer than the others since the project is more complex and requires extra information to fully understand all the systems that make it up.

#### Enemy
In this folder are the scripts that are responsible for managing all types of enemies in the project
##### - Normal
Here are the game's standard enemy routines, as well as their viewpoint manager and attack system.
##### - Bosses
The game bosses, their scripts and routines are stored in this folder.
####Environment
All the scripts that allow you to interact with the environment are in this folder
##### - EnvItem
Here are the scripts of the objects that can be collected throughout the adventure. These items interact with the inventory system which is explained below.
#### Inventory
This is the most complex system in Fatum, and the entire game design is based on it. It's built using a facade model that communicates the skill database with the inventory viewport itself and the player class.

It is made up of an inventory manager, which is in charge of acting as a mediator, and different inventory slots. The player will be able to use the inventory to equip the items that will grant it abilities. Once it does, the inventory manager will ask the skill manager what skill the object is looking for is associated with and will pass it on to the player so that they can use it from that moment on.

When a skill is to be used, the player asks the inventory manager what that skill does. This asks the skill manager how the skill works and activates it for the player.

#### Managers

Here are the Game Manager, the Music Manager and the Resources Manager of the project.

####  Menu

In this folder are the scripts that manage the main menu and the pause menu.

#### Player

Here are the scripts that control the movement of the player, his resources and the camera.

#### Puzzles

In this folder are the scripts that manage the logic of the puzzles. All the puzzles consist of pressing pressure plates and having the map altered by moving walls or siege columns to reveal new paths and block others. There are different managers depending on what the puzzle needs.

#### SaveData

Here is the project save system. It consists of a .txt file generator that generates a txt in the PC's persistent datapath if it doesn't find save files, and overwrites the data if it already exists and we want to save. It is also responsible for loading the saved game data. Currently this system only allows saving a game.

#### Skills
This folder contains the skill class, the database with all the possible skills of the project, the skill manager, which is in contact with the inventory manager and is in charge of managing the skills, and the skill logic, which contains the abilities behaviour.




