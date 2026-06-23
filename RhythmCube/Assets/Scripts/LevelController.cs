using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class LevelController : MonoBehaviour
{
    public bool ShakeyCam = true;
    public Camera MainCamera;
    public Camera StableCamera;
    public FadeText InstructionText;
    public CubeController CubeController;
    public IngredientPool IngredientPool;

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

    void Start()
    {
        if (GameController.Instance == null)
        {
            SceneManager.LoadScene(0);
        }

        InitializeScenarios();
        IncrementScenario();
    }

    public void SetShakeyCam(bool val)
    {
        ShakeyCam = val;
    }

    void Update()
    {
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
}