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

    // 호출되면 랙돌로 변환하는 메소드
    public void ChangeRagdoll(Vector3 direction)
    {
        col.enabled = false;
        charObj.SetActive(false);
        ragdollObj.SetActive(true);
        spine.AddForce(-direction * Random.Range(100, 250), ForceMode.Impulse);
        alienmove.enabled = false;

        // 2초 후에 캐릭터를 제거하는 함수 호출
        Invoke("RemoveCharacter", 2f);
    }

    // 캐릭터를 제거하는 메소드
    private void RemoveCharacter()
    {
        Destroy(me);
    }
}
