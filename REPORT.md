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

    - **Cooperative Evaluation**: Since VimLand involves learning through typing commands, cooperative evaluation enables real-time observation of how players interpret the tutorial and apply the commands. Compared to the traditional “Think Aloud” method, this technique allows players to ask the team clarifying questions during the game play, helping the team identify areas that could cause any confusion, lack of clarity, or ineffective delivery.
    - **Post-Task Walkthrough**: This method allows the team to identify overall game experience and collect direct feedback on the level design, pacing, and command learning.
    - **Interview**: Interviews provide deeper qualitative insights into the overall player experience, including what aspects of the game they found engaging, confusing, or memorable. This helps uncover emotional and motivational responses that are not captured through observational methods.
    - **Survey/ Questionnaire**: Surveys efficiently gather quantitative data from multiple participants, measuring satisfaction, difficulty, and usability. They allow the team to analyse trends and overall player feedback using structured responses.

2. **Participants**

    - **Recruitment method**: As VimLand is an educational game designed to help beginners familiarise themselves with basic Vim commands, participants will be recruited from university peers.
    - **Target audience**: University students aged 18–25 with little or no prior experience using the Vim text editor.
    - **Number of participants**: Approximately 12 or more participants will be invited.

3. **Data Collection**

    - Type of data collected:
      i. Quantitative: Task completion time, number of deaths, task success rate
      ii. Qualitative: player feedback, overall satisfaction, and understanding of Vim commands.
    - Data collection methods: Observation notes during playtesting (Microsoft Word), post-task forms, and online surveys (SurveyMonkey)

4. **Data Analysis**

    - **Evaluation metrics**: average task completion time, average number of deaths, mean player satisfaction score, categorised themes from qualitative comments.
    - These analyses will guide our team in identifying confusing design, pacing issues, or unclear instructions. Gaining users’ feedback can help us directly improve our gameplay design, and refine our tutorial clarity.

5. **Timeline**

    - Design evaluation materials, finalise survey and interview questions – week 10/ week 11
    - Invite and confirm participants – week 11
    - Conduct cooperative evaluations, walkthroughs, and surveys – week 11/ early week 12
    - Process and summarise findings, propose improvements – week 12

6. **Responsibilities**

    - Cooperative Evaluation: Conduct tests with three participants
    - Post-task Walkthrough: Conduct tests with three participants
    - Interview: Prepare questions and interview three participants
    - Survey: Create and distribute surveys to three participants

7. **Expected Outcomes**
    - Usability and interface feedback: Identify areas where instructions are unclear.
    - Improved player engagement and learning flow: Ensure command-based gameplay feels intuitive and rewarding.
    - Players’ overall satisfaction: Achieve positive post-game ratings on enjoyment and clarity.
    - Educational effectiveness: Demonstrate that players have gained familiarity with core Vim commands through gameplay.

## Evaluation Report

### Interview Evaluation (By Vadym Musiienko)

#### Purpose

The interviews aimed to gather in-depth qualitative feedback on players’ experiences, focusing on gameplay clarity, engagement, and overall learning effectiveness. Through direct conversations with participants, the goal was to identify emotional and motivational responses that quantitative methods could not capture.

#### Participants

Three participants were interviewed:

-   **Henry Zhang** – an undergraduate student familiar with programming and basic text editors.
-   **William Haspel** – an undergraduate programming student with prior exposure to Vim.
-   **Emil Musiienko** – a non-student with no prior programming experience or familiarity with Vim.

The participant pool was intentionally diverse to evaluate the game’s accessibility and appeal across different experience levels.

#### Interview Method

Each interview was conducted individually after the playtesting session and lasted approximately 10–15 minutes. The sessions were semi-structured, allowing for follow-up questions and open discussion. Interviews were held in person, with notes recorded during the session.

#### Interview Questions

Participants were asked a mix of structured and open-ended questions, including:

1. What was your first impression of VimLand’s visual design and interface?
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
All three participants found the game’s visual style and concept unique, particularly the typing-based gameplay. However, feedback varied based on programming familiarity.

**Feedback from Henry Zhang and William Haspel (programming background):**

-   Both understood the game’s purpose — to teach basic Vim commands — and appreciated how gameplay was directly tied to learning these commands.
-   They reported that the controls felt intuitive once they recognized the Vim-based movement (using `h`, `j`, `k`, `l`).
-   Henry described the experience as “surprisingly fun for something educational,” noting that the humorous dialogue helped maintain engagement.
-   Both participants suggested that the **UI elements (health bar and experience bar)** did not match the rest of interface (like the book). Based on this, the **UI was redesigned** to visually blend better with the in-game aesthetic (The health and exp bars).
-   William also identified a **WebGL-specific bug** where the **UI appeared shifted and distorted in fullscreen mode**, which was later fixed.
-   Another issue they both noticed was **flickering lighting in the WebGL build**, which was subsequently resolved.

**Feedback from Emil Musiienko (no programming background):**

-   Emil found the movement system using `h`, `j`, `k`, and `l` unintuitive and “weird,” noting that it felt inconvenient compared to typical arrow-key movement.
-   He admitted he “didn’t really get what the game was trying to teach,” which highlighted that the **educational goal of VimLand might not be apparent to non-programmers**.
-   Despite this, he enjoyed the playful writing and humor in the dialogues, which helped make the experience more approachable.

**Overall Themes:**

-   **Clarity and Accessibility:** Participants with prior coding knowledge quickly understood the concept and purpose, while complete beginners struggled. This indicates that the current design appeals more strongly to players who have at least heard of Vim or programming concepts.
-   **User Interface:** The feedback led to tangible improvements — particularly in redesigning the health and experience bars to align with the theme.
-   **Technical Stability:** Interviews helped uncover critical **WebGL issues** (lighting flicker, fullscreen UI misalignment), all of which were fixed after testing.
-   **Engagement and Dialogue:** The humorous tone and character dialogue were consistently praised, leading to further refinement to make conversations more entertaining and dynamic.

#### Summary of Insights

The interview findings reveal that **Escape the VimEnsion successfully engages players familiar with programming** and effectively communicates its educational goals within that niche. However, **players without any Vim or coding context struggle to grasp the purpose**, suggesting the game’s audience is relatively narrow. Future iterations could benefit from an introductory scene or tutorial explaining _why_ Vim is valuable and what commands the player is learning, to make the experience more inclusive.

#### Impact on Development

As a direct result of interview feedback:

-   **UI elements** (health bar and EXP bar) were redesigned to match the book-style interface.
-   **WebGL lighting and fullscreen UI bugs** were identified and resolved.
-   **Dialogue scripts** were improved to include light humor and greater narrative engagement.
-   The development team acknowledged the need to **broaden accessibility** for players unfamiliar with Vim by providing clearer context and visual guidance early in the game.

## Shaders and Special Effects

TODO - see specification for details

## Summary of Contributions

TODO - see specification for details

## References and External Resources

TODO - see specification for details
