using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public GameObject gameController;
    public GameObject endScreen;

    private void Start()
    {
        PlayerHealth.healthReachedZero += StopGame;
    }

    void StopGame()
    {
        gameController.SetActive(false);
        endScreen.SetActive(true);
    }
}
