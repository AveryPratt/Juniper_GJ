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
        public string Metadata { get; set; }
        public ScenarioCallback CheckForCompletion { get; set; }
        public IIngredientType[] IngredientsToAdd { get; set; }

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
            // tutorial
            new Scenario(CheckKeyPressed)
            {
                InstructionText = "Press W",
                Metadata = "W"
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
            // stage 1
            new Scenario(CheckScore)
            {
                InstructionText = "Good Luck!",
                Metadata = "10",
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.White
                }
            },
            new Scenario(CheckScore)
            {
                InstructionText = "",
                Metadata = "50",
                IngredientsToAdd = new IIngredientType[]
                {
                    IIngredientType.White
                }
            }
        };
    }

    public Dictionary<string, bool> KeysPressed = new Dictionary<string, bool>()
    {
        { "W", false },
        { "A", false },
        { "S", false },
        { "D", false },
        { "Q", false },
        { "E", false }
    };

    public bool CheckKeyPressed(LevelController controller, Scenario scenario)
    {
        return controller.KeysPressed[scenario.Metadata];
    }

    public bool CheckScore(LevelController controller, Scenario scenario)
    {
        int threshold = int.Parse(scenario.Metadata);

        return Score >= threshold;
    }
}