using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisDetector : MonoBehaviour
{
    public AxisContainer CurrentContainer;

    void OnTriggerEnter(Collider other)
    {
        CurrentContainer = other.GetComponent<AxisContainer>();
    }
}
