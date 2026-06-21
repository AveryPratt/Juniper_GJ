using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public bool ShakeyCam = true;
    public Camera MainCamera;
    public Camera StableCamera;

    void Start()
    {
        if (GameController.Instance == null)
        {
            SceneManager.LoadScene(0);
        }
    }

    void Update()
    {
        if (ShakeyCam)
        {
            if (StableCamera.isActiveAndEnabled)
            {
                StableCamera.enabled = false;
                MainCamera.enabled = true;
            }
        }
        else
        {
            if (MainCamera.isActiveAndEnabled)
            {
                MainCamera.enabled = false;
                StableCamera.enabled = true;
            }
        }
    }
}
