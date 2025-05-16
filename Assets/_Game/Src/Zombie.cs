using System;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Animal
{
    Player _player;
    NavMeshAgent _navMeshAgent;

    [SerializeField] private float closeEnoughDistance = 3f;
    
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) > closeEnoughDistance)
        {
            UpdatePlayerTarget();
        }
        else
        {
            StopMovement();
        }
    }

    void UpdatePlayerTarget()
    {
        _navMeshAgent.destination = _player.transform.position;
    }

    void StopMovement()
    {
        _navMeshAgent.velocity = Vector3.zero;
    }
}