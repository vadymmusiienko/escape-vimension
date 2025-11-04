# Project 2 Report

Read the [project 2
specification](https://github.com/feit-comp30019/project-2-specification) for
details on what needs to be covered here. You may modify this template as you
see fit, but please keep the same general structure and headings.

Remember that you should maintain the Game Design Document (GDD) in the
`README.md` file (as discussed in the specification). We've provided a
placeholder for it [here](README.md).

## Table of Contents

- [Project 2 Report](#project-2-report)
  - [Table of Contents](#table-of-contents)
  - [Evaluation Plan](#evaluation-plan)
  - [Evaluation Report](#evaluation-report)
    - [Interview Evaluation (By Vadym Musiienko)](#interview-evaluation-by-vadym-musiienko)
      - [Purpose](#purpose)
      - [Participants](#participants)
      - [Interview Method](#interview-method)
      - [Interview Questions](#interview-questions)
      - [Findings and Observations](#findings-and-observations)
      - [Summary of Insights](#summary-of-insights)
      - [Impact on Development](#impact-on-development)
  - [Shaders and Special Effects](#shaders-and-special-effects)
  - [Summary of Contributions](#summary-of-contributions)
  - [References and External Resources](#references-and-external-resources)

## Evaluation Plan

1. **Evaluation Techniques**

    - **Cooperative Evaluation**: Since VimLand involves learning through typing commands, cooperative evaluation enables real-time observation of how players interpret the tutorial and apply the commands. Compared to the traditional "Think Aloud" method, this technique allows players to ask the team clarifying questions during the game play, helping the team identify areas that could cause any confusion, lack of clarity, or ineffective delivery.
    - **Post-Task Walkthrough**: This method allows the team to identify overall game experience and collect direct feedback on the level design, pacing, and command learning.
    - **Interview**: Interviews provide deeper qualitative insights into the overall player experience, including what aspects of the game they found engaging, confusing, or memorable. This helps uncover emotional and motivational responses that are not captured through observational methods.
    - **Survey/ Questionnaire**: Surveys efficiently gather quantitative data from multiple participants, measuring satisfaction, difficulty, and usability. They allow the team to analyse trends and overall player feedback using structured responses.

2. **Participants**

    - **Recruitment method**: As VimLand is an educational game designed to help beginners familiarise themselves with basic Vim commands, participants will be recruited from university peers.
    - **Target audience**: University students aged 18-25 with little or no prior experience using the Vim text editor.
    - **Number of participants**: Approximately 12 or more participants will be invited.

3. **Data Collection**

    - Type of data collected:
      i. Quantitative: Task completion time, number of deaths, task success rate
      ii. Qualitative: player feedback, overall satisfaction, and understanding of Vim commands.
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

    - Cooperative Evaluation: Conduct tests with three participants
    - Post-task Walkthrough: Conduct tests with three participants
    - Interview: Prepare questions and interview three participants
    - Survey: Create and distribute surveys to three participants

7. **Expected Outcomes**
    - Usability and interface feedback: Identify areas where instructions are unclear.
    - Improved player engagement and learning flow: Ensure command-based gameplay feels intuitive and rewarding.
    - Players' overall satisfaction: Achieve positive post-game ratings on enjoyment and clarity.
    - Educational effectiveness: Demonstrate that players have gained familiarity with core Vim commands through gameplay.

## Evaluation Report

### Evaluation Plan (By Zhukun He)
### Evaluation Techniques

Cooperative Evaluation (Observation Method): This will be the primary technique for our 3-person test. As Escape the VimEnsion is an educational game, this method allows for real-time observation of how players interpret tutorials and apply Vim commands. We can directly observe where players "get stuck," particularly when encountering EnemyTurtle's defense mechanism or the BossCursor's attack patterns.

Interview Evaluation (Inquiry Method): This has already been completed by Vadym Musiienko. Our new findings from the Cooperative Evaluation will be used to supplement and reinforce these existing qualitative insights.

Participants

-   **Zhexian Song** - a post graduate student famaliar with programming but knows nothing about Vim.

Consent: All participants will be informed of the test's purpose and how their feedback will be used, and will provide verbal consent before the session begins.

### Data Collection

Data Type: The primary data will be Qualitative, gathered from observation notes.

Method: We will use the Cooperative Evaluation (or "Think Aloud") method, asking the 3 participants to voice their thoughts as they play.

Key Tasks for Observation:

Onboarding: Can the player successfully learn and use the h, j, k, l movement commands?

First Combat: How does the player react to the EnemyTurtle? Do they understand its defense mechanism?

Boss Battle: Can the player identify the BossCursor's rotational sweep attack warning (the SweepAttack) and react appropriately?

Data Analysis

Method: We will analyze the observation notes to identify recurring themes and critical incidents (e.g., "All 3 players failed to dodge the Boss's first attack," or "2 out of 3 players expressed confusion about the hjkl controls").

Impact: These findings will directly inform final-week development. For example, if players consistently fail to understand the boss's attack warning, the SweepAttack must be made larger or brighter.

Timeline & Responsibilities

Week 11/12: Conduct Cooperative Evaluation with 3 participants (Responsible: [Zhukun He]).

### Interview Evaluation (By Vadym Musiienko)

#### Purpose

The interviews aimed to gather in-depth qualitative feedback on players' experiences, focusing on gameplay clarity, engagement, and overall learning effectiveness. Through direct conversations with participants, the goal was to identify emotional and motivational responses that quantitative methods could not capture.

#### Participants

Three participants were interviewed:

-   **Henry Zhang** - an undergraduate student familiar with programming and basic text editors.
-   **William Haspel** - an undergraduate programming student with prior exposure to Vim.
-   **Emil Musiienko** - a non-student with no prior programming experience or familiarity with Vim.

The participant pool was intentionally diverse to evaluate the game's accessibility and appeal across different experience levels.

#### Interview Method

Each interview was conducted individually after the playtesting session and lasted approximately 10-15 minutes. The sessions were semi-structured, allowing for follow-up questions and open discussion. Interviews were held in person, with notes recorded during the session.

#### Interview Questions

Participants were asked a mix of structured and open-ended questions, including:

1. What was your first impression of VimLand's visual design and interface?
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
-   He admitted he "didn't really get what the game was trying to teach," which highlighted that the **educational goal of VimLand might not be apparent to non-programmers**.
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

### Cooperative Evaluation (By [Zhukun He])

### Purpose
The Cooperative Evaluation aimed to gather real-time, in-depth qualitative feedback. The focus was on observing player interaction with the core game mechanics (Vim controls) and identifying usability issues related to gameplay clarity (e.g., enemy attack warnings), in line with the goals of our original Evaluation Plan.

### Participants
Three participants were observed:

-   **Zhexian Song** - A graduated student with extensive gaming experience (preferring WASD controls) but no prior knowledge of Vim.

-   **Yibo Zhang** - An undergraduate student with some programming background but no direct Vim experience.

Participant 3 - An undergraduate student with no programming background.

### Evaluation Method
Each session was conducted individually and lasted approximately 20-30 minutes. Participants were tasked with playing the game from the beginning while following the Cooperative Evaluation (or 'Think Aloud') protocol. They were encouraged to voice their thoughts, frustrations, and assumptions in real-time. Observational notes were recorded during the session.

### Findings and Observations
The findings from this observational test strongly reinforce the qualitative insights gathered during the "Interview Evaluation".

General Impressions: Participants were generally able to navigate the game world but expressed significant confusion regarding the game's core premise, mirroring findings from the Interview Evaluation.

### Feedback on Core Controls (from Zhexian Song):
-   The most critical feedback came from Zhexian Song. He stated that he is "more used to using WASD for movement" and "didn't understand why hjkl had to be used."
-   This finding is a direct replication of the feedback from Emil Musiienko ("found the movement system... unintuitive and 'weird'").
-   This strongly indicates that the game's educational goal is not being communicated effectively to its target audience of non-Vim users. The "why" is missing.

### Feedback about some bugs (from Yibo Zhang):
-   The player might be stuck by the door in front of the boss room.
-   The dash ability is hard to use and sometimes player cannot pass the trap easily.
-   Player may use dash ability to dash into the trap while the dialogue before the trap is running.

### Combat Clarity Observations:

Two participants were observed attacking the EnemyTurtle repeatedly while it was in its defensive state, expressing confusion as to why it wasn't taking damage. This suggests the "defend" visual cue is not distinct enough from its normal state.

### Summary of Insights
The observational findings confirm that while Escape the VimEnsion is mechanically functional, its core educational premise is failing to connect with its target audience. Players without prior context see the hjkl controls not as a learning opportunity, but as a poor design choice. The game must explicitly state its purpose (teaching Vim) at the very beginning to frame the player's experience correctly. Combat feedback (for both the Boss and the Turtle) is also not as clear as it needs to be.

### Impact on Development
As a direct result of this cooperative evaluation and in conjunction with the interview findings:
-   **Clarify "Why"**: An introductory dialogue panel will be added at the start of the game, explicitly stating: "You are in the VimEnsion. To escape, you must master its controls: h, j, k, l." This directly addresses the feedback from Zhexian Song and Emil Musiienko.
-   **Improve Boss sweep attack**: The SweepAttack particle system will be made brighter and larger to more clearly define the attack's safe/unsafe zones.


## Shaders and Special Effects

**Particle System**
Particle System for Assessment: SweepEffect

File Path: Assets/Effects/SweepEffect.prefab

Description and Rationale
This particle system (SweepEffect) is a custom special effect designed for the final boss, the "Mouse Cursor." Its primary function is to serve as a clear and explicit Area of Effect (AoE) Indicator for the boss's "Rotational Sweep" attack.

The rationale for this design is to ensure game "fairness" and "clarity," a key concern identified in our user evaluations. This effect provides players with clear visual feedback by instantaneously "drawing" a precise sector on the ground that matches the boss's attack geometry. The system is non-looping (looping: 0) and set to playOnAwake: 1, ensuring it fires immediately upon instantiation by the BossCursor.cs script.

Particle System Attributes Varied
To achieve the "horizontal slash" effect, several key modules were configured in a non-trivial way:

Transform Orientation:
    Settings: The prefab's Transform component is rotated 90 degrees on the X-axis (m_LocalEulerAnglesHint: {x: 90, y: 0, z: 0}).
    Rationale: Particle systems emit "up" (along their Y-axis) by default. This rotation "flattens" the entire system, making it parallel to the ground. This is the foundational step that turns a vertical effect into a horizontal, ground-based slash.

Shape Module:
    Settings: The Shape is set to Circle (type: 10) , with a Radius of 3 and an Arc of 180 degrees.
    Rationale: This defines the precise size and semi-circular area of the boss's attack.
    Key Setting: The Radius Thickness is set to 0. This is the most critical setting in this module, as it forces all particles to spawn only on the edge (the arc line) of the shape, not within the area. This creates the "slash trajectory" rather than a filled-in pie shape.

Emission Module:
    Settings: Rate over Time is 0. A single Burst is used, emitting 50 particles at Time = 0.
    Rationale: The attack warning must be instantaneous. A single, large burst ensures the entire arc is "drawn" in a single frame, providing a clear and immediate warning to the player.

Main Module (Speed & Lifetime):
    Settings: Start Speed is set to 0. Start Lifetime is set to 1 second.
    Rationale: Setting Start Speed to 0 is essential. It ensures the particles spawn and stay on the arc defined by the Shape module, rather than being fired from it. The 1-second lifetime gives them time to fade out gracefully.

Size & Color over Lifetime Modules:
    Settings: Size over Lifetime is a curve that linearly decreases from value: 1 to value: 0. Color over Lifetime is a gradient whose Alpha fades from 1 (opaque) to 0 (transparent).
    Rationale: Both modules work together to make the particles smoothly fade from existence instead of abruptly disappearing, creating a more polished visual effect.

Trails Module:
    Settings: The module is enabled (enabled: 1) and set to Mode: Ribbon (mode: 1).
    Rationale: This is the key module that creates the "slash" effect. The Ribbon mode connects the 50 discrete particles (spawned by the Burst ) into a single, continuous, flowing "blade" of light.
    Refinement: Width over Trail and Color over Trail  are also set to fade from 1 to 0, ensuring the trail itself fades out elegantly along its length, which reinforces the feeling of a fast-moving, dissipating attack.

Utilisation of Randomness
In this specific effect, randomness was deliberately avoided in key areas like Start Size and Start Color  (both are set to constant values, not "Random Between...").

    Rationale: The primary purpose of this particle system is clarity-to serve as an unambiguous AoE indicator. Introducing randomness to the size or color of the warning could make the attack's boundaries look "fuzzy" or inconsistent, potentially confusing the player. By using constant values, we ensure the warning is clean, sharp, and identical every time, which is crucial for fair, learnable gameplay. The autoRandomSeed: 1  setting provides sufficient internal variation without compromising the effect's core purpose.

## Summary of Contributions

TODO - see specification for details
**Zhukun He**: Player state machine, Player health bar, Enemy state machine, Enemy AI, Enemy boss, level build, dialogue system, trap event, fog cover, door trigger, damage system, boss sweep attack effect

## References and External Resources

TODO - see specification for details
