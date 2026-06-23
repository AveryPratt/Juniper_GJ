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

    void Awake()
    {
        Queue = new Queue<Ingredient>();
    }

    void OnTriggerEnter(Collider other)
    {
        AxisContainer container = other.GetComponent<AxisContainer>();

        if (container != null)
        {
            CurrentContainer = container;
        }
    }

    public void LoadIngredient()
    {
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
            CurrentContainer.TryAddIngredient(Queue.Dequeue());
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
