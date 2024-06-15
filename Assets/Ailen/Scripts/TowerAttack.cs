using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public Transform head; // Ÿ���� ���� ȸ����ų �κ�
    public float range = 15f; // Ž�� ����
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    public float fireRate = 1f; // �߻� ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public Transform firePoint1;
    public Transform firePoint2;
    private Transform target;
    public float bulletSpeed = 20f;
    private float fireCountdown = 0f;

    void Update()
    {
        FindTarget();

        if (target == null)
            return;

        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void LockOnTarget()
    {
        Vector3 direction = target.position - head.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(head.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        head.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        Vector3 firePointBetween = firePoint1.position - firePoint2.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, Quaternion.LookRotation(firePointBetween) * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePointBetween.normalized * bulletSpeed;
        StartCoroutine(DestroyBullet(bullet, 2.0f));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator DestroyBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
