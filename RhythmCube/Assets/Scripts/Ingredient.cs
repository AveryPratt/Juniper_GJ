using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Transform Transform;

    public Ingredient()
    {
        Transform = GetComponent<Transform>();
    }

    public void Attatch(Transform transform)
    {
        Transform.SetParent(Transform);
        Transform.localPosition.Set(0, 0, 0);
    }
}
