using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Transform Transform;

    void Awake()
    {
        Transform = GetComponent<Transform>();
    }

    public void Attatch(Transform parent)
    {
        Transform.SetParent(parent);

        if (parent != null)
        {
            Transform.localPosition = Vector3.zero;
        }
    }
}
