using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public int startMoney = 75;

    public GameObject panel;
    public TMP_Text moneyText;
    public GameObject button;
    public TMP_Text shopItemText;
    public UnPurchasedUnit[] shopItems;

    private void Start()
    {
        Money.moneyUpdated += DisplayMoney;
        Money.Add(startMoney);
        for (int i = 0; i < shopItems.Length; i++)
        {
            GameObject b = Instantiate(button, panel.transform);
            ShopButton script = b.AddComponent<ShopButton>();
            script.item = shopItems[i];
            script.text = shopItemText;
        }
    }

    public void DisplayMoney()
    {
        moneyText.text = Money.MoneyOwned.ToString();
    }
}
