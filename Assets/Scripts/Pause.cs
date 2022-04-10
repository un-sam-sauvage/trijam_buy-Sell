using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void LoadScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene, LoadSceneMode.Single);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
