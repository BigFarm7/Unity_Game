using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AlienMove : MonoBehaviour
{

    public GameObject finalWayPoint; // �߾� ������ GameObject
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float detectionRange = 40f; // �÷��̾� �Ǵ� ��ž Ž�� ����
    public LayerMask detectionLayer; // Ž���� ���̾� (��: �÷��̾�� ��ž ���̾�)
    public float attackRange = 8f; // ���� ����
    private GameObject currentTarget;
    private Animator animator;
    void Start()
    {
        // FinalPoint��� �̸��� GameObject�� ã�Ƽ� �Ҵ��մϴ�.
        finalWayPoint = GameObject.Find("FinalPoint");
        currentTarget = finalWayPoint;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Ž�� ������ ��ü�� ã���ϴ�.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);

        if (hitColliders.Length > 0)
        {
            // Ž���� ��ü �� ù ��° ��ü�� ��ǥ�� �����մϴ�.
            currentTarget = hitColliders[0].gameObject;
        }
        else
        {
            // Ž���� ��ü�� ������ FinalPoint�� �̵��մϴ�.
            currentTarget = finalWayPoint;
        }

        // currentTarget�� �������� ���� ��쿡 ����� üũ
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
            // ���� ���͸� ����մϴ�.
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;

            // ������ ���� �̵��մϴ�.
            transform.position += direction * moveSpeed * Time.deltaTime;

            // ������ ���� ȸ���մϴ�.
            transform.rotation = Quaternion.LookRotation(direction);

           

            // ���� ���� ���� �ִ� ��� ���� �ִϸ��̼� ����
            if (distanceToTarget <= attackRange)
            {
                animator.SetBool("Attack",true);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // ��ǥ�� �޼��� ��� (��: ��ž�� �μ� ���) �ٽ� finalWayPoint�� �̵��մϴ�.
        if (other.gameObject == currentTarget)
        {
            // ���� ��ǥ�� �÷��̾ ��ž�̸� �ش� ��ǥ�� ó���� �� finalWayPoint�� ����
            if (other.gameObject != finalWayPoint)
            {
                // ��ǥ�� ó���ϴ� ���� (��: ��ž �ı�) ���⿡ �߰� ����
                Destroy(other.gameObject); // ��: ��ž �ı�

                // ��ǥ�� �޼��ϸ� finalWayPoint�� �̵�
                currentTarget = finalWayPoint;
            }
        }
    }
    
    
}
