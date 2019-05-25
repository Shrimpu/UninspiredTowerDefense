using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyTracker
{
    static List<GameObject> enemies = new List<GameObject>();

    public enum TargetType { First, Strong, Close, Last }

    public static void AddEnemy(GameObject enemyToAdd)
    {
        enemies.Add(enemyToAdd);
    }

    public static void RemoveEnemy(GameObject enemyToRemove)
    {
        if (enemies.Contains(enemyToRemove))
            enemies.Remove(enemyToRemove);
    }

    public static GameObject[] GetAllActiveEnemies()
    {
        return enemies.ToArray();
    }

    public static bool FindAllEnemiesInRange(Vector3 position, float range, out Transform[] enemies)
    {
        enemies = null;

        Collider[] enemiesInRange = Physics.OverlapSphere(position, range, LayerMask.GetMask("Enemy"));

        if (enemiesInRange.Length == 0)
        {
            return false;
        }
        enemies = new Transform[enemiesInRange.Length];
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            enemies[i] = enemiesInRange[i].transform;
        }
        return true;
    }

    public static bool FindEnemy(Vector3 position, float range, out Transform enemy, TargetType targetType = TargetType.First)
    {
        bool enemyFound = false;
        enemy = null;
        switch (targetType)
        {
            case TargetType.First:
                enemyFound = FindFirstEnemy(position, range, out enemy);
                break;
            case TargetType.Strong:
                enemyFound = FindStrongestEnemy(position, range, out enemy);
                break;
            case TargetType.Close:
                enemyFound = FindClosestEnemy(position, range, out enemy);
                break;
            case TargetType.Last:
                enemyFound = FindLastEnemy(position, range, out enemy);
                break;
        }
        return enemyFound;
    }

    public static bool FindClosestEnemy(Vector3 position, float range, out Transform enemy)
    {
        enemy = null;
        float closestDistance = range + 1;

        Collider[] enemiesInRange = Physics.OverlapSphere(position, range, LayerMask.GetMask("Enemy"));

        if (enemiesInRange.Length == 0)
        {
            return false;
        }

        foreach (Collider e in enemiesInRange)
        {
            float distance = (e.gameObject.transform.position - position).magnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                enemy = e.gameObject.transform;
            }
        }

        return true;
    }

    public static bool FindFirstEnemy(Vector3 position, float range, out Transform enemy)
    {
        enemy = null;
        float progress = 0;

        Collider[] enemiesInRange = Physics.OverlapSphere(position, range, LayerMask.GetMask("Enemy"));
        if (enemiesInRange.Length == 0)
            return false;

        foreach (Collider e in enemiesInRange)
        {
            if (e.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled > progress)
            {
                progress = e.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled;
                enemy = e.gameObject.transform;
            }
        }
        return true;
    }

    public static bool FindLastEnemy(Vector3 position, float range, out Transform enemy)
    {
        enemy = null;

        Collider[] enemiesInRange = Physics.OverlapSphere(position, range, LayerMask.GetMask("Enemy"));
        if (enemiesInRange.Length == 0)
            return false;
        float progress = enemiesInRange[0].gameObject.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled;

        if (enemiesInRange.Length > 1)
        {
            for (int i = 1; i < enemiesInRange.Length; i++)
            {
                if (enemiesInRange[i].GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled < progress)
                {
                    progress = enemiesInRange[i].GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled;
                    enemy = enemiesInRange[i].gameObject.transform;
                }
            }
        }
        return true;
    }

    public static bool FindStrongestEnemy(Vector3 position, float range, out Transform enemy)
    {
        enemy = null;
        float highestDangerValue = 0;
        float progress = 0;

        Collider[] enemiesInRange = Physics.OverlapSphere(position, range, LayerMask.GetMask("Enemy"));
        if (enemiesInRange.Length == 0)
            return false;

        foreach (Collider e in enemiesInRange)
        {
            Enemy enemyComponent = e.GetComponent<Enemy>();

            if (enemyComponent.stats.dangerLevel > highestDangerValue)
            {
                progress = e.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled;
                enemy = e.gameObject.transform;
            }
            else if (enemyComponent.stats.dangerLevel == highestDangerValue &&
                e.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled > progress)
            {
                progress = e.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled;
                enemy = e.gameObject.transform;
            }
        }

        return true;
    }
}