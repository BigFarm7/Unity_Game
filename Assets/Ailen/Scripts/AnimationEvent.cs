using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
 
    public BoxCollider attackCollider; // 공격 범위를 정의하는 BoxCollider

    void Start()
    {
        // 공격 범위 콜라이더가 비활성화 상태로 시작
        attackCollider.enabled = false;
    }

    // 애니메이션 이벤트에서 호출될 함수
    public void CheckAttackHit()
    {
        // 공격 범위 콜라이더를 활성화
        attackCollider.enabled = true;

        // 공격 범위 내에 있는 모든 콜라이더를 가져옴
        Collider[] hitColliders = Physics.OverlapBox(
            attackCollider.bounds.center,
            attackCollider.bounds.extents,
            attackCollider.transform.rotation
        );

        // 공격 범위 내에 플레이어의 콜라이더가 있는지 확인
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

        // 공격 범위 콜라이더를 비활성화
        attackCollider.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackCollider != null)
        {
            // 공격 범위를 시각적으로 표시 (디버그용)
            Gizmos.color = Color.red;
            Gizmos.matrix = attackCollider.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(attackCollider.center, attackCollider.size);
        }
    }
}
