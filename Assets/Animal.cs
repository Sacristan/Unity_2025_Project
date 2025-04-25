using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour, IShootable
{
    Animator _animator;

    private bool isDead = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void GotShot()
    {
        // Debug.Log(gameObject.name + " GOT SHOT");
        Die();
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            _animator.SetTrigger("Die");
            
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
        }
    }
}