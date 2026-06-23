using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class AxisDetector : MonoBehaviour
{
    public string IngredientToLoad;
    public bool PoundsIngredients = false;
    public AxisContainer CurrentContainer;
    public Transform Dock;

    public Queue<Ingredient> Queue;

    private AxisContainer _verificationCandidate;
    private Collider _verificationCollider;
    private float _verificationStartTime;

    void Awake()
    {
        Queue = new Queue<Ingredient>();
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
        if (_verificationCandidate != null /*&& Time.time - _verificationStartTime >= .15f*/)
        {
            float dist = (_verificationCollider.transform.position - transform.position).magnitude;

            if (dist < 3)
            {
                CurrentContainer = _verificationCandidate;
                _verificationCandidate = null;
            }
        }
    }

    public void LoadIngredient()
    {
        if (Queue.Any())
        {
            return;
        }

        if (!string.IsNullOrEmpty(IngredientToLoad) && IngredientPool.Instance.Inventory.ContainsKey(IngredientToLoad))
        {
            Ingredient ingredient = IngredientPool.Instance.FetchIngredient(IngredientToLoad).GetComponent<Ingredient>();

            Queue.Enqueue(ingredient);
            ingredient.Attatch(Dock);
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
