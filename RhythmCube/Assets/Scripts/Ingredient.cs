using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IIngredientType IngredientType;

    void Awake()
    {
        //Transform = GetComponent<Transform>();
    }

    public void SwitchType(IIngredientType ingredientType)
    {

    }

    public void Attatch(Transform parent)
    {
        gameObject.transform.SetParent(parent);

        if (parent != null)
        {
            gameObject.transform.localPosition = Vector3.zero;
        }
    }
}

public enum IIngredientType
{
    White,
    Green,
    Purple,
    Red,
    Blue,
    Pink,
    Yellow
}