using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthLabel;
    [SerializeField] RectTransform deathUIContainer;
    
    Player _player;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.OnHealthChanged += OnPlayerHealthChanged;
        GameManager.instance.OnGameLost += InstanceOnOnGameLost;
        OnPlayerHealthChanged(_player.Health);
    }

    private void OnPlayerHealthChanged(float health)
    {
        healthLabel.text = $"Health: {health}";
    }
    
    private void InstanceOnOnGameLost()
    {
        healthLabel.enabled = false;
        deathUIContainer.gameObject.SetActive(true);
    }
}
