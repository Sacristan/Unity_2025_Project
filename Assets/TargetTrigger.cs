using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    public static event System.Action<TargetTrigger> OnAllTargetsShot;
    
    bool hasRaisedTargets = false;
    private Collider _collider;

    [SerializeField] float raiseDelay = 0.5f;
    [SerializeField] private TargetScript[] targets;

    private List<TargetScript> activeTargets;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        activeTargets = new List<TargetScript>(targets);

        for (int i = 0; i < activeTargets.Count; i++)
        {
            targets[i].Init(this);
        }
    }

    public void HandleTargetShot(TargetScript target)
    {
        activeTargets.Remove(target);
        if (activeTargets.Count <= 0) OnAllTargetsShot?.Invoke(this);
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