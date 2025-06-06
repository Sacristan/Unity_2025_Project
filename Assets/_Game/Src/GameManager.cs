using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event System.Action OnGameWon;
    public event System.Action OnGameLost;

    public static GameManager instance;

    private List<TargetTrigger> _activeTargetTriggers = new();

    bool gameWonTriggered = false;

    AudioSource _audioSource;
    ZombieSpawner _zombieSpawner;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _activeTargetTriggers = new List<TargetTrigger>(FindObjectsOfType<TargetTrigger>());
        TargetTrigger.OnAllTargetsShot += OnTargetTriggerDone;
        Player _player = FindObjectOfType<Player>();
        _zombieSpawner = FindObjectOfType<ZombieSpawner>();
        _player.OnDeath += PlayerOnDeath;
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
            OnAllTargetsShot();
        }
    }

    void OnAllTargetsShot()
    {
        _zombieSpawner.SpawnZombies();
        StartCoroutine(WaitUntilNoZombies());

        IEnumerator WaitUntilNoZombies()
        {
            do
            {
                yield return new WaitForSeconds(1f);
            } while (ZombieManager.instance.ZombieCount > 0);

            // yield return new WaitUntil(() => ZombieManager.instance.ZombieCount <= 0);
            
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

    private void PlayerOnDeath()
    {
        OnGameLost?.Invoke();
        Invoke(nameof(RestartLevel), 3f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}