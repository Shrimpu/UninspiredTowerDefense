using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelected : MonoBehaviour
{
    public delegate void UnitSelectedUpdate(bool selected, Unit unit);
    public static UnitSelectedUpdate selectionUpdated;

    public GameObject circle2D;
    static GameObject circle2DInstance;

    Camera cam;
    static Unit selectedUnit;

    private void Start()
    {
        cam = Camera.main;
        circle2DInstance = Instantiate(circle2D, transform);
        circle2DInstance.SetActive(false);
        circle2DInstance.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Deselect();
        }
    }

    public static void Select(Unit unitToSelect)
    {
        if (selectedUnit != unitToSelect)
        {
            Deselect();
            selectedUnit = unitToSelect;
            DisplayRange(true);
            selectionUpdated?.Invoke(true, selectedUnit);
        }
        else
        {
            Deselect();
        }
    }

    static void Deselect()
    {
        if (selectedUnit != null)
        {
            DisplayRange(false);
            selectedUnit = null;
            selectionUpdated(false, selectedUnit);
        }
    }

    static void DisplayRange(bool display)
    {
        circle2DInstance.transform.position = selectedUnit.transform.position;
        circle2DInstance.transform.localScale = Vector3.one * selectedUnit.stats.range * 2f;
        circle2DInstance.SetActive(display);
    }
}
