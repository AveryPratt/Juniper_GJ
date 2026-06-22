using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AxisContainer : MonoBehaviour
{
    public Transform[] PositionLocks;
    public Stack<Ingredient> Ingredients;

    public AxisContainer()
    {
        Ingredients = new Stack<Ingredient>();
    }

    public bool AddIngredient(Ingredient ingredient)
    {
        if (Ingredients.Count >= PositionLocks.Length)
        {
            return false;
        }

        Ingredients.Push(ingredient);

        return true;
    }

    public Ingredient RemoveIngredient()
    {
        return Ingredients.Pop();
    }

    private void Reorganize()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients.ElementAt(i).Attatch(PositionLocks[i]);
        }
    }
}
