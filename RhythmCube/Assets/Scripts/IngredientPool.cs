using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool : MonoBehaviour
{
    public static IngredientPool Instance;

    public int IngredientCount = 40;
    public Ingredient[] IngredientPrefabs;
    public Dictionary<IIngredientType, Ingredient[]> Inventory;
    public Dictionary<IIngredientType, int> Pointers;

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

        Inventory = new Dictionary<IIngredientType, Ingredient[]>();
        Pointers = new Dictionary<IIngredientType, int>();

        foreach (Ingredient prefabIngredient in IngredientPrefabs)
        {
            Inventory.Add(prefabIngredient.IngredientType, new Ingredient[IngredientCount]);
            Pointers.Add(prefabIngredient.IngredientType, 0);

            for (int i = 0; i < IngredientCount; i++)
            {
                Ingredient instance = Instantiate(prefabIngredient).GetComponent<Ingredient>();
                Inventory[prefabIngredient.IngredientType][i] = instance;
                instance.gameObject.SetActive(false);
            }
        }
    }

    public Ingredient FetchIngredient(IIngredientType ingredientType)
    {
        for (int i = 0; i < IngredientCount; i++)
        {
            if (!Inventory[ingredientType][Pointers[ingredientType]].gameObject.activeInHierarchy)
            {
                break;
            }

            IncrementPointer(ingredientType);
        }

        Ingredient ingredient = Inventory[ingredientType][Pointers[ingredientType]];
        ingredient.gameObject.SetActive(true);
        return ingredient;
    }

    public void IncrementPointer(IIngredientType ingredientType)
    {
        Pointers[ingredientType] += 1;

        if (Pointers[ingredientType] >= Inventory[ingredientType].Length)
        {
            Pointers[ingredientType] = 0;
        }
    }
}
