using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOption : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int curBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (curBuildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(curBuildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Money.Remove(Money.MoneyOwned);
        Application.Quit();
    }
}
