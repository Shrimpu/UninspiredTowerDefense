using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public delegate void MoneyUpdate();
    public static MoneyUpdate moneyUpdated;

    public static int MoneyOwned { get; private set; }

    public static void Add(int amount)
    {
        if (amount < 0)
        {
            Remove(-amount);
            return;
        }
        MoneyOwned += amount;
        moneyUpdated?.Invoke();
    }

    public static bool Remove(int amount)
    {
        if (MoneyOwned - amount >= 0)
        {
            MoneyOwned -= amount;
            moneyUpdated?.Invoke();
            return true;
        }
        return false;
    }
}
