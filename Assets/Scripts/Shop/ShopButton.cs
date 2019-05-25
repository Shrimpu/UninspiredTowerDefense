using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ShopButton : MonoBehaviour, IPointerClickHandler
{
    public UnPurchasedUnit item;
    public TMP_Text text;

    int price;
    private UnitPlacer placer;

    private void Awake()
    {
        placer = FindObjectOfType<UnitPlacer>();
    }

    private void Start()
    {
        price = item.GetStats().price;
        text = Instantiate(text, transform);
        UnitStats s = item.GetStats();
        text.text = item.unit.name + "\nCost: " + s.price.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (price <= Money.MoneyOwned)
        {
            placer.CreateAndHold(item);
        }
        else
            print("Can't afford the fucking thing");
    }
}
