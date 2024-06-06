using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; // 이동할 목표지점 배열
    public float moveSpeed = 5f; // 이동 속도
    public float detectionRange = 10f; // 플레이어 탐지 범위
    public float rotationSpeed = 120f; // 회전 속도 (각도/초)
    public Transform player; // 플레이어 Transform
    public GameObject bulletPrefab; // 총알 프리팹
    public float bulletSpeed = 10f; // 총알 속도
    public float fireRate = 1f; // 총알 발사 간격
    public Transform ShootPosition1; // 총알 발사 위치 (캐릭터 머리 위치)
    public Transform ShootPosition2;

    private int currentWaypointIndex = 0; 
    private bool isPlayerDetected = false; 
    private Coroutine movementCoroutine; 
    private Coroutine fireCoroutine;
    private Coroutine shootCoroutine;
    void Start()
    {

        movementCoroutine = StartCoroutine(MoveToNextWaypoint());
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            if (!isPlayerDetected)
            {
                isPlayerDetected = true;
                StopCoroutine(movementCoroutine);
                fireCoroutine = StartCoroutine(Follow());
                shootCoroutine = StartCoroutine(Shoot());
            }
        }
        else
        {
            if (isPlayerDetected)
            {
                isPlayerDetected = false;
                StopCoroutine(fireCoroutine);
                StopCoroutine(shootCoroutine);
                movementCoroutine = StartCoroutine(MoveToNextWaypoint());
            }
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            Vector3 moveDirection = (targetWaypoint.position - transform.position).normalized;

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }

            yield return null;
        }
    }

    IEnumerator Follow()
    {
        while (isPlayerDetected)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }
    IEnumerator Shoot()
    {
        while (isPlayerDetected)
        {
            // 첫 번째 총구에서 총알 발사
            for (int i = 0; i < 20; i++)
            {
                ShootBullet(ShootPosition1);
                yield return new WaitForSeconds(1f / 20f); // 1초에 10발씩 쏘도록 조절
                ShootBullet(ShootPosition2);
                yield return new WaitForSeconds(1f / 20f); // 1초에 10발씩 쏘도록 조절
            }
            // 5초 쉬기
            yield return new WaitForSeconds(5f);

        }
    }


    void ShootBullet(Transform firePoint)
    {
        // 총알 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 총알 방향 설정
        Vector3 shootDirection = firePoint.forward;

        // 총알 속도 설정
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        // 일정 시간 후 총알 삭제
        Destroy(bullet, 2f);
    }
}
