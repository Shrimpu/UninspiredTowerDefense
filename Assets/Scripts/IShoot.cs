using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot
{
    bool CanShoot { get; set; }

    IEnumerator Shoot();
    void SpawnProjectiles();
}
