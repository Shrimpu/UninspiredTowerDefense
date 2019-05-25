using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Projectile : MonoBehaviour, IDamage
{
    int damage = 1;
    int pierce = 0;
    float speed = 2f;
    string targetTag = "Enemy";
    Unit parentUnit;

    BoxCollider col;
    Rigidbody rb;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.isKinematic = true;
        col.isTrigger = true;
    }

    public void Setup(int damage, int pierce, float speed, float lifeTime, string targetTag, Unit parentUnit)
    {
        this.damage = damage;
        this.pierce = pierce;
        this.speed = speed;
        this.targetTag = targetTag;
        this.parentUnit = parentUnit;

        StartCoroutine(DeathTimer(lifeTime));
        StartCoroutine(Move());
    }

    IEnumerator DeathTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    IEnumerator Move()
    {
        while (true)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            yield return null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            IHit ihit = other.GetComponent<IHit>();
            if (ihit != null)
            {
                if (ihit.TakeDamage(damage))
                {
                    parentUnit.killCount++;
                }
                pierce--;
                if (pierce < 0)
                    Destroy(gameObject);
            }
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}