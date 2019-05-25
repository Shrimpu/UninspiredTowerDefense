using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CircleSpreadUnitStats", menuName = "Stats/UnitStats/CircleSpreadUnitStats")]
public class CircleSpreadUnitStats : UnitStats
{
    [Tooltip("Amount of points around it that it shoots from")]
    public int firepoints = 6;
}
