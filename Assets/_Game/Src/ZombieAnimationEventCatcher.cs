using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieAnimationEventCatcher : MonoBehaviour
{
    Zombie _zombie;
    
    void Start()
    {
        _zombie = GetComponentInParent<Zombie>();
    }
    
    public void OnHitDamage()
    {
        _zombie.OnHitDamage();
    }
}