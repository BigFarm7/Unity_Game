using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerBuild : MonoBehaviour
{
    public GameObject Player;
    public GameObject towerPrefab;
    public GameObject blueprintPrefab;
    public float buildDistance = 5f;
    private GameObject currentBlueprint;
    private bool canBuild;
    private bool isBuilding;
    public TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI component
    public int towerCost = 50;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isBuilding)
        {
            if (GameManager.instance.gold >= towerCost)
            {
                StartBuilding();
                
            }
           
        }

        if (isBuilding)
        {
            MoveBlueprint();
            ShowMessage("ÇÊ¿ä °ñµå : 50");
            if (Input.GetMouseButtonDown(1))
            {
                CancelBuilding();
            }
            else if (Input.GetMouseButton(0) && canBuild)
            {
                PlaceTower();
            }
        }
    }

    void StartBuilding()
    {
        isBuilding = true;
        currentBlueprint = Instantiate(blueprintPrefab);
        ShowMessage(""); // Clear any previous messages
    }

    void MoveBlueprint()
    {
        Vector3 forwardPosition = Player.transform.position + Player.transform.forward * buildDistance;
        currentBlueprint.transform.position = forwardPosition;
        CheckBuildable(forwardPosition);
    }

    void CheckBuildable(Vector3 position)
    {
        // Implement your logic to check if the tower can be built at the position
        Collider[] hitColliders = Physics.OverlapSphere(position, 1f);
       
        canBuild = hitColliders.Length == 3;

        // Change the color of the blueprint to indicate if it's buildable
        Renderer[] renderers = currentBlueprint.GetComponentsInChildren<Renderer>();
        Color color = canBuild ? Color.green : Color.red;

        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                material.color = color;
            }
        }
    }

    void PlaceTower()
    {
        if (GameManager.instance.gold >= towerCost)
        {
            GameManager.instance.gold -= towerCost;
            GameManager.instance.UpdateGoldText(); // Ensure this method is available in GameManager to update the gold display

            Instantiate(towerPrefab, currentBlueprint.transform.position, Quaternion.identity);
            Destroy(currentBlueprint);
            isBuilding = false;
        }
        else
        {
            ShowMessage("Not enough gold to build a tower.");
        }
    }

    void CancelBuilding()
    {
        Destroy(currentBlueprint);
        isBuilding = false;
    }

    void ShowMessage(string message)
    {
        messageText.text = message;
    }
}