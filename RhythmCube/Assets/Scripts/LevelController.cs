using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    void Start()
    {
        if (GameController.Instance == null)
        {
            SceneManager.LoadScene(0);
        }
    }
}
