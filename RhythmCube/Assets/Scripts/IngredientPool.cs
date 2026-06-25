using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool : MonoBehaviour
{
    public static IngredientPool Instance;

    public int IngredientCount = 40;
    public Ingredient[] IngredientPrefabs;
    public IngredientExplosion[] ExplosionPrefabs;
    public Dictionary<IIngredientType, Ingredient[]> Inventory;
    public Dictionary<IIngredientType, int> Pointers;
    public Dictionary<IIngredientType, IngredientExplosion[]> ExplosionInventory;
    public Dictionary<IIngredientType, int> ExplosionPointers;

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
        ExplosionInventory = new Dictionary<IIngredientType, IngredientExplosion[]>();
        ExplosionPointers = new Dictionary<IIngredientType, int>();

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

        foreach (IngredientExplosion prefabExplosion in ExplosionPrefabs)
        {
            ExplosionInventory.Add(prefabExplosion.IngredientType, new IngredientExplosion[IngredientCount]);
            ExplosionPointers.Add(prefabExplosion.IngredientType, 0);

            for (int i = 0; i < IngredientCount; i++)
            {
                IngredientExplosion instance = Instantiate(prefabExplosion).GetComponent<IngredientExplosion>();
                ExplosionInventory[prefabExplosion.IngredientType][i] = instance;
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

    public IngredientExplosion CreateExplosion(IIngredientType ingredientType, Vector3 position)
    {
        for (int i = 0; i < IngredientCount; i++)
        {
            if (!ExplosionInventory[ingredientType][ExplosionPointers[ingredientType]].gameObject.activeInHierarchy)
            {
                break;
            }

            IncrementExplosionPointer(ingredientType);
        }

        IngredientExplosion explosion = ExplosionInventory[ingredientType][ExplosionPointers[ingredientType]];
        explosion.gameObject.SetActive(true);
        explosion.transform.SetPositionAndRotation(position, Quaternion.identity);
        return explosion;

    }

    public void IncrementPointer(IIngredientType ingredientType)
    {
        Pointers[ingredientType] += 1;

        if (Pointers[ingredientType] >= Inventory[ingredientType].Length)
        {
            Pointers[ingredientType] = 0;
        }
    }

    public void IncrementExplosionPointer(IIngredientType ingredientType)
    {
        ExplosionPointers[ingredientType] += 1;

        if (ExplosionPointers[ingredientType] >= ExplosionInventory[ingredientType].Length)
        {
            ExplosionPointers[ingredientType] = 0;
        }
    }
}
