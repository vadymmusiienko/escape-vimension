# Game Design Document: Escape Vimension

## Game Overview

### Core Concept

In **Escape Vimension**, the player is a programmer who wants to write code faster with Vim. As a complete newbie, the player enters the Vim editor without knowing how to quit it. After spending hours trying to exit Vim, the player is shrunk down and trapped inside the "vimesion".

To escape back to the real world, you—the player—must master Vim, with the ultimate goal of learning how to exit and return to reality. The journey of learning Vim is not as easy as it may seem. Along the way, you discover that some programmers have never made it back.

Do you think you are built different? Can you master Vim? We will see…

---

### P.S. _What is Vim?_

Vim is a free, open-source, and highly configurable text editor that originated from the **vi** editor. Designed for efficiency and speed, it allows users to perform complex text-editing tasks entirely with keyboard commands through its command-line interface.

It is also known to have a steep learning curve, since most of us are used to relying on a mouse to move between lines. In Vim, you use the keyboard for everything!

---

### Related Genre(s)

-   **Primary Genre:** 3D Puzzle Platformer
-   **Secondary Genre:** Educational Game

**Similar Games:**

-   _Vim Adventures_ (2D browser game teaching Vim)
-   _Human Resource Machine_ (programming puzzle game)
-   _Superliminal_ (perspective/size-based puzzles)

**What Makes Us Different:**  
Unlike text-based Vim tutorials or 2D educational games, we create an immersive 3D environment where Vim commands have immediate, visible physical effects on the world. Our game also has an engaging plot and inside jokes. Learn Vim in a fun way!

---

### Target Audience

-   **Primary:** Computer science students (18–25) learning programming
-   **Secondary:** Developers wanting to learn Vim in a fun way
-   **Tertiary:** Puzzle game enthusiasts who enjoy unique mechanics

**Accessibility:** No prior Vim knowledge required—the game teaches from scratch.

---

### Unique Selling Points (USPs)

-   **Physical Vim Commands:** Transform abstract text editing into tangible 3D interactions
-   **Learn by Doing:** No boring tutorials; every command is learned through solving puzzles
-   **Terminal Aesthetic in 3D:** Unique visual style mixing retro terminal graphics with modern 3D environments

---

## Story and Narrative

### Backstory

You’re a college student working late on a programming project. Frustrated with your slow editing speed, you stumble upon Vim. Eager to improve, you open a terminal and type `vim .`.

Confused, you realize you have no idea how to edit code, navigate, or—most importantly—exit. In frustration, you mash through countless keyboard shortcuts, but nothing works… until you accidentally enter **Vim Tutor**. Suddenly, you’re shrunk down to the size of a microbe!

Now, the only way to return to reality is to master Vim—by following the tutor and hoping it finally teaches you how to quit.

---

### Setting

The game takes place in a fantasy world of miniatures that exists somewhere between the digital and physical worlds.

-   **Environment**

    -   Giant trees, tall grass, and oversized flowers creating natural mazes
    -   Fallen leaves, pebbles, and sticks acting as obstacles or platforms
    -   Streams, puddles, or small water hazards that must be crossed or avoided

-   **Enemies**

    -   Insects such as ants, cockroaches, spiders, or bees
    -   Mini-boss encounters: larger insects or rogue “viruses” that require command-based strategies to defeat

-   **Interactive Objects**

    -   Collectibles such as crumbs, gems, or code fragments that heal or grant new abilities
    -   Hidden shortcuts or “secret” paths that require clever use of Vim commands

---

### Characters

-   **The Player (You):** A tiny programmer trying to escape, shown as a small humanoid figure
-   **The Vim Tutor (Voice/Guide):** A mysterious creature that guides the player and teaches vim commands
-   **Lost Programmers:** Along the way, you encounter other programmers who failed to escape. They share their stories, warning that there may be no hope left

## Gameplay and Mechanics – Vim Code

### Player Perspective

The game uses a **top-down perspective**, similar to early versions of _The Legend of Zelda_ but in 3D. This allows the player to see more of the environment at once, which helps in solving puzzles.

---

### Controls

The player moves using **basic Vim commands**:

-   `h`, `j`, `k`, `l` → Move the player
-   `x` → Remove a simple object (cut it)
-   `d` → Attack or use a special power (unlocked as the game progresses)
-   `y` → "Paste": place an object from the inventory back into the world
-   `/` → Search: highlight hidden items on the map
-   `gg` → Instantly return to the game’s starting point

P.S. We will add more controls here once we finilize all abilities

---

### Progression

-   **Unlocked by default:** Movement only, plus command `x` to pick up or destroy simple obstacles.
-   **As the game progresses:** Additional abilities are unlocked; puzzles become more complex and require chaining multiple commands.
-   **Buyable commands:** Using coins, players can purchase numbers (1–9) to repeat a command multiple times, similar to Vim.

---

### Rules

-   **Health:** The player has a health bar with 10 health points. If all health is lost → **Respawn at the last checkpoint**.
-   **Target:** Progress through the world by collecting coins and other collectibles to unlock new commands and complete missions assigned by the Vim Tutor. Once all commands are unlocked, the player receives a final mission, after which they learn how to quit Vim and escape Vimension.

---

## World Design

_(Restrict gameplay to two axes but render in 3D → “2.5D” style)_

### Game World

The world is set in a fantasy village with big trees, grass etc.

### Objects

-
-   Terminals allowing player input
-   Enemies (viruses, bugs)
-   Collectibles for healing or unlocking new commands

### Physics

-   Player collides with enemies and obstacles
-   Movable objects may appear in puzzles

### Art Style
-   The overall aesthetics of the game will be more 3D cartoon-like rather than realistic. Since our player is shrunk into an imaginary Vim world, a more fantasy-like style suits our game design better.

-   [Overall game aesthetics](Images/FloatingIslands.png)
-   [Structures in the game](Images/Houses.png)

### Sound and Music
Since this game is an educational puzzle game, the music should be peaceful and calming, leaning toward a lofi style without any lyrics. This helps players stay focused on learning Vim while still creating a relaxed and enjoyable atmosphere.

-   Menu page: A mysterious soundtrack that signals the player’s transition into a fantasy world [Click here](https://freetouse.com/music/zambolino/old-world)

The game’s background is set in a fantasy village, and the accompanying soundtracks evoke the atmosphere of rural village life, complementing the overall theme and aesthetics.
-   Village music 1: [Click here](https://freetouse.com/music/pufino/village)
-   Village music 2: [Click here](https://freetouse.com/music/walen/medieval-village)
-   Village music 3: [Click here](https://freetouse.com/music/conquest/jester-dance)

Instead of using voice acting for the dialogues, we chose an immersive, lightweight, and flexible sound effect that better aligns with our puzzle/fantasy core. Therefore, typing sounds accompany the dialogues in our game design.
-   Typing sound for dialogues: [Click here](https://www.epidemicsound.com/sound-effects/tracks/11e80013-c1ae-4705-b557-8d8ff294d1ba/)

Since most of the paths in our world are covered with grass, interactions with the ground should produce forest-like, leaf-rustling sounds.
-   Pick up item: [Click here](https://www.epidemicsound.com/sound-effects/tracks/7b34345f-8e09-4b10-925a-39fb6fbfe457/)
-   Drop item: [Click here](https://www.epidemicsound.com/sound-effects/tracks/90e1ad5e-aaf4-4382-a5c9-368721d2733c/)
-   Walking: [Click here](https://www.epidemicsound.com/sound-effects/tracks/d5ca5d74-cdc1-4b0c-ba97-9314f36f7054/)
-   Running: [Click here](https://www.epidemicsound.com/sound-effects/tracks/324b3f4f-15c7-43c3-b6e7-80a178597e0c/)

### Assets

-   [The player](Images/Player.png) [3D Model](https://sketchfab.com/3d-models/walk-man-ca5429ebe74944c7949f05270e99b359)
-   [Vim Tutor](Images/VimTutor.png) [3D Model](https://sketchfab.com/3d-models/stylized-animated-octupus-afd522e7a5cb48bb98aea7c37879000d)
-   [Ghost NPC](Images/Ghost.png) [3D Model](https://sketchfab.com/3d-models/ghost-tv-man-f7bc9cc70236499fa09eb185728cf2e5)
-   [Coin Slug](Images/CoinSlug.png) [3D Model](https://sketchfab.com/3d-models/lowpoly-cute-monster-snail-three-animations-8bb433dfbef3479cbaa3bcdf63b5b6a2)
-   [Villager](Images/Villager.png) [3D Model](https://sketchfab.com/3d-models/new-grass-type-starter-pokemon-seedle-aeb918813f8b4de29bd48aa3bd35d6eb)
-   [Enemies](Images/Enemies.png) [3D Model](https://sketchfab.com/3d-models/spider-low-poly-1029c25a78c543259167cdc72a9ae7a7) [3D Model](https://sketchfab.com/3d-models/walking-ant-dcf15f34876e4a90bd0ad67ef55fb9fe)
-   [Health regen melon](Images/RegenerationMelon.png) [3D Model](https://sketchfab.com/3d-models/watermelon-bug-idle-f76e8cbeebcd4b1cae943ab7782ec22d)