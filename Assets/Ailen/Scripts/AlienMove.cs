using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AlienMove : MonoBehaviour
{

    public GameObject finalWayPoint; // 중앙 지점의 GameObject
    public float moveSpeed = 5f; // 이동 속도
    public float detectionRange = 40f; // 플레이어 또는 포탑 탐지 범위
    public LayerMask detectionLayer; // 탐지할 레이어 (예: 플레이어와 포탑 레이어)
    public float attackRange = 8f; // 공격 범위
    private GameObject currentTarget;
    private Animator animator;
    void Start()
    {
        // FinalPoint라는 이름의 GameObject를 찾아서 할당합니다.
        finalWayPoint = GameObject.Find("FinalPoint");
        currentTarget = finalWayPoint;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 탐지 가능한 객체를 찾습니다.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);

        if (hitColliders.Length > 0)
        {
            // 탐지된 객체 중 첫 번째 객체를 목표로 설정합니다.
            currentTarget = hitColliders[0].gameObject;
        }
        else
        {
            // 탐지된 객체가 없으면 FinalPoint로 이동합니다.
            currentTarget = finalWayPoint;
        }

        // currentTarget이 설정되지 않은 경우에 대비한 체크
        if (currentTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
                {
                    if (distanceToTarget > attackRange)
                    {
                        animator.SetBool("Attack", false);
                    }
                }
               
                return;
            }
            // 방향 벡터를 계산합니다.
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;

            // 방향을 따라 이동합니다.
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 방향을 따라 회전합니다.
            transform.rotation = Quaternion.LookRotation(direction);

           

            // 공격 범위 내에 있는 경우 공격 애니메이션 실행
            if (distanceToTarget <= attackRange)
            {
                animator.SetBool("Attack",true);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // 목표를 달성한 경우 (예: 포탑을 부순 경우) 다시 finalWayPoint로 이동합니다.
        if (other.gameObject == currentTarget)
        {
            // 현재 목표가 플레이어나 포탑이면 해당 목표를 처리한 후 finalWayPoint로 변경
            if (other.gameObject != finalWayPoint)
            {
                // 목표를 처리하는 로직 (예: 포탑 파괴) 여기에 추가 가능
                Destroy(other.gameObject); // 예: 포탑 파괴

                // 목표를 달성하면 finalWayPoint로 이동
                currentTarget = finalWayPoint;
            }
        }
    }
    
    
}
