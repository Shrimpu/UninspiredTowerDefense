using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour, IHit
{
    public delegate void HealthReachedZero();
    public static HealthReachedZero healthReachedZero;

    public TMP_Text healthtext;
    public int startHealth;
    public int Health { get; set; }

    void Start()
    {
        Health = startHealth;
        UpdateHealthtext();
    }

    public bool TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
            Health = 0;
        UpdateHealthtext();
        if (Health == 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void UpdateHealthtext()
    {
        healthtext.text = Health.ToString();
    }

    public void Die()
    {
        // Invokes a delegate to get someone else to do something about it
        healthReachedZero?.Invoke();
        enabled = false;
    }
}