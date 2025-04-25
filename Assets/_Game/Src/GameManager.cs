using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event System.Action OnGameWon; 
    
    public static GameManager instance;

    private List<TargetTrigger> _activeTargetTriggers = new();
    
    bool gameWonTriggered = false;
    
    AudioSource _audioSource;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _activeTargetTriggers = new List<TargetTrigger>(FindObjectsOfType<TargetTrigger>());
        TargetTrigger.OnAllTargetsShot += OnTargetTriggerDone;
    }

    private void OnDestroy()
    {
        TargetTrigger.OnAllTargetsShot -= OnTargetTriggerDone;
    }

    void OnTargetTriggerDone(TargetTrigger trigger)
    {
        Debug.Log(nameof(OnTargetTriggerDone));
        
        _activeTargetTriggers.Remove(trigger);

        if (_activeTargetTriggers.Count <= 0)
        {
            GameWon();
        }
    }

    void GameWon()
    {
        if (gameWonTriggered) return;
        gameWonTriggered = true;
        Debug.Log("Game Won");
        OnGameWon?.Invoke();
        _audioSource.Play();
        Invoke(nameof(RestartLevel), 3f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}