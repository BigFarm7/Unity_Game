using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    public GameManager _manager;
    public float maxHealth;
    public float currentHealth;
    public void UpdateHealthBar(float damage, Vector3 direction)
    {
        currentHealth -= damage;
        _healthbarSprite.fillAmount = currentHealth / maxHealth;

        // 체력이 0 이하일 때 처리
        if (currentHealth <= 0)
        {
            _manager.RestartGame();
        }
    }
}
