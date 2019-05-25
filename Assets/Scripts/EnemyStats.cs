using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyStats", menuName = "Stats/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Tooltip("The rate of which the enemy moves")]
    public float speed = 2f;
    [Tooltip("The health and damage")]
    public int dangerLevel = 1;
    public int moneyDrop = 1;
}
