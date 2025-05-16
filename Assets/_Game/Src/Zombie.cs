using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Zombie : Animal
{
    enum ZombieState
    {
        None,
        Chasing,
        Attacking,
        Dead
    }

    Player _player;
    NavMeshAgent _navMeshAgent;

    [SerializeField] private float closeEnoughDistance = 3f;

    ZombieState _state = ZombieState.None;

    ZombieState CurrentState
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                UpdateState();
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        _player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (CurrentState != ZombieState.Dead)
        {
            if (Vector3.Distance(_player.transform.position, transform.position) > closeEnoughDistance)
            {
                CurrentState = ZombieState.Chasing;
                UpdatePlayerTarget();
            }
            else
            {
                CurrentState = ZombieState.Attacking;
                OnCloseEnoughForAttack();
            }
        }
    }

    void UpdatePlayerTarget()
    {
        _navMeshAgent.destination = _player.transform.position;
    }

    void OnCloseEnoughForAttack()
    {
        StopMovement();
    }

    void UpdateState()
    {
        _animator.SetBool("IsWalking", CurrentState == ZombieState.Chasing);
    }

    void StopMovement()
    {
        _navMeshAgent.velocity = Vector3.zero;
    }

    protected override void Die()
    {
        base.Die();
        StopMovement();
        CurrentState = ZombieState.Dead;
        Destroy(_navMeshAgent);
    }
}