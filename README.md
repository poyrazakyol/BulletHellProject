# Gorillaz with Bananaz 🦍🍌

A top-down, action-packed "Bullet Hell" and "Horde Survival" game (similar to Vampire Survivors and Brotato) where players must survive against waves of oncoming enemies, leveling up and upgrading their abilities by collecting experience points (XP).

## 🌟 Features

### ⚔️ Combat & Survival Dynamics
* **Auto-Shooter System:** Automatically detects and fires at the nearest enemy within the player's range (`AutoShooter` system).
* **Dynamic Enemy AI:**
  * **Melee Swarm:** Uses NavMesh to track the player, relentlessly rushing towards them and dealing damage upon contact. Features custom Mixamo-based humanoid animations.
  * **Ranged Units:** Tactical enemies that maintain a specific distance, aim at the player, and fire projectiles.
* **Advanced Damage Feedback:** Screen shake (Camera Shake) upon taking damage, real-time health bar updates, and hit reactions accompanied by custom SFX.

### 🧬 Level Up & Progression System
* **XP Gem System:** Small (Blue), Medium (Green), and Large (Red) experience gems that drop from defeated enemies based on chance.
* **Random Skill Selection (Roguelite Elements):** Pauses the game upon leveling up and offers the player 3 random upgrade choices from the pool:
  * **Improve Fire Rate:** Increases attack speed.
  * **Move Speed:** Increases the character's movement speed.
  * **Unlock Orbit Weapon:** Activates an orbital weapon rotating around the character, damaging nearby enemies.
  * **Unlock Poison Pool:** Spawns toxic pools on the ground that deal damage over time to enemies.
  * **Max Health:** Increases the total health pool and dynamically extends the health bar UI.

### 🎮 Cross-Platform Controls (Mobile & PC)
* **Dynamic Virtual Joystick:** A custom mobile joystick (`MobileJoystick`) that appears where the player touches the screen, equipped with multi-touch protection.
* **PC Controls:** Full mouse and keyboard integration for seamless testing and desktop gameplay.

### 🎵 Audio Engine (Singleton Sound Manager)
* A centralized sound management system that persists across scenes without interruption, separating background music (BGM) and sound effects (SFX) into independent channels.
* **Polyphonic SFX:** Allows multiple overlapping sounds—such as attacks, taking damage, UI clicks, and leveling up—to play simultaneously without cutting each other off.
* **Mute Toggle:** Fully functional UI button to mute/unmute all game audio instantly.

### 💻 User Interface (UI)
* **Responsive Layouts:** Carefully anchored UI panels that maintain their proportions and alignment perfectly across various resolutions (different smartphone screens and monitors).
* **High-Definition Fonts:** Custom fonts processed and baked specifically for TextMeshPro (TMP) to ensure crystal-clear text readability.

## 🛠️ Tech Stack & Tools
* **Game Engine:** Unity 6 (Universal Render Pipeline - URP)
* **AI & Pathfinding:** Unity NavMesh Agent
* **Animation:** Mixamo (Humanoid Rigging, Avatar Configurations)
* **UI Framework:** TextMeshPro (TMP), Canvas UI

## 📂 Core Script Architecture
* `PlayerMovement.cs` / `MobileJoystick.cs`: Manages character movement and mobile touch inputs.
* `PlayerHealth.cs`: Handles player health, damage registration, and Game Over states.
* `AutoShooter.cs`: Controls enemy detection and automated weapon firing.
* `EnemyAI.cs` / `RangedEnemyAI.cs`: Manages enemy decision trees, pathfinding, and randomized XP drop rates.
* `LevelUpManager.cs`: Monitors XP progression, handles game pausing, and selects random perks from the upgrade pool.
* `SoundManager.cs`: A Singleton-pattern controller managing all audio channels and clips.

## 🚀 Installation & Setup
1. Clone the repository.
2. Open the project using **Unity 6** or a newer version.
3. Open the `MainMenu` scene from the Hierarchy window.
4. Press the `Play` button to start surviving!
