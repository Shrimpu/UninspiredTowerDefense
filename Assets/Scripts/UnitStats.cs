using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitStats", menuName = "Stats/UnitStats/BaseUnitStats")]
public class UnitStats : ScriptableObject
{
    [Tooltip("The place this unit can be placed. this can be Ground, Water, Etc.")]
    public string terrainTag = "Ground";
    [Space]
    public GameObject projectile;
    public int damage = 1;
    [Tooltip("How many enemies it can pierce with a single shot")]
    public int pierce = 0;
    public float projectileSpeed = 1.2f;
    [Tooltip("Time the projectile is alive after reaching the end of this unit's range")]
    public float extraLifeTime = 0.2f;
    public float firerate = 1f;
    public float range = 4;
    [Tooltip("The required radius around it that needs to be clear for it to set")]
    public float size = 0.3f;
    [Tooltip("The storeprice for this unit")]
    public int price;
}
