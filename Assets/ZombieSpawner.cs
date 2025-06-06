using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Zombie zombiePrefab;
    [SerializeField] private uint zombieCount = 5;
    [SerializeField] private float spawnOffset = 1f;

    // private void Start()
    // {
    //     SpawnZombies();
    // }

    public void SpawnZombies()
    {
        StartCoroutine(Routine());

        IEnumerator Routine()
        {
            for (int i = 0; i < zombieCount; i++)
            {
                Vector3 offset = new Vector3(
                    Random.Range(-spawnOffset, spawnOffset),
                    0,
                    Random.Range(-spawnOffset, spawnOffset));

                yield return new WaitForSeconds(0.25f);
            
                SpawnObjectAt(transform.position + offset, transform.rotation);
            } 
        }
    }

    void SpawnObjectAt(Vector3 position, Quaternion rotation)
    {
        Instantiate(zombiePrefab.gameObject, position, rotation); 
    }
}
