# Gorillaz With Bananaz

**Gorillaz With Bananaz** is a fast-paced, top-down Action Roguelite / Reverse Bullet Hell game developed for mobile platforms. Play as a magically gifted gorilla wielding enchanted bananas to fend off endless swarms of human captors in a high-stakes experiment orchestrated by hyper-intelligent space apes.

## 🦍 Game Overview
In this "Bullet Heaven" experience, players navigate a series of increasingly hostile arenas. The game blends absurd humor with tight, strategic arcade gameplay, focusing on survival through character builds and primal agility.

* **Platform:** Mobile (Currently on iOS)
* **Engine:** Unity 6 (URP)
* **Genre:** Action Roguelite / Reverse Bullet Hell

## 🚀 Key Gameplay Features

* **Hybrid Combat System:** Features traditional "auto-battler" mechanics combined with **Active-Targeting Skills**, requiring player input for high-impact strikes.
* **The "Keep-2" Progression:** At the end of each level, your arsenal is purged. You must strategically choose exactly **two skills** to carry over to the next biome, ensuring a fresh meta-game for every stage.
* **Boss Stalling (Risk vs. Reward):** Once a boss spawns, you aren't forced to kill it immediately. You can stall to farm more XP, but the boss gains stacking lethal buffs every minute.
* **Curated Draft Pool:** Level up to choose from a randomized pool of 11 total skills (7 General, 3 Unique, and 1 Wild card).
* **Strict Loadout Constraints:** Players are limited to 5 active slots—3 for magical weaponry and 2 dedicated to "Player Skills" (innate primate abilities), preventing over-reliance on projectiles.

## 🛠️ Technical Implementation

### Mobile & UI Optimization
* **Floating Joystick:** A dynamic "Mobile Touch Zone" system that appears at the point of contact, ensuring a clear field of view and preventing UI fatigue.
* **Smart UI Interactivity:** Automatic disabling of joystick input during "Level Up" menus to prevent Raycast conflicts and movement bugs.
* **iOS Deployment:** Full Xcode integration with managed signing and Bundle Identifier optimization for rapid prototyping on physical devices.

### Movement & AI
* **Humanoid Animation Rigging:** Physics-based 3D Gorilla character utilizing Unity’s Humanoid rig system with optimized "Bake Into Pose" animation clips to prevent axis drift.
* **Isometric Camera Follow:** A custom camera script featuring adjustable offsets, smooth target tracking, and a **Screen Shake** system for high-impact combat feedback.
* **Rigidbody Physics:** Movement is handled via velocity-based acceleration and Slerp-based rotation for a responsive, weightful feel.

### Level Generation & Director
* **Procedural Content:** Levels utilize Cellular Automata for layout generation with Flood-Fill algorithms to ensure map connectivity.
* **Game Director:** A background system that monitors survival time to dynamically scale enemy health, speed, and spawn frequency.

## ⚙️ Tech Stack
* **Unity 6:** Core engine and rendering.
* **Universal Render Pipeline (URP):** For optimized mobile performance and atmospheric lighting.
* **C#:** Logic, AI, and system architecture.
* **Mixamo:** Humanoid character animations.
* **Xcode:** iOS deployment and device testing.

## 📱 How to Build
1. Clone the repository to your PC.
2. Open the project in **Unity Hub** (Unity 6 required).

## 👥 The Team
* **Poyraz Akyol** - Lead Game Developer / Computer Engineer
* **Mehmet Levent Postalcıoğlu** - Game Designer / Developer

---
*Developed as part of the Hacettepe University Game Technologies MSc program - Mobile Game Development Course.*
