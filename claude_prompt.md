# Prompt for Claude 3.5 Sonnet

Please generate a professional, structured PowerPoint presentation script/outline for our Master's class game development project. 

**Context:**
This is the "P2" (Progression 2) presentation for our Master's class (BCO605 Mobile Game Development). We previously had a P1 (Progression 1) pitch, and after this, we will have a FINAL presentation. The game has a dark humor theme, but since this is an academic presentation, tone it down to be professional and focused on game mechanics, technical achievements, and development progress. We are not marketing the game; we are showcasing our technical and design work to our professor.

**Game Details:**
*   **Name:** Gorillaz with Bananaz
*   **Genre:** Action Roguelite / Reverse Bullet Hell / Survivor Game
*   **Platform:** Mobile (iOS)
*   **Engine:** Unity 6 (URP)
*   **Theme:** Play as a magically gifted gorilla wielding enchanted bananas to fend off endless swarms of human captors in a high-stakes experiment. 

**Grading Criteria to Address (15 Slides Total):**
1.  **Game Introduction / Reminder (5%):** Briefly remind the audience what the game is.
2.  **Progress Since P1 (10%):** Detailed breakdown of advancements and work done.
3.  **The Big Showcase (70%):** Demos, drafts, code architecture, designs, 3D models, UI, VFX, audio, assets, and all outputs since P1.
4.  **Updated Development Plan to FINAL (5%):** Roadmap for the rest of the semester.
5.  **Presentation Quality & Participation (10%):** Ensure the presentation flow allows for both team members to speak and presents an engaging narrative.

**Team Members:**
*   Poyraz Akyol (Lead Game Developer / Computer Engineer)
*   Mehmet Levent Postalcıoğlu (Game Designer / Developer)

**Key Features & Technical Implementations to Highlight (Extracted from Codebase):**
*   **Mobile & UI:** Custom dynamic Floating Joystick (Mobile Touch Zone), smart UI interactivity (disables joystick during level-up screens), Main Menu, Stats Menu.
*   **Player & Combat:** 
    *   "Keep-2" Progression: Draft exactly 2 skills to carry over to the next biome.
    *   Strict Loadout Constraints: Max 5 slots (3 weapons, 2 primate abilities).
    *   Hybrid Combat: Auto-battler mechanics mixed with Active-Targeting Skills.
    *   Implemented Weapons: Orbital Bananas/Weapons, Poison Droppers, Auto-Shooters.
*   **Enemies & AI:**
    *   Dynamic Game Director that scales enemy health/speed/spawn based on survival time.
    *   Basic Melee AI and Ranged Enemy AI with projectiles.
    *   Boss Stalling Mechanic: Risk/reward system where bosses gain stacking buffs the longer you farm XP.
*   **Architecture & Performance:** 
    *   Heavy use of Object Pooling (Projectiles, XP Gems) for mobile optimization.
    *   Procedural Content Generation using Cellular Automata and Flood-Fill algorithms for dungeon generation.
    *   Map Chunk Management system for infinite/large arenas.
    *   Velocity-based Rigidbody physics for weightful gorilla movement.
*   **Visuals & 3D:** 
    *   3D Mixamo Gorilla rigged using Unity's Humanoid system (Bake Into Pose optimized).
    *   VFX Integration: Cartoon FX Remaster (CFXR) and Bloom post-processing via Universal Render Pipeline (URP).
    *   Isometric Camera with smooth follow and Screen Shake.

**Slide Structure Request:**
Please provide a 15-slide presentation outline. For each slide, include:
*   **Slide Title**
*   **Speaker:** (Alternate between Poyraz and Mehmet to show equal participation)
*   **Visual Suggestion:** (What should be on the screen—e.g., screenshot of code, video of gameplay, UI wireframe)
*   **Bullet Points:** (What appears on the slide)
*   **Speaker Notes:** (What the speaker should actually say, keeping it professional but engaging)

Make sure the slides are heavily weighted towards the "Big Showcase" (70% criteria) focusing on the technical implementations, code, models, and features added since P1!
