using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AxisContainer : MonoBehaviour
{
    public CubeController CubeController;

    public MeshRenderer MeshRenderer;
    public Material DefaultMaterial;
    public Material MarkedMaterial;

    public Transform[] PositionAnchors;
    public LinkedList<Ingredient> Ingredients;

    public AxisContainer()
    {
        Ingredients = new LinkedList<Ingredient>();
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
        if (ingredient == null)
        {
            return false;
        }

        if (Ingredients.Count >= PositionAnchors.Length)
        {
            CubeController.PushToCenter(Ingredients.Last.Value);
            Ingredients.RemoveLast();
        }

        Ingredients.AddFirst(ingredient);

        Reorganize();

        return true;
    }

    public Ingredient RemoveIngredient()
    {
        if (Ingredients.Count == 0)
        {
            return null;
        }

        Ingredient popped = Ingredients.First.Value;
        Ingredients.RemoveFirst();

        popped.Attatch(null);

        Reorganize();

        return popped;
    }

    private void Reorganize()
    {
        for (int i = 0; i < Ingredients.Count; i++)
        {
            Ingredients.ElementAt(i).Attatch(PositionAnchors[i]);
        }
    }
}
