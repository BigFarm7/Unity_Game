using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject interactionUI;
    public GameObject inventoryUI;
    private bool isNearVendingMachine = false;
    private bool isInventoryOpen = false;

    void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        if (isNearVendingMachine && Input.GetKeyDown(KeyCode.F))
        {
            ToggleInventory();
        }
    }

    public void ShowInteractionUI(bool show)
    {
        interactionUI.SetActive(show);
    }

    public void SetNearVendingMachine(bool near)
    {
        isNearVendingMachine = near;
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);
    }
}
