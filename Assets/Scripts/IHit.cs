using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    int Health { get; set; }

    bool TakeDamage(int damage);
    void Die();
}
