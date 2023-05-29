# PC Well
This project is a proposal of educative game which tries to help young people to understand the dangers of the internet, detect and prevent scams, and protect properly their PC. It has a 3D environment simulating the computer with 3 data folders that must be protected and a couple of "defenders" that simulate to be the computer antivirus.
As time passes, popups will appear at the bottom of the screen. These popups will constitute 2D mini-games in which the player must determine if the popup page that has just opened is a malicious or beneficial website. If you complete the test well you will receive points that will allow you to buy more defenders, but if you fail or do not complete the minigame within its time limit you will receive a virus attack.
## Content

These are the folders you will find in this repository.
### General Game

Core scripts of the game. Here are the scripts that control the 3D environment, camera movement, spawning enemies, and creating 2D minigames in the 3D environment.

The enemy spawners are in the Enemies subfolder, as their spawn is a bit more complex than the other main systems.
### MainMenu
Scripts that manage the main menu.
### Minigames

The scripts that handle each of the minigames. The different types of minigames are:

- Email: These are minigames in which you have to read the text of the email and determine if it is spam or a legal email. If they are spam you have to close them, but if they are good emails you have to answer them.

- Roulete: The typical malicious popup. To complete it, you have to close the popup without throwing the wheel.

- Login: From time to time the popup will be a web page that will ask us for identification (the password is assigned when starting the game). The player must determine if the page is legal, in which case the password must be entered, or if it is malicious, it must be closed.
- Connect dots: It is the most offtopic minigame of the project. You have to connect the points on the left side of the screen with those on the right that correspond in color.

### UI
Scripts that control the user interface.