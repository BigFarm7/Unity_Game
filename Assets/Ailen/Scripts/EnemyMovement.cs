using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform finalTarget; // 최종 목표
    public float finalTargetRange = 20f; // 최종 목표를 향해 나아가는 도중 탐지 범위
    public float moveSpeed = 5f; // 이동 속도
    public float rotationSpeed = 5.0f; // 고개를 돌리는 속도
    public GameObject bulletPrefab; // 총알 프리팹
    public Transform bulletSpawnPoint1; // 총알이 발사될 위치 1
    public Transform bulletSpawnPoint2; // 총알이 발사될 위치 2
    public float bulletSpeed = 20.0f; // 총알 속도
    public float shootingInterval = 1.0f; // 발사 간격
    public float attackDistanceThreshold = 15f; // 공격할 거리 임계값
    public float detectionDistanceThreshold = 10f; // 탐지한 오브젝트와의 거리 임계값
    public LayerMask detectionLayer;
    private float lastShotTime; // 마지막으로 총알을 발사한 시간
    private bool isAttackingObject = false; // 오브젝트를 공격 중인지 여부
    private Transform currentTarget; // 현재 공격 대상

    void Start()
    {
        finalTarget = GameObject.Find("FinalPoint").transform;
    }
    void Update()
    {
        // 공격 상태에 따라 처리
        if (isAttackingObject)
        {
            HandleAttackObjectState();
        }
        else
        {

            // 주변 오브젝트 탐지 및 이동
            HandleDetectionAndMove();
        }
    }

    void HandleDetectionAndMove()
    {
        // 주변에 있는 오브젝트 탐지
        Collider[] colliders = Physics.OverlapSphere(transform.position, finalTargetRange,detectionLayer);

        Transform nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
          

            if (collider.gameObject != finalTarget.gameObject)
            {
                // 특정 태그를 가진 오브젝트 탐지
                if (collider.CompareTag("Player") || collider.CompareTag("Tower") || collider.CompareTag("Lotte"))
                {
                    // 오브젝트와의 거리 계산
                    float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);

                    // 가장 가까운 오브젝트를 찾음
                    if (distanceToTarget < nearestDistance)
                    {
                        nearestDistance = distanceToTarget;
                        nearestTarget = collider.transform;
                    }
                }
            }
        }

      

        // 가장 가까운 오브젝트를 찾았을 경우 공격 상태로 전환
        if (nearestTarget != null)
        {
            StopMovingToFinalTarget();
            currentTarget = nearestTarget;
            isAttackingObject = true;
        }
        else
        {
            // 최종 목표로 이동
            MoveToFinalTarget();
        }
    }

    void MoveToFinalTarget()
    {
        if (finalTarget == null) return;
        // 최종 목표 방향 계산
        Vector3 targetDirection = (finalTarget.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));

        // 고개를 목표 방향으로 돌립니다.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // 최종 목표 방향으로 이동합니다.
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void StopMovingToFinalTarget()
    {
        // 최종 목표로 이동을 멈춥니다.
        // 필요에 따라 추가적인 정지 처리를 여기에 추가a할 수 있습니다.
    }

    void HandleAttackObjectState()
    {
        if (currentTarget == null || !isAttackingObject)
        {
            // 공격 대상이 없거나 공격 상태가 아니면 공격 상태를 해제하고 최종 목표로 이동합니다.
            isAttackingObject = false;
            currentTarget = null;
            return;
        }

        // 탐지한 오브젝트와의 거리를 확인하여 일정 거리 이상 멀어지면 최종 목표로 이동합니다.
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
        if (distanceToTarget > detectionDistanceThreshold)
        {
            isAttackingObject = false; // 공격 상태 해제
            currentTarget = null;
            MoveToFinalTarget(); // 최종 목표로 이동
            return;
        }

        // 공격 대상을 향해 고개를 돌립니다.
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // 발사 간격이 지났다면 총을 발사합니다.
        if (Time.time - lastShotTime > shootingInterval)
        {
            // 두 개의 총알 발사 지점에서 발사합니다.
            ShootAtTarget(bulletSpawnPoint1);
            ShootAtTarget(bulletSpawnPoint2);

            lastShotTime = Time.time; // 총알 발사 시간 기록
        }
    }

    void ShootAtTarget(Transform spawnPoint)
    {
        // 총알 발사 위치 설정 (총구 위치에서 나가도록)
        Vector3 shootDirection = spawnPoint.forward; // 총알이 나가야 할 방향은 총구의 forward 방향
        Quaternion shootRotation = Quaternion.LookRotation(shootDirection);

        // 총알 생성 및 발사
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, shootRotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

        if (bulletRB != null)
        {
            bulletRB.velocity = shootDirection * bulletSpeed;
        }
    }
}