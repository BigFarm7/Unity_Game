using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Follow : MonoBehaviour
{
    public Transform target; // Ÿ�� ��ġ
    public Vector3 offset = new Vector3(0, 2, 0); // �ʱ� ������ ��
    public float smoothTime = 0.3f; // �ε巴�� ��ȯ�Ǵ� �ð�

    private bool On = true;
    private float targetOffsetY; // ��ǥ y ������ ��
    private float currentVelocity = 0.0f; // ���� �ӵ�

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

        // offset.y ���� �ε巴�� ��ȯ
        offset.y = Mathf.SmoothDamp(offset.y, targetOffsetY, ref currentVelocity, smoothTime);

        // �������� ����Ͽ� ���� ��ġ ����
        transform.position = target.position + offset;
    }
}

