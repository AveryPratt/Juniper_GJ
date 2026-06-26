using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate bool ScenarioCallback(LevelController controller, Scenario scenario);

    public class Scenario
    {
        public string InstructionText { get; set; }
        public string PausedText { get; set; }
        public string PausedDescription { get; set; }
        public string Metadata { get; set; }
        public ScenarioCallback CheckForCompletion { get; set; }
        public IIngredientType[] IngredientsToAdd { get; set; }
        public int[] ActiveTracks { get; set; }
        public bool ExplodeOnCompletion { get; set; }

        public Scenario(ScenarioCallback checkForCompletion)
        {
            CheckForCompletion = checkForCompletion;
        }
    }
}

public partial class LevelController : MonoBehaviour
{
    private Scenario[] _scenarios;

    private void InitializeScenarios()
    {
        _scenarios = new Scenario[]
        {
            new Scenario(Wait)
            {
                InstructionText = "Let's warm up!",
                PausedDescription = "Press W, A, S, D, Q, and E to rotate the container.",
                PausedText = "Game Paused",
                Metadata = "3",
                ActiveTracks = new int[] { 0 }
            },
            // tutorial
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press W",
                Metadata = "W",
            },
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press A",
                Metadata = "A"
            },
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press S",
                Metadata = "S"
            },
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press D",
                Metadata = "D"
            },
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press Q",
                Metadata = "Q"
            },
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press E",
                Metadata = "E"
            },
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press Esc",
                PausedText = "Game Paused",
                PausedDescription = "Don't know what to do?\r\n\r\nYou can pause the game at any time for a hint or description of how to advance.",
                Metadata = "Escape"
            },
            new Scenario(Wait)
            {
                InstructionText = "Break a Leg!",
                Metadata = "3",
                ActiveTracks = new int[] { 0, 1 }
            },
            // stage 1
            new Scenario(CheckActionCompleted)
            {
                InstructionText = "Activate the center: Get 3 in a row",
                PausedDescription =@"
Line up 3 identical objects into one container segment, then add another object of the same color to push it to the center.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
-1 point for pushing the wrong color object into the center.",
                Metadata = "3-of-a-kind",
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.White,
                    IIngredientType.Green
                }
            },
            new Scenario(CheckScore)
            {
                InstructionText = "Reach a score of 24",
                PausedDescription =@"
Reach a score of 24.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
-1 point for pushing the wrong color object into the center.",
                Metadata = "24",
                ExplodeOnCompletion = true
            },
            new Scenario(Wait)
            {
                InstructionText = "Good First Movement!",
                Metadata = "3",
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.Purple
                },
                ActiveTracks = new int[] { 0, 1, 2 }
            },
            // stage 2
            new Scenario(CheckActionCompleted)
            {
                InstructionText = "Duplets: Fill 2 containers with the same pattern",
                PausedDescription =@"
Fill 2 containers with the same pattern. Push an object of the same outside color to activate duplets.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
+18 points for activating duplicate patterns.
-1 point for pushing the wrong color object into the center.",
                Metadata = "Duplet"
            },
            new Scenario(CheckScore)
            {
                InstructionText = "Reach a score of 72",
                PausedDescription =@"
Reach a score of 72.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
+18 points for activating duplicate patterns.
-1 point for pushing the wrong color object into the center.",
                Metadata = "72",
                ExplodeOnCompletion = true
            },
            new Scenario(Wait)
            {
                InstructionText = "Nice Technique!",
                Metadata = "3",
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.Red
                },
                ActiveTracks = new int[] { 0, 1, 2, 3 }
            },
            // stage 3
            new Scenario(CheckActionCompleted)
            {
                InstructionText = "Triplets: Fill 3 containers with the same pattern",
                PausedDescription =@"
Fill 3 containers with the same pattern to activate triplets.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
+18 points for activating duplicate patterns.
+36 points for activating triplet patterns.
-1 point for pushing the wrong color object into the center.",
                Metadata = "Triplet",
            },
            new Scenario(CheckScore)
            {
                InstructionText = "Reach a score of 144",
                PausedDescription =@"
Reach a score of 144.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
+18 points for activating duplicate patterns.
+36 points for activating triplet patterns.
-1 point for pushing the wrong color object into the center.",
                Metadata = "144",
                ExplodeOnCompletion = true
            },
            new Scenario(Wait)
            {
                InstructionText = "Bravo! Bravo!",
                Metadata = "3",
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.Blue
                },
                ActiveTracks = new int[] { 0, 1, 2, 4 }
            },
            // stage 4
            new Scenario(CheckActionCompleted)
            {
                InstructionText = "Quadruplets: Fill 4 containers with the same pattern",
                PausedDescription =@"
Fill 4 containers with the same pattern to activate quadruplets.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
+18 points for activating duplicate patterns.
+36 points for activating triplet patterns.
+72 points for activating quadruplet patterns.
-1 point for pushing the wrong color object into the center.",
                Metadata = "Quadruplet"
            },
            new Scenario(CheckScore)
            {
                InstructionText = "Reach a score of 288",
                PausedDescription =@"
Reach a score of 288.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
+18 points for activating duplicate patterns.
+36 points for activating triplet patterns.
+72 points for activating quadruplet patterns.
-1 point for pushing the wrong color object into the center.",
                Metadata = "288",
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.Pink
                }
            },
            new Scenario(Wait)
            {
                InstructionText = "They're on the edge of their seats!",
                Metadata = "3",
                ActiveTracks = new int[] { 0, 1, 2, 5 },
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.Yellow
                }
            },
            // stage 5
            new Scenario(CheckActionCompleted)
            {
                InstructionText = "Finale: Change center to 7 distinct colors without losing a point",
                PausedDescription =@"
Win game by changing center to 7 distinct colors without losing a point.

+1 point for pushing object of center color into center (accumulates).
+6 points for changing center color by activating 3 in a row.
+18 points for activating duplicate patterns.
+36 points for activating triplet patterns.
+72 points for activating quadruplet patterns.
-1 point for pushing the wrong color object into the center.",
                Metadata = "Finale"
            },
            new Scenario(Wait)
            {
                InstructionText = "Standing Ovation! Encore! Encore!",
                Metadata = "3"
            },
        };
    }

    public Dictionary<string, bool> KeysPressed = new Dictionary<string, bool>()
    {
        { "W", false },
        { "A", false },
        { "S", false },
        { "D", false },
        { "Q", false },
        { "E", false },
        { "Escape", false }
    };

    public Dictionary<string, bool> ActionsCompleted = new Dictionary<string, bool>()
    {
        { "3-of-a-kind", false },
        { "Duplet", false },
        { "Triplet", false },
        { "Quadruplet", false },
        { "Finale", false }
    };

    private bool _waitTriggered = false;
    private float _waitTimer = 0;

    public bool CheckKeyPressed(LevelController controller, Scenario scenario)
    {
        return controller.KeysPressed[scenario.Metadata];
    }

    public bool CheckScore(LevelController controller, Scenario scenario)
    {
        int threshold = int.Parse(scenario.Metadata);

        return Score >= threshold;
    }

    public bool CheckActionCompleted(LevelController controller, Scenario scenario)
    {
        return ActionsCompleted[scenario.Metadata];
    }

    public bool Wait(LevelController controller, Scenario scenario)
    {
        if (!_waitTriggered)
        {
            _waitTimer = float.Parse(scenario.Metadata);
            _waitTriggered = true;
        }

        if (_waitTimer <= 0)
        {
            _waitTriggered = false;
            return true;
        }

        return false;
    }
}