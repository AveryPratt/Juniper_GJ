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
    public event Action OnPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnForward?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnLeft?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnBackward?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRight?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Greater))
        {
            OnCounterClockwise?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Less))
        {
            OnClockwise?.Invoke();
        }
    }
}
