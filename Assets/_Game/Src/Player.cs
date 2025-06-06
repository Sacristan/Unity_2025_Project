using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfimaGames.LowPolyShooterPack;

public class Player : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    float health = 100f;
    public float Health => health;

    bool isDead = false;

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
        if (isDead) return;
        isDead = true;
        
        OnDeath?.Invoke();
        
        Disable(GetComponent<Movement>());
        Disable(GetComponent<Character>());
        Disable( GetComponentInChildren<CameraLook>());
        
        void Disable(MonoBehaviour behaviour)
        {
            if (behaviour) behaviour.enabled = false;
        }
    }
}