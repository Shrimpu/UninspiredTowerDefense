using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IShoot, IClickable
{
    public UnitStats stats;
    public int killCount = 0;

    public EnemyTracker.TargetType targetType = EnemyTracker.TargetType.First;
    [Space]
    public Transform[] firepoint;
    public bool canRotate = true;
    public virtual bool CanRotate { get { return canRotate; } set { canRotate = value; } }

    protected Coroutine follow;

    public virtual bool CanShoot { get; set; } = true;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    public virtual IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / stats.firerate);
            if (EnemyTracker.FindEnemy(transform.position, stats.range, out Transform enemy, targetType)) // get enemy
            {
                if (CanRotate)
                {
                    RotateTowardsTarget(enemy.position);
                    if (follow != null)
                        StopCoroutine(follow);
                    follow = StartCoroutine(FollowTarget(enemy));
                }
                SpawnProjectiles();
            }
        }
    }

    public virtual void SpawnProjectiles()
    {
        for (int i = 0; i < firepoint.Length; i++)
        {
            GameObject projectile = Instantiate(stats.projectile, firepoint[i].position, firepoint[i].rotation);
            SetUpProjectile(projectile);
        }
        if (firepoint.Length == 0)
        {
            GameObject projectile = Instantiate(stats.projectile, transform.position, transform.rotation);
            SetUpProjectile(projectile);
        }
    }

    public virtual void SetUpProjectile(GameObject projectile)
    {
        projectile.AddComponent<Projectile>().Setup(stats.damage, stats.pierce, stats.projectileSpeed,
                stats.range / stats.projectileSpeed + stats.extraLifeTime, "Enemy", this);
    }

    public virtual IEnumerator FollowTarget(Transform target)
    {
        while (true)
        {
            yield return null;
            if (CanRotate && target != null)
            {
                if ((target.position - transform.position).magnitude < stats.range)
                    RotateTowardsTarget(target.position);
                else break;
            }
            else break;
            yield return null;
        }
    }

    protected virtual void RotateTowardsTarget(Vector3 target)
    {
        if (CanRotate)
        {
            Vector3 dir = target - transform.position;
            dir.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = lookRotation;
        }
    }

    public void OnMouseDown()
    {
        UnitSelected.Select(this);
    }
}
