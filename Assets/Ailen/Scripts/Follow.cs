using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Follow : MonoBehaviour
{
    public Transform target; // 타겟 위치
    public Vector3 offset = new Vector3(0, 2, 0); // 초기 오프셋 값
    public float smoothTime = 0.3f; // 부드럽게 변환되는 시간

    private bool On = true;
    private float targetOffsetY; // 목표 y 오프셋 값
    private float currentVelocity = 0.0f; // 현재 속도

    void Start()
    {
        targetOffsetY = offset.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (On)
            {
                targetOffsetY = 6;
                On = false;
            }
            else
            {
                targetOffsetY = 2;
                On = true;
            }
        }

        // offset.y 값을 부드럽게 변환
        offset.y = Mathf.SmoothDamp(offset.y, targetOffsetY, ref currentVelocity, smoothTime);

        // 오프셋을 사용하여 현재 위치 설정
        transform.position = target.position + offset;
    }
}

