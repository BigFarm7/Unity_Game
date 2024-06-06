using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SpiderLegController : MonoBehaviour
{

    public Transform[] legJoints; // 각 다리의 관절
    public float moveSpeed = 1f; // 다리 이동 속도
    public float jointRotationSpeed = 30f; // 관절 회전 속도
    public float jointRotationLimit = 45f; // 관절 회전 제한

    private int currentIndex = 0; // 현재 처리 중인 관절 인덱스
    private bool movingForward = true; // 다음 관절로 이동하는 방향

    void Update()
    {
        // 현재 다리의 관절을 회전시킵니다.
        RotateJoint(legJoints[currentIndex], jointRotationSpeed * Time.deltaTime);

        // 다음 관절로 이동합니다.
        MoveToNextJoint();
    }

    // 다음 관절로 이동하는 함수
    void MoveToNextJoint()
    {
        // 다음 관절으로 이동해야 하는지 확인합니다.
        if (Mathf.Abs(legJoints[currentIndex].localRotation.eulerAngles.z) >= jointRotationLimit)
        {
            movingForward = !movingForward;
            currentIndex += movingForward ? 1 : -1;
            currentIndex = Mathf.Clamp(currentIndex, 0, legJoints.Length - 1);
        }

        // 현재 다리의 관절을 회전시킵니다.
        RotateJoint(legJoints[currentIndex], jointRotationSpeed * Time.deltaTime);
    }

    // 관절을 회전시키는 함수
    void RotateJoint(Transform joint, float rotationSpeed)
    {
        // 회전 방향과 속도에 따라 관절을 회전시킵니다.
        float rotationAmount = rotationSpeed * (movingForward ? 1f : -1f);
        joint.Rotate(Vector3.forward, rotationAmount);
    }
}
