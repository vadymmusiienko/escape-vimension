# Project 2 Report

## Table of Contents

- [Project 2 Report](#project-2-report)
  - [Table of Contents](#table-of-contents)
  - [Evaluation Plan](#evaluation-plan)
  - [Evaluation Report](#evaluation-report)
    - [Cooperative Evaluation (By Zhunkun He)](#cooperative-evaluation-by-zhunkun-he)
      - [Purpose](#purpose)
      - [Participants](#participants)
      - [Evaluation Method](#evaluation-method)
    - [Findings and Observations](#findings-and-observations)
      - [Summary of Insights](#summary-of-insights)
      - [Impact on Development](#impact-on-development)
    - [Post-task Walkthrough (By Casey Watt-Calder)](#post-task-walkthrough-by-casey-watt-calder)
      - [Purpose](#purpose-1)
      - [Participants](#participants-1)
      - [Evaluation Method](#evaluation-method-1)
      - [Findings and Observations](#findings-and-observations-1)
        - [Participant 1 Observations During Playthrough](#participant-1-observations-during-playthrough)
        - [Participant 1 Reflections After Playthrough](#participant-1-reflections-after-playthrough)
        - [Participant 2 Observations During Playthrough](#participant-2-observations-during-playthrough)
        - [Participant 2 Reflections After Playthrough](#participant-2-reflections-after-playthrough)
      - [Summary of Insights](#summary-of-insights-1)
      - [Impact on Development](#impact-on-development-1)
    - [Interview (By Vadym Musiienko)](#interview-by-vadym-musiienko)
      - [Purpose](#purpose-2)
      - [Participants](#participants-2)
      - [Evaluation Method](#evaluation-method-2)
      - [Interview Questions](#interview-questions)
      - [Findings and Observations](#findings-and-observations-2)
      - [Summary of Insights](#summary-of-insights-2)
      - [Impact on Development](#impact-on-development-2)
    - [Survey (By Xinyi Ren)](#survey-by-xinyi-ren)
      - [Purpose](#purpose-3)
      - [Participants](#participants-3)
      - [Evaluation Method](#evaluation-method-3)
      - [Findings and Observations](#findings-and-observations-3)
      - [Summary of Insights](#summary-of-insights-3)
      - [Impact on Development](#impact-on-development-3)
  - [Shaders and Special Effects](#shaders-and-special-effects)
      - [Particle System](#particle-system)
      - [Shaders](#shaders)
  - [Summary of Contributions](#summary-of-contributions)
  - [References and External Resources](#references-and-external-resources)

## Evaluation Plan

1. **Evaluation Techniques**

    - **Cooperative Evaluation**: Since Escape the VimEnsion involves learning through typing commands, cooperative evaluation enables real-time observation of how players interpret the tutorial and apply the commands. Compared to the traditional "Think Aloud" method, this technique allows players to ask the team clarifying questions during the game play, helping the team identify areas that could cause any confusion, lack of clarity, or ineffective delivery.
    - **Post-Task Walkthrough**: This method allows the team to identify overall game experience and collect direct feedback on the level design, pacing, and command learning.
    - **Interview**: Interviews provide deeper qualitative insights into the overall player experience, including what aspects of the game they found engaging, confusing, or memorable. This helps uncover emotional and motivational responses that are not captured through observational methods.
    - **Survey/ Questionnaire**: Surveys efficiently gather quantitative data from multiple participants, measuring satisfaction, difficulty, and usability. They allow the team to analyse trends and overall player feedback using structured responses.

2. **Participants**

    - **Recruitment method**: As Escape the VimEnsion is an educational game designed to help beginners familiarise themselves with basic Vim commands, participants will be recruited from university peers.
    - **Target audience**: University students aged 18-25 with little or no prior experience using the Vim text editor.
    - **Number of participants**: Approximately 12 or more participants will be invited.

3. **Data Collection**

    - Type of data collected:
        - Quantitative: Task completion time, number of deaths, task success rate
        - Qualitative: player feedback, overall satisfaction, and understanding of Vim commands.
    - Data collection methods: Observation notes during playtesting (Microsoft Word), post-task forms, and online surveys (SurveyMonkey)

4. **Data Analysis**

    - **Evaluation metrics**: average task completion time, average number of deaths, mean player satisfaction score, categorised themes from qualitative comments.
    - These analyses will guide our team in identifying confusing design, pacing issues, or unclear instructions. Gaining users' feedback can help us directly improve our gameplay design, and refine our tutorial clarity.

5. **Timeline**

    - Design evaluation materials, finalise survey and interview questions - week 10/ week 11
    - Invite and confirm participants - week 11
    - Conduct cooperative evaluations, walkthroughs, and surveys - week 11/ early week 12
    - Process and summarise findings, propose improvements - week 12

6. **Responsibilities**

    - Cooperative Evaluation: Conduct tests with at least three participants
    - Post-task Walkthrough: Conduct tests with at least three participants
    - Interview: Prepare questions and interview at least three participants
    - Survey: Create and distribute surveys to at least three participants

7. **Expected Outcomes**
    - Usability and interface feedback: Identify areas where instructions are unclear.
    - Improved player engagement and learning flow: Ensure command-based gameplay feels intuitive and rewarding.
    - Players' overall satisfaction: Achieve positive post-game ratings on enjoyment and clarity.
    - Educational effectiveness: Demonstrate that players have gained familiarity with core Vim commands through gameplay.

## Evaluation Report

### Cooperative Evaluation (By Zhunkun He)

#### Purpose

The Cooperative Evaluation aimed to gather real-time, in-depth qualitative feedback. The focus was on observing player interaction with the core game mechanics (Vim controls) and identifying usability issues related to gameplay clarity (e.g., enemy attack warnings), in line with the goals of our original Evaluation Plan.

#### Participants

-   **Zhexian Song** - A graduated student with extensive gaming experience (preferring WASD controls) but no prior knowledge of Vim.
-   **Yibo Zhang** - An undergraduate student with some programming background but no direct Vim experience.
-   **Daniel Zhang** - An undergraduate student with some programming background and knows Vim.

#### Evaluation Method

Each session was conducted individually and lasted approximately 20-30 minutes. Participants were tasked with playing the game from the beginning while following the Cooperative Evaluation (or 'Think Aloud') protocol. They were encouraged to voice their thoughts, frustrations, and assumptions in real-time. Observational notes were recorded during the session.

### Findings and Observations

General Impressions: Participants were generally able to navigate the game world but expressed significant confusion regarding the game's core premise, mirroring findings from the Interview Evaluation.

**Feedback on Core Controls (from Zhexian Song)**

-   The most critical feedback came from Zhexian Song. He stated that he is "more used to using WASD for movement" and "didn't understand why hjkl had to be used."
-   This finding is a direct replication of the feedback from Emil Musiienko ("found the movement system... unintuitive and 'weird'").
-   This strongly indicates that the game's educational goal is not being communicated effectively to its target audience of non-Vim users. The "why" is missing.

**Feedback about some bugs (from Yibo Zhang)**

-   The player might be stuck by the door in front of the boss room.
-   The dash ability is hard to use and sometimes player cannot pass the trap easily.

**Feedback about something to improve (from Daniel Zhang)**
I liked the vim motion controls, I felt that the gameplay and attacks were a bit one dimensional though, since there doesn't seem to be a sequence of attacks that disables the cursor's attacks or enables a special ability. Graphics wise, the "dash" movement of the figurine could be made more dynamic to emphasis the speed and agility of the special powerup. Overall, its a game with a lot of potential if expanded upon!

**Combat Clarity Observations**
Two participants were observed attacking the EnemyTurtle repeatedly while it was in its defensive state, expressing confusion as to why it wasn't taking damage. This suggests the "defend" visual cue is not distinct enough from its normal state.

#### Summary of Insights

The observational findings confirm that while Escape the VimEnsion is mechanically functional, its core educational premise is failing to connect with its target audience. Players without prior context see the hjkl controls not as a learning opportunity, but as a poor design choice. The game must explicitly state its purpose (teaching Vim) at the very beginning to frame the player's experience correctly. Combat feedback (for both the Boss and the Turtle) is also not as clear as it needs to be.

#### Impact on Development

As a direct result of this cooperative evaluation and in conjunction with the interview findings:

-   **Clarify "Why"**: An introductory dialogue panel will be added at the start of the game, explicitly stating: "You are in the VimEnsion. To escape, you must master its controls: h, j, k, l."
-   **Improve Boss sweep attack**: The SweepAttack particle system will be made brighter and larger to more clearly define the attack's safe/unsafe zones.

### Post-task Walkthrough (By Casey Watt-Calder)

#### Purpose

This evaluation method provides a direct impression of how players would naturally play the game and navigate its tasks, providing insight into the process and reflections of a new player. This is particularly useful for understanding player patterns and how effectively the game communicates how to play.

#### Participants

-   Participant 1: Undergraduate computer science student with no vim experience
-   Participant 2: Undergraduate student with programming experience but no vim experience

#### Evaluation Method

For a post-task walkthrough, Participants were instructed to play the game without communication with as they did so, and were observed with note taking. Immediately after finishing I discussed the participant's thoughts thoughts and reflections with them, drawing attention to interesting gameplay behaviours.

#### Findings and Observations

##### Participant 1 Observations During Playthrough

-   Participant walked around a bit initially and stumbled into the potion room to the right before heading back and following the bones
-   Participant went left to the number 5 before triggering the dialogue mentioning that room
-   Participant completed the turtle fight quite easily after managing to hit the turtle the first time
-   Participant picked up item drops from the turtle before the dialogue drew attention to them
-   Participant finished both fights by mostly just repeating the attack action
-   Participant frequently looked at keyboard while playing and often seemed to mix up the buttons to move up and down.

##### Participant 1 Reflections After Playthrough

-   Participant wanted to see what else there was before following they identified as the intended path.
-   Participant chose to go left at toward the number 5 as it looked more like an offshoot of the main path
-   Participant felt most of the difficulty in the game was in getting used to the controls
-   Participant liked the ending

##### Participant 2 Observations During Playthrough

-   Participant clicked the notebook at the very beginning of the game
-   Participant intuitively pressed space to skip dialogue animations
-   Participant also looked at keyboard a lot and struggled to quickly find the correct key for an action
-   Participant also completed both fights with relative ease, mostly by repeating the attack action, sometimes looking at keyboard to orient the player toward the enemy
-   Participant restarted after finishing the game and opened the notebook which still contained all of the commands
-   Participant restarted and tried to use ":wq" command

##### Participant 2 Reflections After Playthrough

-   Participant was initially confused about the purpose of the notebook
-   Participant found the sound effects, especially the door opening to be loud
-   Participant felt the lack of dash animation looked awkward
-   Participant noticed "you might have to fight it" repeated in the pre turtle dialogue
-   Participant expected ":wq" command to do something different to just ":q"

#### Summary of Insights

Both participants were able to progress to the end of the game without getting stuck, showing suffient intuitivity and readability. There were some minor confusions and bugs that can be fixed although they didn't significantly derail the game. The main struggle players reported was with managing the controls, however since the purpose of the game is to teach the player how to use vim, this is appropriate as part of doing so requires learning to navigate vim's difficult controls. Participant 1 showed that a curious player might explore some things out of order, but also fortunately that doing so did not break the game nor cause them to get lost.

#### Impact on Development

In response to this feedback the following changes have been made:

-   Adding a placeholder text if the notebook would otherwise be empty
-   Resetting the notebook when restarting the game
-   Rebalancing audio
-   Removing the repeated “you might have to fight it” dialogue line

### Interview (By Vadym Musiienko)

#### Purpose

The interviews aimed to gather in-depth qualitative feedback on players' experiences, focusing on gameplay clarity, engagement, and overall learning effectiveness. Through direct conversations with participants, the goal was to identify emotional and motivational responses that quantitative methods could not capture.

#### Participants

-   **Henry Zhang** - an undergraduate student familiar with programming and basic text editors.
-   **William Haspel** - an undergraduate programming student with prior exposure to Vim.
-   **Emil Musiienko** - a non-student with no prior programming experience or familiarity with Vim.
    The participant pool was intentionally diverse to evaluate the game's accessibility and appeal across different experience levels.

#### Evaluation Method

Each interview was conducted individually after the playtesting session and lasted approximately 10-15 minutes. The sessions were semi-structured, allowing for follow-up questions and open discussion. Interviews were held in person, with notes recorded during the session.

#### Interview Questions

Participants were asked a mix of structured and open-ended questions, including:

1. What was your first impression of Escape the VimEnsion visual design and interface?
2. How easy or difficult was it to understand what you were supposed to do in the game?
3. Did the game make learning Vim commands feel engaging or tedious?
4. Were there any parts of the gameplay or UI that you found confusing or frustrating?
5. How did you feel about the overall tone and writing of the dialogues?
6. Was there anything you particularly liked or disliked about the game experience?
7. Did you notice any technical issues or bugs during your playthrough?
8. Do you think the game effectively communicated its educational goal? Why or why not?
9. If you could change or add one thing to improve the game, what would it be?

#### Findings and Observations

**General Impressions:**  
All three participants found the game's visual style and concept unique, particularly the typing-based gameplay. However, feedback varied based on programming familiarity.

**Feedback from Henry Zhang and William Haspel (programming background):**

-   Both understood the game's purpose - to teach basic Vim commands - and appreciated how gameplay was directly tied to learning these commands.
-   They reported that the controls felt intuitive once they recognized the Vim-based movement (using `h`, `j`, `k`, `l`).
-   Henry described the experience as "surprisingly fun for something educational," noting that the humorous dialogue helped maintain engagement.
-   Both participants suggested that the **UI elements (health bar and experience bar)** did not match the rest of interface (like the book). Based on this, the **UI was redesigned** to visually blend better with the in-game aesthetic (The health and exp bars).
-   William also identified a **WebGL-specific bug** where the **UI appeared shifted and distorted in fullscreen mode**, which was later fixed.
-   Another issue they both noticed was **flickering lighting in the WebGL build**, which was subsequently resolved.

**Feedback from Emil Musiienko (no programming background):**

-   Emil found the movement system using `h`, `j`, `k`, and `l` unintuitive and "weird", noting that it felt inconvenient compared to typical arrow-key movement.
-   He admitted he "didn't really get what the game was trying to teach," which highlighted that the **educational goal of Escape the VimEnsion might not be apparent to non-programmers**.
-   Despite this, he enjoyed the playful writing and humor in the dialogues, which helped make the experience more approachable.

**Overall Themes:**

-   **Clarity and Accessibility:** Participants with prior coding knowledge quickly understood the concept and purpose, while complete beginners struggled. This indicates that the current design appeals more strongly to players who have at least heard of Vim or programming concepts.
-   **User Interface:** The feedback led to tangible improvements - particularly in redesigning the health and experience bars to align with the theme.
-   **Technical Stability:** Interviews helped uncover critical **WebGL issues** (lighting flicker, fullscreen UI misalignment), all of which were fixed after testing.
-   **Engagement and Dialogue:** The humorous tone and character dialogue were consistently praised, leading to further refinement to make conversations more entertaining and dynamic.

#### Summary of Insights

The interview findings reveal that **Escape the VimEnsion successfully engages players familiar with programming** and effectively communicates its educational goals within that niche. However, **players without any Vim or coding context struggle to grasp the purpose**, suggesting the game's audience is relatively narrow. Future iterations could benefit from an introductory scene or tutorial explaining _why_ Vim is valuable and what commands the player is learning, to make the experience more inclusive.

#### Impact on Development

As a direct result of interview feedback:

-   **UI elements** (health bar and EXP bar) were redesigned to match the book-style interface.
-   **WebGL lighting and fullscreen UI bugs** were identified and resolved.
-   **Dialogue scripts** were improved to include light humor and greater narrative engagement.
-   The development team acknowledged the need to **broaden accessibility** for players unfamiliar with Vim by providing clearer context and visual guidance early in the game.

### Survey (By Xinyi Ren)

#### Purpose

The purpose of the survey evaluation was to collect structured feedback from players regarding their overall gameplay experience. By using quantitative ratings and short qualitative comments, the survey aimed to measure aspects such as enjoyability, difficulty, clarity of instructions, audio-visual quality, and learning effectiveness.

#### Participants

-   **Shun Fukui** - an undergraduate design student who is familiar with programming but not Vim.
-   **Howie Hu** - an postgraduate software engineering student who is familiar with programming but never used vim.
-   **Tarish Kadam** - an undergraduate science student who is familiar with programming but never used vim.
-   **Daniel Lee** - an undergraduate biotechnology degree student who has no experience with programming.
-   **Patrick Zhu** - an postgraduate psychology degree student who has no experience with programming.

#### Evaluation Method

Our team conducted a playtesting session with the participants to evaluate the overall enjoyability and effectiveness of our game. The participants were university students aged between 20 and 25, all had some general gaming experience and some were familiar with the concept of Vim but none of them had ever used it before.

#### Findings and Observations

**Quantitative Feedback**
Each participant completed a short survey about different aspects of the game. The average ratings were as follows:
| Evaluation Aspect | Average Rating ( /5.0) |
|--------------------|--------------------|
| Overall Enjoyability | 4.0 |
| Understanding of Rules | 4.4 |
| Core Gameplay | 3.6 |
| Graphics and Art Styles | 4.0 |
| Music and Sound Effects | 4.4 |
| Effectiveness of Teaching Vim Commands | 4.4 |
| Clarity and Helpfulness of Instructions | 4.2 |

Additionally, participants reported their completion times:

-   66.7% finished within 5–10 minutes,
-   33.3% finished within 10–15 minutes.
    Since the game was designed to take roughly 5–10 minutes, the feedback suggests the current difficulty and pacing are well aligned with the intended design.

**Qualitative Feedback**
Participants were also asked to provide some comments highlighting both strengths and areas for improvement.

-   Positive feedback: Most players praised the game’s graphics and visuals, describing the dash mechanic as fun and engaging. Several appreciated the boss design (represented as a “cursor”) as a creative concept.

*   Negative feedback:
    -   Some players found the instructions being not specific enough, leading to confusion about when to input commands. For instance, several attempted to press keys during dialogues rather than after them.
    -   A few participants also remarked that the background music volume was too low compared to the sound effects, which reduced the overall auditory balance.
    -   Some participants explicitly commented that they could not see their character and recommended making obstructive walls fade away when the player moves behind them.
    -   A few participants mentioned the controls were difficult to use in the game.

**Additional Observations**

-   The notebook feature located in the top-right corner, designed to record unlocked Vim commands and their descriptions. However, none of the participants noticed or used this feature during testing, indicating a need for stronger onboarding or visual cues to draw attention to its functionality.

#### Summary of Insights

Overall, the survey evaluation indicates that players found the game enjoyable, visually appealing, and effective in teaching basic Vim commands. The implemented changes successfully addressed key usability issues while maintaining the intended educational and design goals of the project.

#### Impact on Development

-   **Improved Instructions:** - We refined dialogue text to improve clarity, adding “Press Space to continue” prompts to ensure players complete the dialogue before interacting.
-   **Audio Balancing** - The background music volume was increased to maintain consistency with sound effects.
-   **Visual Adjustments** - Walls that previously obscured the player’s view were made transparent, improving spatial awareness while maintaining gameplay constraints.
-   **Vim Key Controls** - Some participants found the Vim-based controls unintuitive at first but adapted quickly. Since the core design goal was to familiarise players with Vim commands, we retained the control scheme in its current form.
-   **Notebook Instructions** - We added an introductory dialogue early in the game explicitly mentioning the notebook and encouraging players to refer to it when needed.

## Shaders and Special Effects

#### Particle System

Particle System for Assessment: **SweepEffect**

File Path: [Assets/Effects/SweepEffect.prefab](Assets/Effects/SweepEffect.prefab)

Description and Rationale
This particle system (SweepEffect) is a custom special effect designed for the final boss, the "Mouse Cursor." Its primary function is to serve as a clear and explicit Area of Effect (AoE) Indicator for the boss's "Rotational Sweep" attack.

The rationale for this design is to ensure game "fairness" and "clarity," a key concern identified in our user evaluations. This effect provides players with clear visual feedback by instantaneously "drawing" a precise sector on the ground that matches the boss's attack geometry. The system is non-looping (looping: 0) and set to playOnAwake: 1, ensuring it fires immediately upon instantiation by the BossCursor.cs script.

Particle System Attributes Varied
To achieve the "horizontal slash" effect, several key modules were configured in a non-trivial way:

Transform Orientation:

-   Settings: The prefab's Transform component is rotated 90 degrees on the X-axis (m_LocalEulerAnglesHint: {x: 90, y: 0, z: 0}).

-   Rationale: Particle systems emit "up" (along their Y-axis) by default. This rotation "flattens" the entire system, making it parallel to the ground. This is the foundational step that turns a vertical effect into a horizontal, ground-based slash.

Shape Module:

-   Settings: The Shape is set to Circle (type: 10) , with a Radius of 3 and an Arc of 180 degrees.
-   Rationale: This defines the precise size and semi-circular area of the boss's attack.
-   Key Setting: The Radius Thickness is set to 0. This is the most critical setting in this module, as it forces all particles to spawn only on the edge (the arc line) of the shape, not within the area. This creates the "slash trajectory" rather than a filled-in pie shape.

Emission Module:

-   Settings: Rate over Time is 0. A single Burst is used, emitting 50 particles at Time = 0.
-   Rationale: The attack warning must be instantaneous. A single, large burst ensures the entire arc is "drawn" in a single frame, providing a clear and immediate warning to the player.

Main Module (Speed & Lifetime):

-   Settings: Start Speed is set to 0. Start Lifetime is set to 1 second.
-   Rationale: Setting Start Speed to 0 is essential. It ensures the particles spawn and stay on the arc defined by the Shape module, rather than being fired from it. The 1-second lifetime gives them time to fade out gracefully.

Size & Color over Lifetime Modules:

-   Settings: Size over Lifetime is a curve that linearly decreases from value: 1 to value: 0. Color over Lifetime is a gradient whose Alpha fades from 1 (opaque) to 0 (transparent).
-   Rationale: Both modules work together to make the particles smoothly fade from existence instead of abruptly disappearing, creating a more polished visual effect.

Trails Module:

-   Settings: The module is enabled (enabled: 1) and set to Mode: Ribbon (mode: 1).
-   Rationale: This is the key module that creates the "slash" effect. The Ribbon mode connects the 50 discrete particles (spawned by the Burst ) into a single, continuous, flowing "blade" of light.
-   Refinement: Width over Trail and Color over Trail are also set to fade from 1 to 0, ensuring the trail itself fades out elegantly along its length, which reinforces the feeling of a fast-moving, dissipating attack.

Utilisation of Randomness:  
In this specific effect, randomness was deliberately avoided in key areas like Start Size and Start Color (both are set to constant values, not "Random Between...").

Rationale:  
The primary purpose of this particle system is clarity - to serve as an unambiguous AoE indicator. Introducing randomness to the size or color of the warning could make the attack's boundaries look "fuzzy" or inconsistent, potentially confusing the player. By using constant values, we ensure the warning is clean, sharp, and identical every time, which is crucial for fair, learnable gameplay. The autoRandomSeed: 1 setting provides sufficient internal variation without compromising the effect's core purpose.

#### Shaders

Shaders for Assessment: **Glowing Potion Effect** and **Erosion Effect**

Glowing Potion Effect Path: [`Assets/Effects/PotionGlowNew.shader`](Assets/Effects/PotionGlowNew.shader)

Erosion Effect Path: [`Assets/Effects/EnemyErosionPattern.shader`](Assets/Effects/EnemyErosionPattern.shader)

In the game, two non-trivial custom shaders were designed and implemented, including a glowing potion effect and an enemy erosion effect. These shaders not only enhance the overall visual quality but also align closely with the game’s background, reinforcing its fantasy atmosphere.

1. **Glowing Potion Effect**

    More specifically, the glowing potion shader follows the Forward Rendering logic of Unity’s Built-in Render Pipeline, implementing custom vertex and fragment functions defined through the UnityCG.cginc file. By tagging the pass as Transparent, enabling Blend SrcAlpha OneMinusSrcAlpha, disabling depth writes with ZWrite Off, and culling back faces, the shader ensures that the potion bottles render with realistic transparency, which perfectly matching the glass-like fantasy aesthetic the game aims for.

    The visual foundation of the shader begins with \_MainTexture multiplied by \_BaseColour, forming the base albedo. This is then enriched by diffuse, specular, and optional reflection components, controlled by \_SpecularColor, \_Shininess, \_Reflectivity, and the \_GlossyReflections toggle. These parameters collectively give the surface a polished, physically convincing finish, approaching the richness of Unity’s Standard shader while retaining custom artistic control.

    The signature repeating glow is driven by a sine wave, shaped through \_GlowSpeed, \_GlowIntensity, and \_GlowColor, which modulate both an outer emission and a companion Fresnel-based inner glow for natural luminosity. On top of this, rim lighting, which is configured by \_RimPower, \_RimIntensity, and \_RimColor, adds an additional layer of readability, highlighting object contours in synchrony with the animated glow. This creates a cohesive, dynamic lighting response that not only enhances the potion’s magical appearance but also serves as a clear in-game visual cue for the players.

    Moreover, our game is designed to help players familiarise themselves with Vim commands through an engaging and interactive gameplay experience. To achieve this goal, we aimed to make the clues within each puzzle as clear and intuitive as possible, ensuring that players can focus on learning command-based movement rather than struggling to identify objectives. The dynamic glowing effect applied to the potions provides a strong visual contrast against the otherwise static environment, naturally drawing the player’s attention toward key interactable elements.

    In addition, because the overall lighting design of the game is intentionally dark to maintain a mysterious and immersive atmosphere, the glow effect plays a crucial role in visual readability. It helps the potions stand out from the background, acting as both a gameplay cue and an aesthetic feature that complements the game’s fantasy theme. By highlighting interactive objects through controlled luminance, the shader not only enhances visual appeal but also improves player guidance, therefore simplifying gameplay and supporting the game’s educational purpose.

2. **Enemy Erosion Effect**

    Similarly, the project implements an erosion effect to make enemy characters disappear in a more visually engaging way, thereby enhancing the overall game visuals. The shader targets the Built-in Render Pipeline and uses a custom vertex and fragment pass that includes UnityCG.cginc and Lighting.cginc. Unlike the earlier potion shader, this material renders in the geometry queue as an opaque surface with depth writes enabled and no alpha blending.

    The dissolve effect is driven by the `clip(patternEdge - 0.001)` statement in the fragment stage, in which fragments that fail the comparison are discarded, producing the eroding effect. A triplanar sampling of the pattern texture blends world-space projections, ensuring the breakup follows surface orientation, while the Edge Width parameter softens the transition band. After testing several grayscale masks, we selected a soft cloud texture because it produced the smoothest breakup without visible seams.

    Each enemy character using this shader consists of a \_Threshold slider, while the materials tune \_EdgeWidth, tiling, glow, and reflection options per prefab. To automate the effect in game, an EnemyErosionController script is created to manage the process. It gathers child renderers on Awake, caches a MaterialPropertyBlock, and restores the alive threshold in OnEnable so respawned enemies appear intact. When gameplay scripts detect an enemy’s death, they invoke the `TriggerErode()` method, which starts a coroutine that interpolates \_Threshold from the alive value to the dead value over a specified duration. This design allows flexible per-enemy timing while maintaining consistent lighting and mask-based erosion visuals across the game.

    Furthermore, as mentioned earlier, the inclusion of the erosion effect was primarily intended to enhance the visual quality of the game and make enemy deaths appear more dynamic and engaging. During development, several mask textures were tested, including circle, square, and honeycomb patterns, with the aim of achieving a distinctive “torn apart” effect. After multiple trials, the cloud pattern produced the most visually appealing result, offering a smooth, organic breakup without noticeable seams.

    In addition, the decision to adopt this erosion-based dissolution also aligns with common practices in modern game design, where enemies rarely vanish instantly upon defeat. Instead, they often fade, burn, or dissolve in visually satisfying ways that reinforce narrative tone and polish. Our chosen dissolve style was partly inspired by the death sequence of Lord Voldemort in Harry Potter and the Deathly Hallows, as well as the advanced disintegration effects showcased in Resident Evil VIII: Village. By referencing these cinematic and AAA game examples, the effect adds both thematic depth and visual sophistication, elevating the overall presentation of enemy defeat in our game.

## Summary of Contributions

**Vadym Musiienko**

-   Acted as **team lead**
-   Designed and implemented **Start**, **Death**, and **End** scenes start to end.
-   **Integrated and programmed** the entire audio system, including background music, sound effects, and event-based audio triggers.
-   Implemented and integrated **character animations**, such as fighting, picking up items, and dashing.
-   **Coded core gameplay mechanics**, including combat, item pickup, and dash functionalities.
-   Developed the complete **level progression system**, including experience tracking and scaling.
-   Built the full **health system** for both player and enemies, along with corresponding health bars.
-   Conceived the **core game concept** (Vim-inspired gameplay) and contributed to the **overall game design direction**.
-   Wrote and implemented the **narrative and dialogue system**.
-   Implemented all **item-related systems**, such as experience potions and ability unlocks (e.g., dash).
-   Developed **camera functionality**, including dynamic zoom during player growth.
-   Programmed **scaling mechanics**, allowing the player’s size, movement and strength speed to increase with level.
-   Managed all **Git merges** and manually resolved **merge conflicts**.
-   Created and scripted the **game ending sequence**, including the `:q` mechanic to quit and complete the game.
-   Contributed additional **polish, debugging, and system integration** across multiple gameplay areas.
-   Contributed to the **project README**.
-   Conducted **interview evaluations with three participants** as part of the user evaluation process.
-   Implemented transparent wall feature

**Zhukun He**

-   Implemented features including:
    -   Player state machine
    -   layer health bar
    -   Enemy state machine
    -   Enemy AI
    -   Enemy boss
    -   Level build
    -   Dialogue system
    -   Trap event
    -   Fog cover
    -   Door trigger
    -   Damage system
    -   Boss sweep attack effect

**Casey Watt-Calder**

-   Recorded and produced gameplay video
-   Created notebook ui element with dialogue integration
-   Performed post-task walkthrough evaluation and made improvements according to the results

**Xinyi Ren**

-   Included initial GDD models (removed due to their file sizes)
-   Implemented features including:
    -   Potion glowing shader
    -   Enemy erosion shader
    -   Star attack particle system
-   Constructed evaluation plan
-   Performed survey evaluation and made improvements according to the results

## References and External Resources

**Shader references**

-   https://www.youtube.com/watch?v=kfM-yu0iQBk
-   https://www.youtube.com/watch?v=3mfvZ-mdtZQ
-   https://www.youtube.com/watch?v=f4s1h2YETNY
-   https://www.youtube.com/watch?v=k11wcndXrmc
-   https://www.youtube.com/watch?v=Nd4vKyDGidY
-   https://www.youtube.com/watch?v=OrWBSN0yasQ&list=PLq4ehwQIHfrUHo2UcxDAl_gcPLb2f3T2y&index=1
-   https://www.youtube.com/watch?v=6CECPjNAoZw&list=PLq4ehwQIHfrUHo2UcxDAl_gcPLb2f3T2y&index=4
-   https://www.youtube.com/watch?v=pOT5n1ZLxcE&list=PLq4ehwQIHfrUHo2UcxDAl_gcPLb2f3T2y&index=14
-   https://www.youtube.com/watch?v=RtevmyHKvWE&list=PLq4ehwQIHfrUHo2UcxDAl_gcPLb2f3T2y&index=14
-   https://www.youtube.com/watch?v=7q6GSVfyUbQ
-   Star png: https://www.rawpixel.com/image/5997620/illustration-png-sticker-elements
-   Cloud pattern: https://unsplash.com/photos/a-black-and-white-photo-of-clouds-in-the-sky-7Oq9r2CiTLg

**Audio references**

-   Background music (MainScene): https://freetouse.com/music/walen/medieval-village
-   All sound effects were found here: https://pixabay.com/

Notes and Future Improvements

Here are some issues we ran into in WebGL and weren't able to fix in time.

-   On some operating systems, there is a lighting glitch that appears only when the game is hosted in a browser via WebGL.
-   The transparent wall effect also doesn’t work in the browser as intended.
