using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
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
        Debug.Log("Cube moves Forward");

        Animator.SetTrigger("Forward");
    }

    public void MoveLeft()
    {
        Debug.Log("Cube moves Left");

        Animator.SetTrigger("Left");
    }

    public void MoveBackward()
    {
        Debug.Log("Cube moves Backward");

        Animator.SetTrigger("Backward");
    }

    public void MoveRight()
    {
        Debug.Log("Cube moves Right");

        Animator.SetTrigger("Right");
    }
}
