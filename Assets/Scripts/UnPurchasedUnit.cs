using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitPreview", menuName = "UnitPreview")]
public class UnPurchasedUnit : ScriptableObject // this script holds what youll see when draging a unit from the shop and the unit it represents
{
    [Tooltip("The Unit it represents")]
    public GameObject unit;
    [Tooltip("The graphic you'll see")]
    public GameObject previewableUnit;

    public UnitStats GetStats()
    {
        return unit.GetComponent<Unit>().stats;
    }
}
