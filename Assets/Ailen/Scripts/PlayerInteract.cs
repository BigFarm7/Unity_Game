using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private bool isNearVendingMachine = false;
    public GameObject interactTextUI;
    public GameObject purchaseUI;

    void Update()
    {
        CheckForInteract();
    }

    void CheckForInteract()
    {
        if (isNearVendingMachine && Input.GetKeyDown(KeyCode.E))
        {
            bool isPurchaseUIActive = purchaseUI.activeSelf;
            purchaseUI.SetActive(!isPurchaseUIActive);
            interactTextUI.SetActive(isPurchaseUIActive); // purchaseUI�� Ȱ��ȭ�Ǹ� interactTextUI ��Ȱ��ȭ
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vending"))
        {
            isNearVendingMachine = true;
            if (!purchaseUI.activeSelf) // purchaseUI�� ��Ȱ��ȭ ������ ���� interactTextUI Ȱ��ȭ
            {
                interactTextUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vending"))
        {
            isNearVendingMachine = false;
            interactTextUI.SetActive(false);
            purchaseUI.SetActive(false); // ���Ǳ⿡�� �־����� ���� UI ��Ȱ��ȭ
        }
    }
}
