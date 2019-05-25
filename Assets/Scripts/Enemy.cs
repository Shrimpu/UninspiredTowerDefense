using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

[RequireComponent(typeof(PathFollower))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour, IHit, IClickable
{
    public EnemyStats stats;

    public AudioClip clickClip;
    AudioSource au;

    public int Health { get; set; }

    private void Awake()
    {
        au = GetComponent<AudioSource>();
    }

    void Start()
    {
        Health = stats.dangerLevel;
        GetComponent<PathFollower>().speed = stats.speed;
        EnemyTracker.AddEnemy(gameObject);
        au.clip = clickClip;
    }

    public bool TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    public void Die()
    {
        EnemyTracker.RemoveEnemy(gameObject);
        Money.Add(stats.moneyDrop);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        // prevents error on shutdown :)
        Invoke("HurtPlayer", 0.1f);
    }

    void HurtPlayer()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.GetComponent<IHit>().TakeDamage(stats.dangerLevel);
        EnemyTracker.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        au.Play(0);
    }
}
