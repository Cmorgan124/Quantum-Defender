Hello and welcome to the repo of my first ever game: Quantum Defender.

A quantum-themed tower defense game built in Unity (C#), heavily inspired by one of my favorite games growing up: *Bloons Tower Defense*. 

Note: This project is actively under development with a playable demo coming soon. Future milestones include a full wave system, cash generator tower, colossal tower, and a boss atom. 

Core Gameplay Mechanics & Systems Design:

Meaningful Tower Progression & Visual Feedback--  
Player satisfaction is driven by highly rewarding tower upgrades. Every single upgrade is designed to fundamentally shift the tower's mechanical utility while providing immediate visual feedback to the player. I believe that visual upgrades are just as important as gameplay ones, so I made sure to give the towers new sprites for each upgrade.

Quantum Enemy Ecosystem & Counter-Play--  
The enemy types forces strategic, dynamic counter-play. Enemies are designed around specific scientific and physical properties, requiring exact elemental and structural counters:
Infrared Particles (Stealth Mechanics): Invisible to standard defenses. Players must build or upgrade towers to possess *Enhanced Vision* modules to detect and target them.
Shielded Water Molecules (AOE Counters): Guarded by a protective molecular shield that absorbs high single-target damage. Players must utilize *Area-of-Effect (AOE)* explosive or chain-damage towers to crack the shield and damage the internal structure.

Engineering:
To see my code go to Asset->Code->Scripts. I use a couple singletons to manage building towers and selecting them for upgrading/moving/selling. Enemies follow a path index to move throughout the map, and different types have different health/worth. Most of the towers use the turret script to do there tracking and shooting, while certain special towers like the freeze trap and power station have their own unique scripts. The tower data script tracks most things for the towers like kills, sell/move value, upgrade level, and range. The tower data script also holds the upgrade path for each tower, so each prefab level of that tower needs said values filled out. Its not the best method, but it works. The UI swaps between the shop menu and tower menu, and the tower menu accesses the tower data to correctly display the upgrades, name, and kills. The projectiles all use the same class, with specific upgrades having their own subclasses.

Sadly I don't have demo just yet, but here is some screenshots of some gameplay. All art was made by me using the website Piskel. The bottom button pad with all of the atoms faces is just a dev tool and will not be there in the final version. 

<img width="244" height="140" alt="Screenshot 2026-09-22 131536" src="https://github.com/user-attachments/assets/738b3b22-3174-4615-935f-f9628797c0e3" />

<img width="244" height="140" alt="Screenshot 2026-09-22 131250" src="https://github.com/user-attachments/assets/27fc3979-0999-4530-a5a8-9ac660504957" />

<img width="244" height="140" alt="Screenshot 2026-07-06 220903" src="https://github.com/user-attachments/assets/417a500f-0381-4f6f-b98b-46dcd11e85ed" />





