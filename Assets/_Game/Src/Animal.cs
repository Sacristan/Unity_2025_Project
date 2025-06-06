using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour, IShootable
{
    public static event Action<Animal> OnDeath;
    
    protected Animator _animator;
    private bool isDead = false;

    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void GotShot()
    {
        // Debug.Log(gameObject.name + " GOT SHOT");
        Die();
    }

    protected virtual void Die()
    {
        if (!isDead)
        {
            isDead = true;
            _animator.SetTrigger("Die");
            
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<Rigidbody>());

            OnDeath?.Invoke(this);
        }
    }
}