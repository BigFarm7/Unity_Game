using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
 
    public BoxCollider attackCollider; // ���� ������ �����ϴ� BoxCollider

    void Start()
    {
        // ���� ���� �ݶ��̴��� ��Ȱ��ȭ ���·� ����
        attackCollider.enabled = false;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��� �Լ�
    public void CheckAttackHit()
    {
        // ���� ���� �ݶ��̴��� Ȱ��ȭ
        attackCollider.enabled = true;

        // ���� ���� ���� �ִ� ��� �ݶ��̴��� ������
        Collider[] hitColliders = Physics.OverlapBox(
            attackCollider.bounds.center,
            attackCollider.bounds.extents,
            attackCollider.transform.rotation
        );

        // ���� ���� ���� �÷��̾��� �ݶ��̴��� �ִ��� Ȯ��
        foreach (Collider hitCollider in hitColliders)
        {
            if ((hitCollider.gameObject.tag =="Player"))
            {
                PlayerHealth Health = hitCollider.GetComponent<PlayerHealth>();
              
                    Health.UpdateHealthBar(5,Vector3.up);
                
            }
            if ((hitCollider.gameObject.tag == "Tower"))
            {
                HealthBar Health = hitCollider.GetComponentInChildren<HealthBar>();
                Health.UpdateHealthBar(5,Vector3.up);
            }
            if ((hitCollider.gameObject.tag == "Lotte"))
            {
                PlayerHealth Health = hitCollider.GetComponent<PlayerHealth>();

                Health.UpdateHealthBar(5, Vector3.up);
            }
        }

        // ���� ���� �ݶ��̴��� ��Ȱ��ȭ
        attackCollider.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackCollider != null)
        {
            // ���� ������ �ð������� ǥ�� (����׿�)
            Gizmos.color = Color.red;
            Gizmos.matrix = attackCollider.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(attackCollider.center, attackCollider.size);
        }
    }
}
