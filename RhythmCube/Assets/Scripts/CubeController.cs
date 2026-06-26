using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeController : MonoBehaviour
{
    public bool SpinLock = false;
    public Animator Animator;

    public Transform CenterCube;
    private MeshRenderer _centerMeshRenderer;
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

    public Material[] CenterMaterials;

    public AxisContainer[] Containers;
    public AxisDetector[] Detectors;
    private Ingredient _centerIngredient;

    void Awake()
    {
        _centerMeshRenderer = CenterCube.GetComponent<MeshRenderer>();
    }

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

        Containers = new AxisContainer[]
        {
            Axis_X,
            Axis_Y,
            Axis_Z,
            Axis_X1,
            Axis_Y1,
            Axis_Z1
        };

        Detectors = new AxisDetector[]
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

    public void PushToCenter(Ingredient ingredient)
    {
        if (_centerIngredient != null)
        {
            _centerIngredient.gameObject.SetActive(false);
        }

        ingredient.Attatch(CenterCube);
        _centerIngredient = ingredient;

        ActivateCenterIngredient();
    }

    public void ActivateCenterIngredient()
    {
        Material centerMat;

        if (_centerIngredient == null)
        {
            centerMat = CenterMaterials[0];
        }
        else
        {
            switch (_centerIngredient.IngredientType)
            {
                case IIngredientType.White:
                    centerMat = CenterMaterials[6];
                    break;
                case IIngredientType.Green:
                    centerMat = CenterMaterials[2];
                    break;
                case IIngredientType.Purple:
                    centerMat = CenterMaterials[4];
                    break;
                case IIngredientType.Red:
                    centerMat = CenterMaterials[5];
                    break;
                case IIngredientType.Blue:
                    centerMat = CenterMaterials[1];
                    break;
                case IIngredientType.Pink:
                    centerMat = CenterMaterials[3];
                    break;
                case IIngredientType.Yellow:
                    centerMat = CenterMaterials[7];
                    break;
                default:
                    centerMat = CenterMaterials[0];
                    break;
            }

            LevelController.Instance.Score += 6;
        }

        _centerMeshRenderer.material = centerMat;
    }

    public void LoadIngredients()
    {
        foreach (AxisDetector detector in Detectors)
        {
            detector.LoadIngredient();
        }
    }

    public void DockIngredients()
    {
        foreach (AxisDetector detector in Detectors)
        {
            detector.DockIngredient();
        }
    }

    public void PoundIngredients()
    {
        foreach (AxisDetector detector in Detectors)
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
