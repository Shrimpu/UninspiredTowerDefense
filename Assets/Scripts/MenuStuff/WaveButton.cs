using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveButton : MonoBehaviour
{
    public GameObject button;

    Spawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        Spawner.waveComplete += () => { StartCoroutine(CheckForEnemies()); };
    }

    IEnumerator CheckForEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            GameObject[] activeEnemies = EnemyTracker.GetAllActiveEnemies();
            if (activeEnemies.Length == 0)
            {
                ShowButton();
                break;
            }
        }
    }

    void ShowButton()
    {
        button.SetActive(true);
    }

    public void StartNextWave()
    {
        button.SetActive(false);
        spawner.StartWave();
    }
}
