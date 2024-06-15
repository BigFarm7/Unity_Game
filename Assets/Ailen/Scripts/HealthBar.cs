using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    private Camera _cam;
    private GameObject _gameObject;
    public float maxHealth;
    public float currentHealth;
    public int goldReward = 10; // 죽었을 때 주는 골드 보상
    public RagdollChanger Rag; // RagdollChanger 스크립트 참조

    void Start()
    {
        _cam = Camera.main;
        currentHealth = maxHealth;
        _gameObject = this.gameObject;
    }

    // 데미지를 받았을 때 호출되는 메소드
    public void UpdateHealthBar(float damage, Vector3 direction)
    {
        currentHealth -= damage;
        _healthbarSprite.fillAmount = currentHealth / maxHealth;

        // 체력이 0 이하일 때 처리
        if (currentHealth <= 0)
        {
            // 골드 보상을 추가


            // RagdollChanger를 이용하여 적절히 처리
            if (_gameObject.CompareTag("Enemy"))
            {
                if (_gameObject.name == "Spider")
                {
                    GameManager.instance.AddGold(goldReward);
                    Destroy(transform.parent.gameObject); // Spider는 부모 오브젝트를 제거
                   
                }
                else
                {
                    Rag.ChangeRagdoll(direction);
                    gameObject.SetActive(false); // Alien은 비활성화만 수행
                    GameManager.instance.AddGold(goldReward);
                }
            }
            else if (_gameObject.CompareTag("Tower"))
            {
                Destroy(transform.parent.gameObject);
            }
           
        }
    }

    void Update()
    {
        // healthBar의 방향을 카메라 방향으로 설정
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
}