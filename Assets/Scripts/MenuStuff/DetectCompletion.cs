using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCompletion : MonoBehaviour
{
    public GameObject winScreen;

    void Start()
    {
        Spawner.allWavesComplete += ShowCompletionScreen;
    }

    void ShowCompletionScreen()
    {
        winScreen.SetActive(true);
    }
}
