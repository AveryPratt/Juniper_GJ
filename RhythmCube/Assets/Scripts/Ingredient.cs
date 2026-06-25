using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IIngredientType IngredientType;

    public void Attatch(Transform parent)
    {
        gameObject.transform.SetParent(parent);

        if (parent != null)
        {
            gameObject.transform.localPosition = Vector3.zero;
        }
    }

    public void Explode()
    {
        IngredientPool.Instance.CreateExplosion(IngredientType, transform.position);
        gameObject.SetActive(false);
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