using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPlacer : MonoBehaviour
{
    public delegate void UnitPlaced(UnitStats unitInfo);
    public UnitPlaced unitPlaced;

    UnPurchasedUnit heldUnit;
    GameObject previewObject;
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void CreateAndHold(UnPurchasedUnit unitToHold)
    {
        RemoveHeldUnit();
        heldUnit = unitToHold;
        // instantiate a preview of the unit with no logic in it
        previewObject = Instantiate(unitToHold.previewableUnit, Vector3.one * 1000, Quaternion.identity);
        // get the preview to follow your cursor
        StartCoroutine(DragUnit());
        StartCoroutine(CheckInputs());
    }

    void RemoveHeldUnit()
    {
        StopAllCoroutines();
        if (previewObject != null)
            Destroy(previewObject);
        heldUnit = null;
    }

    IEnumerator DragUnit()
    {
        while (true)
        {
            yield return null;
            // moves the preview to the mouses position at the end of the frame
            if (previewObject != null)
            {
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 25f))
                {
                    previewObject.transform.position = hit.point + Vector3.up;
                }
            }
        }
    }

    IEnumerator CheckInputs()
    {
        while (true)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                RemoveHeldUnit();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (PlaceUnit())
                {
                    RemoveHeldUnit();
                    StopAllCoroutines();
                }
            }

            yield return null;
        }
    }

    bool PlaceUnit()
    {
        UnitStats stats = heldUnit.GetStats();
        // checks the place under your cursor and returns a RaycastHit
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 25f))
        {
            // checks if the collider that got hit is suitable for the unit
            if (hit.collider.CompareTag(stats.terrainTag))
            {
                // collects all the colliders within a sphere
                Collider[] Obstacles = Physics.OverlapSphere(hit.point, stats.size);
                foreach (Collider col in Obstacles)
                {
                    // checks all the gathered colliders and cancels function if the unit can't fit
                    if (!col.CompareTag(stats.terrainTag))
                    {
                        if (!col.CompareTag("Ground"))
                        {
                            return false;
                        }
                    }
                }
                // after passing all checks for a suitable spawn, spawn a copy of the prefab
                Instantiate(heldUnit.unit, hit.point + (Vector3.up * 0.5f), Quaternion.identity);
                // send a notice to all subscribed scripts
                unitPlaced?.Invoke(stats);
                // deduct money
                Money.Remove(stats.price);
                RemoveHeldUnit();
                return true;
            }
        }
        return false;
    }
}
