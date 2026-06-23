using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AxisContainer : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public Material DefaultMaterial;
    public Material MarkedMaterial;

    public Transform[] PositionLocks;
    public Stack<Ingredient> Ingredients;

    public AxisContainer()
    {
        Ingredients = new Stack<Ingredient>();
    }

    public void Mark()
    {
        MeshRenderer.material = MarkedMaterial;
    }

    public void Unmark()
    {
        MeshRenderer.material = DefaultMaterial;
    }

    public bool TryAddIngredient(Ingredient ingredient)
    {
        if (ingredient == null || Ingredients.Count >= PositionLocks.Length)
        {
            return false;
        }

        Ingredients.Push(ingredient);

        Reorganize();

        return true;
    }

    public Ingredient RemoveIngredient()
    {
        if (Ingredients.Count == 0)
        {
            return null;
        }

        Ingredient popped = Ingredients.Pop();

        popped.Attatch(null);

        Reorganize();

        return popped;
    }

    private void Reorganize()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients.ElementAt(i).Attatch(PositionLocks[i]);
        }
    }
}
