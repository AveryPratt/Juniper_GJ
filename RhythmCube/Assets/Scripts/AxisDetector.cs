using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class AxisDetector : MonoBehaviour
{
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

    public void DockIngredient()
    {
        if (Queue.Any())
        {
            CurrentContainer.AddIngredient(Queue.Dequeue());
        }
    }

    public void Pound()
    {

    }
}
