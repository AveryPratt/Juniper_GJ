using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool : MonoBehaviour
{
    public static IngredientPool Instance;

    public int IngredientCount = 10;
    public GameObject[] IngredientTypes;
    public Dictionary<string, GameObject[]> Inventory;
    public Dictionary<string, int> Pointers;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (GameObject type in IngredientTypes)
        {
            Inventory = new Dictionary<string, GameObject[]>();
            Inventory.Add(type.name, new GameObject[IngredientCount]);
            Pointers = new Dictionary<string, int>();
            Pointers.Add(type.name, 0);

            for (int i = 0; i < IngredientCount; i++)
            {
                GameObject instance = Instantiate(type);
                Inventory[type.name][i] = instance;
                instance.SetActive(false);
            }
        }
    }

    public GameObject FetchIngredient(string ingredientType)
    {
        for (int i = 0; i < IngredientCount; i++)
        {
            if (!Inventory[ingredientType][Pointers[ingredientType]].activeInHierarchy)
            {
                break;
            }

            IncrementPointer(ingredientType);
        }

        GameObject ingredient = Inventory[ingredientType][Pointers[ingredientType]];
        ingredient.SetActive(true);
        return ingredient;
    }

    public void IncrementPointer(string ingredientType)
    {
        Pointers[ingredientType] += 1;

        if (Pointers[ingredientType] >= Inventory[ingredientType].Length)
        {
            Pointers[ingredientType] = 0;
        }
    }
}
