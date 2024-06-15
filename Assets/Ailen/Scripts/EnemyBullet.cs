using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Wall"�� �� ���� ȿ�� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth _healthBar = collision.gameObject.GetComponentInChildren<PlayerHealth>();
            _healthBar.UpdateHealthBar(5, Vector3.up);


            // ���� ȿ�� ���� �� ���� ����

        }

        if (collision.gameObject.CompareTag("Tower"))
        {
            

            HealthBar _healthBar = collision.gameObject.GetComponentInChildren<HealthBar>();
            _healthBar.UpdateHealthBar(5, Vector3.up);


            // ���� ȿ�� ���� �� ���� ����
            
        }
        if (collision.gameObject.CompareTag("Lotte"))
        {

            PlayerHealth _healthBar = collision.gameObject.GetComponentInChildren<PlayerHealth>();
            _healthBar.UpdateHealthBar(5, Vector3.up);


            // ���� ȿ�� ���� �� ���� ����

        }
        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject effect in effects)
        {
            Destroy(effect);
        }

        Destroy(gameObject);
    }
}
