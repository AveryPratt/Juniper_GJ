using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnForward;
    public event Action OnLeft;
    public event Action OnBackward;
    public event Action OnRight;
    public event Action OnCounterClockwise;
    public event Action OnClockwise;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause");
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Forward");

            OnForward?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Left");

            OnLeft?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Backward");

            OnBackward?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right");

            OnRight?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Less))
        {
            Debug.Log("CounterClockwise");

            OnCounterClockwise?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Greater))
        {
            Debug.Log("Clockwise");

            OnClockwise?.Invoke();
        }
    }
}
