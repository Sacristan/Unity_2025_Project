using System;
using System.Collections.Generic;
using InfimaGames.LowPolyShooterPack;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;

    readonly List<Zombie> zombies = new List<Zombie>();
    public int ZombieCount => zombies.Count;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Animal.OnDeath += AnimalOnDeath;
    }

    private void OnDestroy()
    {
        Animal.OnDeath -= AnimalOnDeath;
    }

    public void AddZombie(Zombie zombie)
    {
        zombies.Add(zombie);
        Debug.Log(ZombieCount);
    }

    private void AnimalOnDeath(Animal animal)
    {
        if (animal is Zombie)
        {
            zombies.Remove(animal as Zombie);
            Debug.Log(ZombieCount);
        }
    }
    
}