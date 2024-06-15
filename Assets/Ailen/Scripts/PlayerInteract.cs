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
            interactTextUI.SetActive(isPurchaseUIActive); // purchaseUI가 활성화되면 interactTextUI 비활성화
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vending"))
        {
            isNearVendingMachine = true;
            if (!purchaseUI.activeSelf) // purchaseUI가 비활성화 상태일 때만 interactTextUI 활성화
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
            purchaseUI.SetActive(false); // 자판기에서 멀어지면 구매 UI 비활성화
        }
    }
}
