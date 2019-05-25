using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusEffectUnit : Unit
{
    public override IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / stats.firerate);
            if (EnemyTracker.FindAllEnemiesInRange(transform.position, stats.range, out Transform[] enemies))
            {
                AreaEffect(enemies);
            }
        }
    }

    public virtual void AreaEffect(Transform[] enemies)
    {
        SpawnProjectiles();
    }
}
