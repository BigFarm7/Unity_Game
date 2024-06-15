using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class RagdollChanger : MonoBehaviour
{
    public GameObject me;
    public GameObject charObj;
    public GameObject ragdollObj;
    public BoxCollider col;
    public Rigidbody spine;
    public AlienMove alienmove;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    // ȣ��Ǹ� ������ ��ȯ�ϴ� �޼ҵ�
    public void ChangeRagdoll(Vector3 direction)
    {
        col.enabled = false;
        charObj.SetActive(false);
        ragdollObj.SetActive(true);
        spine.AddForce(-direction * Random.Range(100, 250), ForceMode.Impulse);
        alienmove.enabled = false;

        // 2�� �Ŀ� ĳ���͸� �����ϴ� �Լ� ȣ��
        Invoke("RemoveCharacter", 2f);
    }

    // ĳ���͸� �����ϴ� �޼ҵ�
    private void RemoveCharacter()
    {
        Destroy(me);
    }
}
