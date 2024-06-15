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
    public int goldReward = 10; // �׾��� �� �ִ� ��� ����
    public RagdollChanger Rag; // RagdollChanger ��ũ��Ʈ ����

    void Start()
    {
        _cam = Camera.main;
        currentHealth = maxHealth;
        _gameObject = this.gameObject;
    }

    // �������� �޾��� �� ȣ��Ǵ� �޼ҵ�
    public void UpdateHealthBar(float damage, Vector3 direction)
    {
        currentHealth -= damage;
        _healthbarSprite.fillAmount = currentHealth / maxHealth;

        // ü���� 0 ������ �� ó��
        if (currentHealth <= 0)
        {
            // ��� ������ �߰�


            // RagdollChanger�� �̿��Ͽ� ������ ó��
            if (_gameObject.CompareTag("Enemy"))
            {
                if (_gameObject.name == "Spider")
                {
                    GameManager.instance.AddGold(goldReward);
                    Destroy(transform.parent.gameObject); // Spider�� �θ� ������Ʈ�� ����
                   
                }
                else
                {
                    Rag.ChangeRagdoll(direction);
                    gameObject.SetActive(false); // Alien�� ��Ȱ��ȭ�� ����
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
        // healthBar�� ������ ī�޶� �������� ����
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }
}