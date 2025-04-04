using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    bool hasRaisedTargets = false;
    private Collider _collider;

    [SerializeField] float raiseDelay = 0.5f;
    [SerializeField] private TargetScript[] targets;

    List<TargetScript> activeTargets = new List<TargetScript>();

    private void Start()
    {
        _collider = GetComponent<Collider>();
        activeTargets = new List<TargetScript>(targets);

        foreach (var target in activeTargets)
        {
            target.Init(this);
        }
    }

    public void OnTargetShot(TargetScript target)
    {
        activeTargets.Remove(target);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(nameof(TargetTrigger) + " -> " + collider.gameObject.name);

        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone");
            TryRaiseTargets();
        }
    }

    void TryRaiseTargets()
    {
        if (hasRaisedTargets)
        {
            return;
        }

        hasRaisedTargets = true;
        _collider.enabled = false;

        Debug.Log("Raise Targets");

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].Raise(raiseDelay);
        }
    }
}