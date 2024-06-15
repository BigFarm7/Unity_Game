using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform finalTarget; // ���� ��ǥ
    public float finalTargetRange = 20f; // ���� ��ǥ�� ���� ���ư��� ���� Ž�� ����
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float rotationSpeed = 5.0f; // ���� ������ �ӵ�
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform bulletSpawnPoint1; // �Ѿ��� �߻�� ��ġ 1
    public Transform bulletSpawnPoint2; // �Ѿ��� �߻�� ��ġ 2
    public float bulletSpeed = 20.0f; // �Ѿ� �ӵ�
    public float shootingInterval = 1.0f; // �߻� ����
    public float attackDistanceThreshold = 15f; // ������ �Ÿ� �Ӱ谪
    public float detectionDistanceThreshold = 10f; // Ž���� ������Ʈ���� �Ÿ� �Ӱ谪
    public LayerMask detectionLayer;
    private float lastShotTime; // ���������� �Ѿ��� �߻��� �ð�
    private bool isAttackingObject = false; // ������Ʈ�� ���� ������ ����
    private Transform currentTarget; // ���� ���� ���

    void Start()
    {
        finalTarget = GameObject.Find("FinalPoint").transform;
    }
    void Update()
    {
        // ���� ���¿� ���� ó��
        if (isAttackingObject)
        {
            HandleAttackObjectState();
        }
        else
        {

            // �ֺ� ������Ʈ Ž�� �� �̵�
            HandleDetectionAndMove();
        }
    }

    void HandleDetectionAndMove()
    {
        // �ֺ��� �ִ� ������Ʈ Ž��
        Collider[] colliders = Physics.OverlapSphere(transform.position, finalTargetRange,detectionLayer);

        Transform nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
          

            if (collider.gameObject != finalTarget.gameObject)
            {
                // Ư�� �±׸� ���� ������Ʈ Ž��
                if (collider.CompareTag("Player") || collider.CompareTag("Tower") || collider.CompareTag("Lotte"))
                {
                    // ������Ʈ���� �Ÿ� ���
                    float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);

                    // ���� ����� ������Ʈ�� ã��
                    if (distanceToTarget < nearestDistance)
                    {
                        nearestDistance = distanceToTarget;
                        nearestTarget = collider.transform;
                    }
                }
            }
        }

      

        // ���� ����� ������Ʈ�� ã���� ��� ���� ���·� ��ȯ
        if (nearestTarget != null)
        {
            StopMovingToFinalTarget();
            currentTarget = nearestTarget;
            isAttackingObject = true;
        }
        else
        {
            // ���� ��ǥ�� �̵�
            MoveToFinalTarget();
        }
    }

    void MoveToFinalTarget()
    {
        if (finalTarget == null) return;
        // ���� ��ǥ ���� ���
        Vector3 targetDirection = (finalTarget.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));

        // ���� ��ǥ �������� �����ϴ�.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // ���� ��ǥ �������� �̵��մϴ�.
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void StopMovingToFinalTarget()
    {
        // ���� ��ǥ�� �̵��� ����ϴ�.
        // �ʿ信 ���� �߰����� ���� ó���� ���⿡ �߰�a�� �� �ֽ��ϴ�.
    }

    void HandleAttackObjectState()
    {
        if (currentTarget == null || !isAttackingObject)
        {
            // ���� ����� ���ų� ���� ���°� �ƴϸ� ���� ���¸� �����ϰ� ���� ��ǥ�� �̵��մϴ�.
            isAttackingObject = false;
            currentTarget = null;
            return;
        }

        // Ž���� ������Ʈ���� �Ÿ��� Ȯ���Ͽ� ���� �Ÿ� �̻� �־����� ���� ��ǥ�� �̵��մϴ�.
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
        if (distanceToTarget > detectionDistanceThreshold)
        {
            isAttackingObject = false; // ���� ���� ����
            currentTarget = null;
            MoveToFinalTarget(); // ���� ��ǥ�� �̵�
            return;
        }

        // ���� ����� ���� ���� �����ϴ�.
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // �߻� ������ �����ٸ� ���� �߻��մϴ�.
        if (Time.time - lastShotTime > shootingInterval)
        {
            // �� ���� �Ѿ� �߻� �������� �߻��մϴ�.
            ShootAtTarget(bulletSpawnPoint1);
            ShootAtTarget(bulletSpawnPoint2);

            lastShotTime = Time.time; // �Ѿ� �߻� �ð� ���
        }
    }

    void ShootAtTarget(Transform spawnPoint)
    {
        // �Ѿ� �߻� ��ġ ���� (�ѱ� ��ġ���� ��������)
        Vector3 shootDirection = spawnPoint.forward; // �Ѿ��� ������ �� ������ �ѱ��� forward ����
        Quaternion shootRotation = Quaternion.LookRotation(shootDirection);

        // �Ѿ� ���� �� �߻�
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, shootRotation);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

        if (bulletRB != null)
        {
            bulletRB.velocity = shootDirection * bulletSpeed;
        }
    }
}