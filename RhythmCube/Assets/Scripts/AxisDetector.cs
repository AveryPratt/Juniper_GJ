using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class AxisDetector : MonoBehaviour
{
    public bool LoadsIngredients = false;
    public IIngredientType[] IngredientsToLoad;
    public bool PoundsIngredients = false;
    public AxisContainer CurrentContainer;
    public Transform Dock;

    public Queue<Ingredient> Queue;
    public int MaxIngredients = 3;

    private AxisContainer _verificationCandidate;
    private Collider _verificationCollider;
    private float _verificationStartTime;
    private Dictionary<IIngredientType, int> _ingredientHistories;

    void Awake()
    {
        Queue = new Queue<Ingredient>();
        _ingredientHistories = new Dictionary<IIngredientType, int>();
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
        if (!LoadsIngredients || Queue.Count >= MaxIngredients)
        {
            return;
        }

        IIngredientType? forceLoad = UpdateIngredientHistories();

        if (forceLoad == null)
        {
            foreach (IIngredientType key in _ingredientHistories.Keys.ToArray())
            {
                _ingredientHistories[key] += 1;

                if (_ingredientHistories[key] >= IngredientsToLoad.Length * 2)
                {
                    forceLoad = key;
                }
            }
        }

        IIngredientType? ingredientType = IngredientsToLoad.Length == 0 ? null : forceLoad != null ? forceLoad : IngredientsToLoad[Mathf.FloorToInt(Random.Range(0, IngredientsToLoad.Length))];

        if (ingredientType.HasValue)
        {
            _ingredientHistories[ingredientType.Value] = 0;

            if (IngredientPool.Instance.Inventory.ContainsKey(ingredientType.Value))
            {
                Ingredient ingredient = IngredientPool.Instance.FetchIngredient(ingredientType.Value).GetComponent<Ingredient>();

                Queue.Enqueue(ingredient);
                ingredient.Attatch(Dock);

                ArrangeQueue(true);
            }
        }
    }

    private void ArrangeQueue(bool full)
    {
        Vector3 localOffset = Vector3.zero;

        List<Ingredient> orderedList = Queue.ToList();

        for (int i = orderedList.Count; i < (full ? MaxIngredients : MaxIngredients - 1); i++)
        {
            orderedList.Insert(0, null);
        }

        foreach (Ingredient item in orderedList)
        {
            if (item != null)
            {
                item.Attatch(Dock);
                item.gameObject.transform.localPosition += localOffset;
            }

            localOffset += new Vector3(0f, 2f, 0f);
        }
    }

    private IIngredientType? UpdateIngredientHistories()
    {
        IIngredientType? forceLoad = null;

        foreach (IIngredientType key in IngredientsToLoad)
        {
            if (!_ingredientHistories.ContainsKey(key))
            {
                _ingredientHistories.Add(key, 0);

                if (forceLoad == null)
                {
                    forceLoad = key;
                }
            }
        }

        foreach (IIngredientType key in _ingredientHistories.Keys)
        {
            if (!IngredientsToLoad.Contains(key))
            {
                _ingredientHistories.Remove(key);
            }
        }

        return forceLoad;
    }

    public void DockIngredient()
    {
        if (Queue.Count >= MaxIngredients)
        {
            if (CurrentContainer.TryAddIngredient(Queue.Peek()))
            {
                Queue.Dequeue();
            }
        }

        ArrangeQueue(false);
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
