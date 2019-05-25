using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnPattern", menuName = "SpawnPattern")]
public class SpawnPattern : ScriptableObject
{
    int completionlevel;
    public Pattern[] enemyGroups;

    [System.Serializable]
    public class Pattern
    {
        public GameObject enemy;
        public int amount = 10;
        public float spawnrate = 1;
        public float initialWait = 0;

        [HideInInspector]
        public GameObject[] enemies;

        public void Setup()
        {
            enemies = new GameObject[amount];
            for (int i = 0; i < amount; i++)
            {
                enemies[i] = Instantiate(enemy, new Vector3(100, 0, 0), Quaternion.identity);
                enemies[i].SetActive(false);
            }
        }
    }

    public void SetupAll()
    {
        for (int i = 0; i < enemyGroups.Length; i++)
        {
            enemyGroups[i].Setup();
        }
    }
}
