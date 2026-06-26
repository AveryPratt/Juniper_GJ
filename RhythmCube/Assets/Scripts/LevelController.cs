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

    private bool _isPaused = false;
    public bool IsPaused
    {
        get
        {
            return _isPaused;
        }
        set
        {
            _isPaused = value;
            Time.timeScale = _isPaused ? 0 : 1;
            InstructionText.gameObject.SetActive(!_isPaused);
            PausedText.gameObject.SetActive(_isPaused);
            PausedDescription.gameObject.SetActive(_isPaused);
            PausedBackdrop.gameObject.SetActive(_isPaused);
            QuitButton.gameObject.SetActive(_isPaused);

            if (!_isPaused)
            {
                InstructionText.RefreshText();

                foreach (AudioSource track in MusicTracks)
                {
                    track.UnPause();
                }
            }
            else
            {
                foreach (AudioSource track in MusicTracks)
                {
                    track.Pause();
                }
            }
        }
    }
    public bool ShakeyCam = true;
    public Camera MainCamera;
    public Camera StableCamera;
    public FadeText InstructionText;
    public Text PausedText;
    public Text PausedDescription;
    public Image PausedBackdrop;
    public Button QuitButton;
    public Text ScoreText;
    public CubeController CubeController;
    public IngredientPool IngredientPool;

    public List<KeyValuePair<IIngredientType[], string>> Recipes = new List<KeyValuePair<IIngredientType[], string>>()
    {
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.White, IIngredientType.White, IIngredientType.White }, "White"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Green, IIngredientType.Green, IIngredientType.Green }, "Green"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Purple, IIngredientType.Purple, IIngredientType.Purple }, "Purple"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Red, IIngredientType.Red, IIngredientType.Red }, "Red"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Blue, IIngredientType.Blue, IIngredientType.Blue }, "Blue"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Pink, IIngredientType.Pink, IIngredientType.Pink }, "Pink"),
        new KeyValuePair<IIngredientType[], string>(new IIngredientType[3] { IIngredientType.Yellow, IIngredientType.Yellow, IIngredientType.Yellow }, "Yellow")
    };

    public AudioSource[] MusicTracks;
    public int[] ActiveTracks;

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

        PausedText.gameObject.SetActive(false);
        PausedDescription.gameObject.SetActive(false);
        PausedBackdrop.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.PlayerInput.OnPause += Pause;
        }
    }

    void Update()
    {
        ScoreText.text = "Score: " + Score.ToString();

        if (_waitTimer > 0)
        {
            _waitTimer -= Time.deltaTime;
        }

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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                KeysPressed["Escape"] = true;
            }
        }
    }

    public void SetShakeyCam(bool val)
    {
        ShakeyCam = val;
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void IncrementScenario()
    {
        _activeScenarioIdx += 1;

        if (_activeScenario.ActiveTracks != null)
        {
            ActiveTracks = _activeScenario.ActiveTracks;
        }

        if (_activeScenarioIdx >= _scenarios.Length)
        {
            Won = true;
            InstructionText.SetText("You Win!");
            return;
        }

        if (_activeScenario.IngredientsToAdd != null && _activeScenario.IngredientsToAdd.Any())
        {
            List<IIngredientType> ingredientsList = CubeController.Detector_Y.IngredientsToLoad.ToList();

            ingredientsList.AddRange(_activeScenario.IngredientsToAdd);

            CubeController.Detector_Y.IngredientsToLoad = ingredientsList.ToArray();
        }

        if (_activeScenario.InstructionText != null)
        {
            InstructionText.SetText(_activeScenario.InstructionText);
        }
        if (_activeScenario.PausedText != null)
        {
            PausedText.text = _activeScenario.PausedText;
        }
        if (_activeScenario.PausedDescription != null)
        {
            PausedDescription.text = _activeScenario.PausedDescription;
        }
    }

    public void StartMusic()
    {
        int i = 0;
        bool downbeat = MusicTracks[0].time > MusicTracks[0].clip.length - .5;
        bool beginning = MusicTracks[1].time > MusicTracks[1].clip.length - .5;

        foreach (AudioSource track in MusicTracks)
        {
            if ((downbeat || i == 0) && (ActiveTracks.Contains(i) || (i > 1 && ActiveTracks.Contains(1))) && (!track.isPlaying || track.time > track.clip.length - .5 || (i > 1 && beginning)))
            {
                track.Stop();
                track.Play();
            }

            if (i > 1)
            {
                track.mute = !ActiveTracks.Contains(i);
            }

            i += 1;
        }
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
        // nothing if container isn't full
        if (container.Ingredients.Count < 3)
        {
            return null;
        }

        // 3-of-a-kind
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

        // Duplets/Triplets
        GetDuplets(container);

        if (container.Duplets != null)
        {
            if (container.Duplets.Length > 2)
            {
                foreach (AxisContainer dc in container.Duplets)
                {
                    dc.Mark("Quadruplets");
                }

                return "Quadruplets";
            }
            else if (container.Duplets.Length == 2)
            {
                foreach (AxisContainer dc in container.Duplets)
                {
                    dc.Mark("Triplets");
                }

                return "Triplets";
            }
            else if (container.Duplets.Length == 1)
            {
                foreach (AxisContainer dc in container.Duplets)
                {
                    dc.Mark("Duplets");
                }

                return "Duplets";
            }
        }

        // none
        return null;
    }

    public void GetDuplets(AxisContainer container)
    {
        List<AxisContainer> duplicateContainers = new List<AxisContainer>();
        Ingredient[] ingredients = container.Ingredients.ToArray();

        foreach (AxisContainer other in CubeController.Containers)
        {
            if (other.name == container.name || other.Ingredients.Count < ingredients.Length)
            {
                continue;
            }

            Ingredient[] otherIngredients = other.Ingredients.ToArray();

            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i].IngredientType != otherIngredients[i].IngredientType)
                {
                    break;
                }

                if (i == ingredients.Length - 1)
                {
                    duplicateContainers.Add(other);
                }
            }
        }

        container.Duplets = duplicateContainers.ToArray();
    }
}