using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    public bool SpinLock = false;
    public Animator Animator;

    void OnEnable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.PlayerInput.OnForward += MoveForward;
            GameController.Instance.PlayerInput.OnLeft += MoveLeft;
            GameController.Instance.PlayerInput.OnBackward += MoveBackward;
            GameController.Instance.PlayerInput.OnRight += MoveRight;
        }
    }

    void OnDisable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.PlayerInput.OnForward -= MoveForward;
            GameController.Instance.PlayerInput.OnLeft -= MoveLeft;
            GameController.Instance.PlayerInput.OnBackward -= MoveBackward;
            GameController.Instance.PlayerInput.OnRight -= MoveRight;
        }
    }

    public void MoveForward()
    {
        if (SpinLock)
        {
            Debug.Log("Input is locked");
            return;
        }

        Debug.Log("Cube moves Forward");

        Animator.SetTrigger("Forward");
    }

    public void MoveLeft()
    {
        if (SpinLock)
        {
            Debug.Log("Input is locked");
            return;
        }

        Debug.Log("Cube moves Left");

        Animator.SetTrigger("Left");
    }

    public void MoveBackward()
    {
        if (SpinLock)
        {
            Debug.Log("Input is locked");
            return;
        }

        Debug.Log("Cube moves Backward");

        Animator.SetTrigger("Backward");
    }

    public void MoveRight()
    {
        if (SpinLock)
        {
            Debug.Log("Input is locked");
            return;
        }

        Debug.Log("Cube moves Right");

        Animator.SetTrigger("Right");
    }
}
