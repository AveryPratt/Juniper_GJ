using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AxisDetector : MonoBehaviour
{
    public AxisContainer CurrentContainer;

    void OnTriggerEnter(Collider other)
    {
        AxisContainer container = other.GetComponent<AxisContainer>();

        if (container != null)
        {
            CurrentContainer = container;
        }
    }
}
