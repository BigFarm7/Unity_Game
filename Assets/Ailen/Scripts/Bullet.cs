using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public GameObject impactEffectPrefab; // �浹 �� ���� ȿ�� ������

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Wall"�� �� ���� ȿ�� ���
        if (collision.gameObject.CompareTag("Wall"))
        {
            ContactPoint contact = collision.contacts[0]; // �浹 ���� ���� ��������
            Vector3 impactDirection = contact.normal; // �浹 ������ ���� ������ �ݴ� ����


            // ���� ȿ�� ���� �� ���� ����
            GameObject impactEffect = Instantiate(impactEffectPrefab, contact.point, Quaternion.identity);
            impactEffect.transform.forward = impactDirection;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ContactPoint contact = collision.contacts[0]; // �浹 ���� ���� ��������
            Vector3 impactDirection = contact.normal; // �浹 ������ ���� ������ �ݴ� ����

            HealthBar _healthBar = collision.gameObject.GetComponentInChildren<HealthBar>();
            _healthBar.UpdateHealthBar(5,impactDirection);


            // ���� ȿ�� ���� �� ���� ����
            GameObject impactEffect = Instantiate(impactEffectPrefab, contact.point, Quaternion.identity);
            impactEffect.transform.forward = impactDirection;
        }
        if (collision.gameObject.CompareTag("Spider"))
        {
            ContactPoint contact = collision.contacts[0]; // �浹 ���� ���� ��������
            Vector3 impactDirection = contact.normal; // �浹 ������ ���� ������ �ݴ� ����

            HealthBar _healthBar = collision.gameObject.GetComponentInChildren<HealthBar>();
            _healthBar.UpdateHealthBar(5, impactDirection);


            // ���� ȿ�� ���� �� ���� ����
            GameObject impactEffect = Instantiate(impactEffectPrefab, contact.point, Quaternion.identity);
            impactEffect.transform.forward = impactDirection;
        }
        GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (GameObject effect in effects)
        {
            Destroy(effect);
        }

        Destroy(gameObject);
    }
    

}
