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

            GameObject[] effects = GameObject.FindGameObjectsWithTag("Effect");
            foreach (GameObject effect in effects)
            {
                Destroy(effect);
            }

            // ���� ȿ�� ���� �� ���� ����
            GameObject impactEffect = Instantiate(impactEffectPrefab, contact.point, Quaternion.identity);
            impactEffect.transform.forward = impactDirection;
        }
       

        Destroy(gameObject);
    }
    

}
