using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public bool ShakeyCam = true;
    public Camera MainCamera;
    public Camera StableCamera;
    public FadeText InstructionText;
    public Text ScoreText;
    public CubeController CubeController;
    public IngredientPool IngredientPool;

    public List<KeyValuePair<IIngredientType[], string>> Recipes = new List<KeyValuePair<IIngredientType[], string>>()
    {
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.White, IIngredientType.White, IIngredientType.White }, "10"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Green, IIngredientType.Green, IIngredientType.Green }, "10"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Purple, IIngredientType.Green, IIngredientType.Green }, "10"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Red, IIngredientType.Green, IIngredientType.Green }, "10"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Blue, IIngredientType.Green, IIngredientType.Green }, "10"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Pink, IIngredientType.Green, IIngredientType.Green }, "10"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Yellow, IIngredientType.Green, IIngredientType.Green }, "10")
    };

public int Score = 0;
    public bool Won = false;

    private int _activeScenarioIdx = -1;
    private Scenario _activeScenario
    {
        get
        {
            return _scenarios[_activeScenarioIdx];
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (GameController.Instance == null)
        {
            SceneManager.LoadScene(0);
        }

        InitializeScenarios();
        IncrementScenario();
    }

    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString();

        if (ShakeyCam)
        {
            if (StableCamera.isActiveAndEnabled)
            {
                StableCamera.enabled = false;
                MainCamera.enabled = true;
            }
        }
        else
        {
            if (MainCamera.isActiveAndEnabled)
            {
                MainCamera.enabled = false;
                StableCamera.enabled = true;
            }
        }

        if (!Won && _activeScenario.CheckForCompletion(this, _activeScenario))
        {
            IncrementScenario();
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                KeysPressed["W"] = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                KeysPressed["A"] = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                KeysPressed["S"] = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                KeysPressed["D"] = true;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                KeysPressed["Q"] = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                KeysPressed["E"] = true;
            }
        }
    }

    public void IncrementScenario()
    {
        _activeScenarioIdx += 1;

        if (_activeScenarioIdx >= _scenarios.Length)
        {
            Won = true;
            UpdateInstructionText("You Win!");
            return;
        }

        if (_activeScenario.IngredientsToAdd != null && _activeScenario.IngredientsToAdd.Any())
        {
            List<IIngredientType> ingredientsList = CubeController.Detector_Y.IngredientsToLoad.ToList();

            ingredientsList.AddRange(_activeScenario.IngredientsToAdd);

            CubeController.Detector_Y.IngredientsToLoad = ingredientsList.ToArray();
        }

        UpdateInstructionText(_activeScenario.InstructionText);
    }

    public void UpdateInstructionText(string text)
    {
        InstructionText.SetText(text);
    }

    public void LoadIngredients()
    {
        CubeController.LoadIngredients();
    }

    public void DockIngredients()
    {
        CubeController.DockIngredients();
    }

    public void PoundIngredients()
    {
        CubeController.PoundIngredients();
    }

    public string CheckRecipes(AxisContainer container)
    {
        if (container.Ingredients.Count < 3)
        {
            return null;
        }

        foreach (KeyValuePair<IIngredientType[], string> recipe in Recipes)
        {
            int i = 0;

            foreach (Ingredient ingredient in container.Ingredients)
            {
                if (ingredient.IngredientType != recipe.Key[i])
                {
                    break;
                }

                i += 1;
            }

            if (i == 3)
            {
                return recipe.Value;
            }
        }

        return null;
    }
}