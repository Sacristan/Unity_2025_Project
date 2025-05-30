using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    
    float health = 100f;
    public float Health => health;

    public void OnReceiveDamage(float damage, Animal from)
    {
        Debug.Log($"Player received {damage} damage from {from.gameObject.name}");
        health -= damage;
        OnHealthChanged?.Invoke(health);
        
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        throw new System.NotImplementedException();
    }
}