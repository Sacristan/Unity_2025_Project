using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthLabel;
    
    Player _player;
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.OnHealthChanged += OnPlayerHealthChanged;
        OnPlayerHealthChanged(_player.Health);
    }

    private void OnPlayerHealthChanged(float health)
    {
        healthLabel.text = $"Health: {health}";
    }
}
