using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints; // �̵��� ��ǥ���� �迭
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float detectionRange = 10f; // �÷��̾� Ž�� ����
    public float rotationSpeed = 120f; // ȸ�� �ӵ� (����/��)
    public Transform player; // �÷��̾� Transform
    public GameObject bulletPrefab; // �Ѿ� ������
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�
    public float fireRate = 1f; // �Ѿ� �߻� ����
    public Transform ShootPosition1; // �Ѿ� �߻� ��ġ (ĳ���� �Ӹ� ��ġ)
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
            // ù ��° �ѱ����� �Ѿ� �߻�
            for (int i = 0; i < 20; i++)
            {
                ShootBullet(ShootPosition1);
                yield return new WaitForSeconds(1f / 20f); // 1�ʿ� 10�߾� ��� ����
                ShootBullet(ShootPosition2);
                yield return new WaitForSeconds(1f / 20f); // 1�ʿ� 10�߾� ��� ����
            }
            // 5�� ����
            yield return new WaitForSeconds(5f);

        }
    }


    void ShootBullet(Transform firePoint)
    {
        // �Ѿ� ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �Ѿ� ���� ����
        Vector3 shootDirection = firePoint.forward;

        // �Ѿ� �ӵ� ����
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;

        // ���� �ð� �� �Ѿ� ����
        Destroy(bullet, 2f);
    }
}
