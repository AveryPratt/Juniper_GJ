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

    private AxisContainer[] _containers;
    private AxisDetector[] _detectors;

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

        _containers = new AxisContainer[]
        {
            Axis_X,
            Axis_Y,
            Axis_Z,
            Axis_X1,
            Axis_Y1,
            Axis_Z1
        };

        _detectors = new AxisDetector[]
        {
            Detector_X,
            Detector_Y,
            Detector_Z,
            Detector_X1,
            Detector_Y1,
            Detector_Z1
        };
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

    public void LoadIngredients()
    {
        foreach (AxisDetector detector in _detectors)
        {
            detector.LoadIngredient();
        }
    }

    public void DockIngredients()
    {
        foreach (AxisDetector detector in _detectors)
        {
            detector.DockIngredient();
        }
    }

    public void PoundIngredients()
    {
        foreach (AxisDetector detector in _detectors)
        {
            detector.Pound();
        }
    }

    public void MoveForward()
    {
        if (SpinLock)
        {
            return;
        }

        Animator.SetTrigger("Forward");
    }

    public void MoveLeft()
    {
        if (SpinLock)
        {
            return;
        }

        Animator.SetTrigger("Left");
    }

    public void MoveBackward()
    {
        if (SpinLock)
        {
            return;
        }

        Animator.SetTrigger("Backward");
    }

    public void MoveRight()
    {
        if (SpinLock)
        {
            return;
        }

        Animator.SetTrigger("Right");
    }

    public void MoveCounterClockwise()
    {
        if (SpinLock)
        {
            return;
        }

        Animator.SetTrigger("CounterClockwise");
    }

    public void MoveClockwise()
    {
        if (SpinLock)
        {
            return;
        }

        Animator.SetTrigger("Clockwise");
    }
}
