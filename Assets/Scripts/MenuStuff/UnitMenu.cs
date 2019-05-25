using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitMenu : MonoBehaviour
{
    public GameObject menu;
    public TMP_Text upgradeText;

    Unit heldUnit;

    void Start()
    {
        UnitSelected.selectionUpdated += ToggleActive;
    }

    void ToggleActive(bool active, Unit unit)
    {
        menu.SetActive(active);
        heldUnit = unit;
        ChangeText();
    }

    public void Upgrade()
    {
        if (heldUnit != null)
        {
            UnitStats nextLevel = UpgradeUnit.Upgrade(heldUnit.stats);
            if (Money.Remove(nextLevel.price))
            {
                heldUnit.stats = nextLevel;
                ChangeText();
            }
        }
    }

    public void ChangeText()
    {
        if (upgradeText != null && heldUnit != null)
        {
            UnitStats nextStats = UpgradeUnit.Upgrade(heldUnit.stats);
            if (nextStats != heldUnit.stats)
                upgradeText.text = "Upgrade Cost = " + nextStats.price;
            else
                upgradeText.text = "MAX LEVEL";
        }
    }
}
