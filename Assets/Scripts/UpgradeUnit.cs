using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUnit : MonoBehaviour
{
    public UnitLevel[] unitLevels; // how to make a double array that shows up in the inspector

    [System.Serializable]
    public class UnitLevel
    {
        public UnitStats[] levels;
    }

    static List<Dictionary<UnitStats, UnitStats>> statChains = new List<Dictionary<UnitStats, UnitStats>>();

    void Start()
    {
        InitializeDictionaries();
    }

    void InitializeDictionaries()
    {
        foreach (UnitLevel ul in unitLevels)
        {
            Dictionary<UnitStats, UnitStats> newDictionary = new Dictionary<UnitStats, UnitStats>();
            for (int i = 0; i < ul.levels.Length - 1; i++)
            {
                newDictionary.Add(ul.levels[i], ul.levels[i + 1]);
            }
            statChains.Add(newDictionary);
        }
    }

    public static UnitStats Upgrade(UnitStats stats)
    {
        for (int i = 0; i < statChains.Count; i++)
        {
            if (statChains[i].TryGetValue(stats, out UnitStats newStats))
            {
                return newStats;
            }
        }
        return stats;
    }
}
