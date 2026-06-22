using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    public bool SpinLock = false;
    public Animator Animator;

    public AxisContainer Axis_X;
    public AxisContainer Axis_Y;
    public AxisContainer Axis_Z;
    public AxisContainer Axis_X1;
    public AxisContainer Axis_Y1;
    public AxisContainer Axis_Z1;

    public AxisDetector Detector_X;
    public AxisDetector Detector_Y;
    public AxisDetector Detector_Z;
    public AxisDetector Detector_X1;
    public AxisDetector Detector_Y1;
    public AxisDetector Detector_Z1;

    void OnEnable()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.PlayerInput.OnForward += MoveForward;
            GameController.Instance.PlayerInput.OnLeft += MoveLeft;
            GameController.Instance.PlayerInput.OnBackward += MoveBackward;
            GameController.Instance.PlayerInput.OnRight += MoveRight;
            GameController.Instance.PlayerInput.OnCounterClockwise += MoveCounterClockwise;
            GameController.Instance.PlayerInput.OnClockwise += MoveClockwise;
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
            GameController.Instance.PlayerInput.OnCounterClockwise -= MoveCounterClockwise;
            GameController.Instance.PlayerInput.OnClockwise -= MoveClockwise;
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

    public void MoveCounterClockwise()
    {
        if (SpinLock)
        {
            Debug.Log("Input is locked");
            return;
        }

        Debug.Log("Cube moves Counter Clockwise");

        Animator.SetTrigger("CounterClockwise");
    }

    public void MoveClockwise()
    {
        if (SpinLock)
        {
            Debug.Log("Input is locked");
            return;
        }

        Debug.Log("Cube moves Clockwise");

        Animator.SetTrigger("Clockwise");
    }
}
