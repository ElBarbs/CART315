# Design Journal - Louis Barbier

## Enter the Gungeon - Game Analysis

### Mechanics

- Lots of different guns (243) and passive/active items (271).
  - 1410 synergies possible between guns and items.
  - 5 shooting types for guns:
    - Automatic
    - Semi-automatic
    - Charged
    - Beam
    - Burst
- Some items will increase your curse stat, which makes the enemies stronger in exchange for better loot.
- Ammo crates only refill one of your guns, so you have to choose which wisely.
- Incremental unlocks for new characters, guns, items, NPCs, etc.
- The layout of the different floors are randomly generated at the start of each run.
  - Teleporters are available to move between rooms on the same floor.
  - Each floor has an alternate secret version, which is harder but has better loot.
  - Each floor, you get a limited number of “blanks” that can be used to clear all bullets on the screen.
- An elevator can be unlocked and upgraded to skip the first floors when starting a new run.
- Special reward when killing the boss of a floor without taking damage.

### Ideas for Future Projects

- Synergies between items.
- Limited quantity of ammo.
- Teleporters.
- Different shooting types.
- "Curse" stat.

## Shindig Tycoon - Prototype 1

### Description

**"Shindig Tycoon"** is an exhilarating single-player management simulation where you step into the shoes of the ultimate house party host. Your mission? To throw the most legendary parties in the heart of Partydale. As the mastermind behind these unforgettable gatherings, you're tasked with juggling a myriad of responsibilities to keep your guests ecstatic. From blasting the latest hits that keep the energy high, maintaining an impeccable environment, to mixing the wildest cocktails, every detail counts. Your objective is clear: create a buzz that makes your house the talk of the town while skillfully avoiding unwanted attention. Dodge the watchful eyes of your parents and steer clear of any reasons for the police to disrupt your epic celebration. Welcome to **"Shindig Tycoon,"** where the night is young, and the possibilities are endless.

### Game Features

#### Art Direction / Sprites

- Map
  - Multiple floors.
  - House gets bigger as you advance through the game.
- Character
  - Mutiple colors.
- Interactable Objects
  - DJ booth.
  - Toilet.
  - Fridge.
  - Etc.

#### Character AI

- AI Interactions with Minigames.
- AI Pathfinding.
- Villain Interactions
  - Police.
  - Neighbours.
  - Parents.

#### Game Manager

- HUD / UI
  - Number of attendees.
  - Time.
  - Event log.
- The Meters
  - Happiness.
  - Music.
  - Cleanliness.
  - Alcohol.

### Images

![Prototype 1 Screen](/Process/Images/CART315_Prototype01_Screen.jpg)
![Prototype 1 Cards](/Process/Images/CART315_Prototype01_Cards.jpg)
 
## Shindig Tycoon - Prototype 2

### Description

We are trying to figure out the look and feel of the game. We found assets together and I created the playable area while Noah worked on the HUD. I got to familiarize myself with Grid and Tilemap components, as well as how to import sprites and create a Tile Palette. The next step will be to work on the mechanics, more specifically on the AI pathfinding.

### Images

![Prototype 2 Screen](/Process/Images/CART315_Prototype02_Screen.png)

## Shindig Tycoon - Prototype 3

### Description

We added more furniture to the playable area and merged it with the HUD. I wrote a script to spawn NPCs and used the "AI Navigation" package to create a NavigationMesh that allows them to know where they are able to go in the playable area. For now, they don't have any walking animation. For next week, I need to find a way of making them move naturally, to interact with the environment and to express their emotions.

### Images

![Prototype 3 Screen](/Process/Images/CART315_Prototype03_Screen.png)
![Prototype 3 NavigationMesh](/Process/Images/CART315_Prototype03_NavigationMesh.png)

## Shindig Tycoon - Prototype 4

### Description

NPCs now follow each other if they are part of the same group of friends. They can also emote and vomit on the floor based on their drunkenness level. The meter representing the cleanliness of the house is affected by the amount of vomit on the floor and if it reaches 0, the game is over. I need to add a way of cleaning up the house and more interactions between the NPCs and the environment.

### Images

![Prototype 4 Pathfinding Demo](/Process/Images/CART315_Prototype04_PathfindingDemo.gif)
