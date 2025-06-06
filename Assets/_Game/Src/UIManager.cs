using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthLabel;
    [SerializeField] RectTransform deathUIContainer;

    [Header("Damage Indication")]
    [SerializeField] Image damageIndicator;
    [SerializeField] float damageIndicationDuration = 1.5f;

    private Color damageIndicatorColor;
    private Color damageTargetIndicatorColor;
    Coroutine damageIndicatorCoroutine;
    Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.OnHealthChanged += (health) => OnPlayerHealthChanged(health, showDamageIndicator: true);
        GameManager.instance.OnGameLost += InstanceOnOnGameLost;

        damageIndicatorColor = damageIndicator.color;
        damageTargetIndicatorColor = damageIndicator.color;
        damageTargetIndicatorColor.a = 0f;

        OnPlayerHealthChanged(_player.Health, showDamageIndicator: false);
    }

    private void OnPlayerHealthChanged(float health, bool showDamageIndicator = true)
    {
        healthLabel.text = $"Health: {health}";
        if (showDamageIndicator) ShowDamageIndicator();
    }

    private void InstanceOnOnGameLost()
    {
        healthLabel.enabled = false;
        deathUIContainer.gameObject.SetActive(true);
    }

    void ShowDamageIndicator()
    {
        if (damageIndicatorCoroutine != null) StopCoroutine(damageIndicatorCoroutine);
        damageIndicatorCoroutine = StartCoroutine(DamageIndicatorRoutine());

        IEnumerator DamageIndicatorRoutine()
        {
            float t = 0f;

            damageIndicator.color = damageIndicatorColor;
            damageIndicator.enabled = true;

            while (t <= 1f)
            {
                t += Time.deltaTime / damageIndicationDuration;
                damageIndicator.color = Color.Lerp(damageIndicatorColor, damageTargetIndicatorColor, t);
                yield return null;
            }

            damageIndicator.enabled = false;
        }
    }
}