using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpreadUnit : RadiusEffectUnit
{
    public override void AreaEffect(Transform[] enemies)
    {
        float arcLength = 360f / ((CircleSpreadUnitStats)stats).firepoints;
        for (int i = 0; i < ((CircleSpreadUnitStats)stats).firepoints; i++)
        {
            GameObject projectile = Instantiate(stats.projectile, transform.position, Quaternion.Euler(0, arcLength * i, 0));
            SetUpProjectile(projectile);
        }
    }
}
