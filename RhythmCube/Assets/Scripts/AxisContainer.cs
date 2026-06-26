using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AxisContainer : MonoBehaviour
{
    public CubeController CubeController;

    public MeshRenderer MeshRenderer;
    public Material DefaultMaterial;
    public Material[] MarkedMaterials;
    public Material CoolMaterial;
    public Material WarmMaterial;
    public Material HotMaterial;

    public Transform[] PositionAnchors;
    public LinkedList<Ingredient> Ingredients;

    public AxisContainer[] Tuplets { get; set; }

    private string _recipeResult;

    public AxisContainer()
    {
        Ingredients = new LinkedList<Ingredient>();
    }

    public void Mark(string markValue)
    {
        switch (markValue)
        {
            case "White":
                MeshRenderer.material = MarkedMaterials[0];
                break;
            case "Green":
                MeshRenderer.material = MarkedMaterials[1];
                break;
            case "Purple":
                MeshRenderer.material = MarkedMaterials[2];
                break;
            case "Red":
                MeshRenderer.material = MarkedMaterials[3];
                break;
            case "Blue":
                MeshRenderer.material = MarkedMaterials[4];
                break;
            case "Pink":
                MeshRenderer.material = MarkedMaterials[5];
                break;
            case "Yellow":
                MeshRenderer.material = MarkedMaterials[6];
                break;
            case "Duplet":
                MeshRenderer.material = CoolMaterial;
                break;
            case "Triplet":
                MeshRenderer.material = WarmMaterial;
                break;
            case "Quadruplet":
                MeshRenderer.material = HotMaterial;
                break;
            default:
                throw new System.Exception("Unhandled mark value");
        }
    }

    public void Unmark()
    {
        MeshRenderer.material = DefaultMaterial;
    }

    public void ExplodeAll()
    {
        foreach (Ingredient ingredient in Ingredients)
        {
            ingredient.Explode();
        }

        Ingredients.Clear();
    }

    private string[] _valid3OfAKindRecipes = new string[]
    {
        "White",
        "Green",
        "Purple",
        "Red",
        "Blue",
        "Pink",
        "Yellow"
    };

    public bool TryAddIngredient(Ingredient ingredient)
    {
        if (ingredient == null)
        {
            return false;
        }

        if (Ingredients.Count >= PositionAnchors.Length)
        {
            if (ingredient.IngredientType == Ingredients.First.Value.IngredientType)
            {
                if (_valid3OfAKindRecipes.Contains(_recipeResult))
                {
                    CubeController.PushToCenter(Ingredients.First.Value);
                    LevelController.Instance.ActionsCompleted["3-of-a-kind"] = true;
                    ingredient.Explode();
                    ExplodeAll();
                    Reorganize();
                    LevelController.Instance.Score += 6;
                    return true;
                }
                else if (_recipeResult == "Duplet")
                {
                    foreach (AxisContainer other in Tuplets)
                    {
                        other.ExplodeAll();
                    }

                    LevelController.Instance.ActionsCompleted["Duplet"] = true;
                    ingredient.Explode();
                    ExplodeAll();
                    Reorganize();
                    LevelController.Instance.Score += 18;
                    return true;
                }
                else if (_recipeResult == "Triplet")
                {
                    foreach (AxisContainer other in Tuplets)
                    {
                        other.ExplodeAll();
                    }

                    LevelController.Instance.ActionsCompleted["Triplet"] = true;
                    ingredient.Explode();
                    ExplodeAll();
                    Reorganize();
                    LevelController.Instance.Score += 36;
                    return true;
                }
                else if (_recipeResult == "Quadruplet")
                {
                    foreach (AxisContainer other in Tuplets)
                    {
                        other.ExplodeAll();
                    }

                    LevelController.Instance.ActionsCompleted["Quadruplet"] = true;
                    ingredient.Explode();
                    ExplodeAll();
                    Reorganize();
                    LevelController.Instance.Score += 72;
                    return true;
                }
            }

            Ingredients.Last.Value.Explode();
            Ingredients.RemoveLast();
            LevelController.Instance.Score -= 1;
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

        foreach (AxisContainer dc in LevelController.Instance.CubeController.Containers)
        {
            dc._recipeResult = LevelController.Instance.CheckRecipes(dc);

            if (dc._recipeResult == null)
            {
                dc.Unmark();
            }
            else
            {
                dc.Mark(dc._recipeResult);
            }
        }
    }
}
