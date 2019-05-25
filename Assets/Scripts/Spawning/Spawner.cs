using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public delegate void WaveComplete();
    public static WaveComplete waveComplete;
    public static WaveComplete allWavesComplete;

    public Wave[] waves;
    int waveIndex = 0;

    [System.Serializable]
    public class Wave
    {
        public SpawnPattern[] spawnPatterns;
        [HideInInspector]
        public int spawnIndex = 0; // keeps track of the current spawn pattern
        [HideInInspector]
        public int completionLevel = 0;
    }

    public void StartWave()
    {
        if (waveIndex < waves.Length)
        {
            for (int i = 0; i < waves[waveIndex].spawnPatterns.Length; i++)
            {
                waves[waveIndex].spawnPatterns[i] = Instantiate(waves[waveIndex].spawnPatterns[i]); // creates an instance of the scriptable object
                waves[waveIndex].spawnPatterns[i].SetupAll();

            }
            ChangeSpattern();
        }
    }

    void ChangeSpattern()
    {
        if (waves[waveIndex].spawnIndex < waves[waveIndex].spawnPatterns.Length) // checks if there is another wave available
        {
            for (int i = 0; i < waves[waveIndex].spawnPatterns[waves[waveIndex].spawnIndex].enemyGroups.Length; i++) // loops through all enemygroups
            {
                StartCoroutine(Spawn(waves[waveIndex].spawnPatterns[waves[waveIndex].spawnIndex].enemyGroups[i].initialWait,
                    waves[waveIndex].spawnPatterns[waves[waveIndex].spawnIndex].enemyGroups[i].spawnrate,
                    waves[waveIndex].spawnPatterns[waves[waveIndex].spawnIndex].enemyGroups[i].enemies));
            }
        }
    }

    public IEnumerator Spawn(float initialWait, float spawnrate, GameObject[] enemies)
    {
        yield return new WaitForSeconds(initialWait);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(true); // the enemies have already been spawned. This just activates it
            yield return new WaitForSeconds(1f / spawnrate);
        }
        AdvancePatternCompletionLevel();
    }

    void AdvancePatternCompletionLevel()
    {
        waves[waveIndex].completionLevel++;
        if (waves[waveIndex].completionLevel >= waves[waveIndex].spawnPatterns[waves[waveIndex].spawnIndex].enemyGroups.Length) // if there are no more enemies to spawn proceed
        {
            waves[waveIndex].spawnIndex++;
            if (waves[waveIndex].spawnIndex == waves[waveIndex].spawnPatterns.Length) // checks if the wave has been completed
            {
                waveIndex++;
                if (waveIndex >= waves.Length)
                {
                    allWavesComplete?.Invoke();
                    return;
                }
                waveComplete?.Invoke();
                return;
            }
            ChangeSpattern(); // starts a new pattern
            waves[waveIndex].completionLevel = 0; // resets completionLevel
        }
    }
}