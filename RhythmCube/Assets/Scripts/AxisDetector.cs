using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class AxisDetector : MonoBehaviour
{
    public bool LoadsIngredients = false;
    public string[] IngredientsToLoad;
    public bool PoundsIngredients = false;
    public AxisContainer CurrentContainer;
    public Transform Dock;

    public Queue<Ingredient> Queue;

    private AxisContainer _verificationCandidate;
    private Collider _verificationCollider;
    private float _verificationStartTime;
    private Dictionary<string, int> _ingredientHistories;

    void Awake()
    {
        Queue = new Queue<Ingredient>();
        _ingredientHistories = new Dictionary<string, int>();
    }

    void OnTriggerEnter(Collider other)
    {
        AxisContainer container = other.GetComponent<AxisContainer>();

        if (container != null)
        {
            _verificationStartTime = Time.time;
            _verificationCandidate = other.GetComponent<AxisContainer>();
            _verificationCollider = other;
        }
    }

    void Update()
    {
        if (_verificationCandidate != null)
        {
            float dist = (_verificationCollider.transform.position - transform.position).magnitude;

            if (dist < 1)
            {
                //if (!string.IsNullOrEmpty(IngredientToLoad)) { CurrentContainer.Unmark(); }

                CurrentContainer = _verificationCandidate;

                //if (!string.IsNullOrEmpty(IngredientToLoad)) { CurrentContainer.Mark(); }

                _verificationCandidate = null;
            }
        }
    }

    public void LoadIngredient()
    {
        if (!LoadsIngredients || Queue.Any())
        {
            return;
        }

        UpdateIngredientHistories();

        string forceLoad = null;

        foreach (string key in _ingredientHistories.Keys.ToArray())
        {
            _ingredientHistories[key] += 1;

            if (_ingredientHistories[key] >= IngredientsToLoad.Length * 2)
            {
                forceLoad = key;
            }
        }

        string ingredientType = IngredientsToLoad.Length == 0 ? null : !string.IsNullOrEmpty(forceLoad) ? forceLoad : IngredientsToLoad[Mathf.FloorToInt(Random.Range(0, IngredientsToLoad.Length))];
        _ingredientHistories[ingredientType] = 0;

        if (!string.IsNullOrEmpty(ingredientType) && IngredientPool.Instance.Inventory.ContainsKey(ingredientType))
        {
            Ingredient ingredient = IngredientPool.Instance.FetchIngredient(ingredientType).GetComponent<Ingredient>();

            Queue.Enqueue(ingredient);
            ingredient.Attatch(Dock);
        }
    }

    private void UpdateIngredientHistories()
    {
        foreach (string key in IngredientsToLoad)
        {
            if (!_ingredientHistories.ContainsKey(key))
            {
                _ingredientHistories.Add(key, 0);
            }
        }

        foreach (string key in _ingredientHistories.Keys)
        {
            if (!IngredientsToLoad.Contains(key))
            {
                _ingredientHistories.Remove(key);
            }
        }
    }

    public void DockIngredient()
    {
        if (Queue.Any())
        {
            if (CurrentContainer.TryAddIngredient(Queue.Peek()))
            {
                Queue.Dequeue();
            }
        }
    }

    public void Pound()
    {
        if (!PoundsIngredients)
        {
            return;
        }

        Ingredient ingredient = CurrentContainer.RemoveIngredient();

        if (ingredient != null)
        {
            ingredient.gameObject.SetActive(false);
        }
    }
}
