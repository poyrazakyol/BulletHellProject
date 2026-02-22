# Top-Down 3D Roguelite Survivor

A Top-Down 3D Bullet Heaven / Survivor-like game developed in Unity, featuring algorithmic level generation and dynamic AI pathfinding. The game focuses on surviving endless waves of enemies in a procedurally generated dark fantasy dungeon.

## 🚀 Key Features

* **Procedural Dungeon Generation:** Levels are dynamically created at runtime using a **Cellular Automata** algorithm. A Custom **Flood Fill (BFS)** algorithm is implemented to detect and remove isolated rooms, ensuring a fully accessible play area.
* **Dynamic AI Navigation:** Enemies utilize Unity's AI Navigation package. The NavMesh is baked dynamically at runtime *after* the procedural dungeon is generated, allowing enemies to navigate complex, randomly generated corridors seamlessly.
* **Dynamic Difficulty Scaling:** A central Game Director actively monitors survival time, dynamically increasing enemy health, movement speed, and spawn rates to create an escalating "bullet hell" experience.
* **Optimized Object Pooling:** Zero garbage collection (GC) spikes during combat. All projectiles are managed through a custom Object Pool pattern, ensuring smooth frame rates even with hundreds of entities on screen.
* **Enemy Diversity:** Features multiple enemy archetypes, including standard melee chasers and ranged spitters that force the player to dodge and constantly reposition.
* **RNG Loot & Meta-Progression:** Enemies drop different tiers of XP gems (Small, Medium, Large) based on a weighted RNG system. The dynamic Level Up system pulls random abilities from a central pool, respecting maximum level caps for each upgrade (e.g., Fire Rate, Move Speed, Orbiting Weapons, AoE Poison Pools, Max Health).
* **Real-time Stats & UI:** Fully responsive UI built with Auto-Size TextMeshPro and Horizontal Layout Groups. Includes a dynamic health bar that physically scales with max health upgrades, and a Pause/Stats Menu to track real-time player attributes.
* **Game Feel & Polish:** Integrated custom screen shake mechanics, smooth Slerp/Lerp character movement, and particle effects (death bursts) to enhance the impact and weight of the combat.

## 🛠️ Technical Stack
* **Engine:** Unity 6
* **Language:** C#
* **Render Pipeline:** Universal Render Pipeline (URP) for optimized lighting and post-processing in a dark atmospheric setting.

## ⚙️ Core Mechanics (Currently Implemented)
- [x] Player Movement (Rigidbody Physics + Smooth Rotation)
- [x] Cellular Automata Dungeon Generator
- [x] Runtime NavMesh Baking
- [x] Enemy AI (Pathfinding & Ranged Attacks)
- [x] Dynamic Difficulty & Time Scaling
- [x] Object Pooling (Projectiles)
- [x] Auto-Shooter System with Range Detection
- [x] RNG Drop System (Multiple XP Gem Tiers)
- [x] Dynamic Upgrade Pool with Level Caps
- [x] Game Flow: Main Menu & Scene Management
- [x] UI/UX Integration (Scaling Health Bar, Dynamic Level Up Panel, Stats/Pause Menu)
- [x] Weapon Types: Directed Projectiles, Orbiting Blades, AoE Poison Pools

## 🎮 How to Play
1. Clone the repository.
2. Open the project in Unity.
3. Open the `MainMenu` scene.
4. Press **Play** and click "Play".
5. Use `W, A, S, D` to move. Press `TAB` to view your current stats. 
6. Avoid enemies, collect gems to level up, build your character, and survive as long as possible!
