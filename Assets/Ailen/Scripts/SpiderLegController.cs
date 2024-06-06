using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SpiderLegController : MonoBehaviour
{

    public Transform[] legJoints; // �� �ٸ��� ����
    public float moveSpeed = 1f; // �ٸ� �̵� �ӵ�
    public float jointRotationSpeed = 30f; // ���� ȸ�� �ӵ�
    public float jointRotationLimit = 45f; // ���� ȸ�� ����

    private int currentIndex = 0; // ���� ó�� ���� ���� �ε���
    private bool movingForward = true; // ���� ������ �̵��ϴ� ����

    void Update()
    {
        // ���� �ٸ��� ������ ȸ����ŵ�ϴ�.
        RotateJoint(legJoints[currentIndex], jointRotationSpeed * Time.deltaTime);

        // ���� ������ �̵��մϴ�.
        MoveToNextJoint();
    }

    // ���� ������ �̵��ϴ� �Լ�
    void MoveToNextJoint()
    {
        // ���� �������� �̵��ؾ� �ϴ��� Ȯ���մϴ�.
        if (Mathf.Abs(legJoints[currentIndex].localRotation.eulerAngles.z) >= jointRotationLimit)
        {
            movingForward = !movingForward;
            currentIndex += movingForward ? 1 : -1;
            currentIndex = Mathf.Clamp(currentIndex, 0, legJoints.Length - 1);
        }

        // ���� �ٸ��� ������ ȸ����ŵ�ϴ�.
        RotateJoint(legJoints[currentIndex], jointRotationSpeed * Time.deltaTime);
    }

    // ������ ȸ����Ű�� �Լ�
    void RotateJoint(Transform joint, float rotationSpeed)
    {
        // ȸ�� ����� �ӵ��� ���� ������ ȸ����ŵ�ϴ�.
        float rotationAmount = rotationSpeed * (movingForward ? 1f : -1f);
        joint.Rotate(Vector3.forward, rotationAmount);
    }
}
