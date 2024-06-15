using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int gold = 50;
    public Text goldText; // TextMeshProUGUI 컴포넌트를 참조합니다.
    public Image gunimage;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = gold.ToString();
    }

    public GameObject Monster;
    public GameObject FourLeg;
    public Transform[] wayPoints;
    public float spawnInterval = 5f;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI statusText;

    private bool isSpawning = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(ManageSpawning());
        UpdateGoldText();
    }

    void Update()
    {

    }

    IEnumerator ManageSpawning()
    {
        while (true)
        {
            // 30초 카운트다운
            statusText.text = "정비 시간";
            for (float countdown = 30f; countdown > 0; countdown -= Time.deltaTime)
            {
                countdownText.text = $"{Mathf.Ceil(countdown)}";
                yield return null;
            }

            // 1분간 스폰 시작
            statusText.text = "남은 시간";
            isSpawning = true;
            InvokeRepeating("SpawnAlien", 0f, spawnInterval);
            for (float countdown = 60f; countdown > 0; countdown -= Time.deltaTime)
            {
                countdownText.text = $"{Mathf.Ceil(countdown)}";
                yield return null;
            }

            isSpawning = false;
            CancelInvoke("SpawnAlien");

            // 30초 정비시간
            statusText.text = "정비 시간";
            for (float countdown = 30f; countdown > 0; countdown -= Time.deltaTime)
            {
                countdownText.text = $"{Mathf.Ceil(countdown)}";
                yield return null;
            }

            statusText.text = "남은 시간";
            isSpawning = true;
            InvokeRepeating("SpawnAlien", 0f, 3);
            for (float countdown = 60f; countdown > 0; countdown -= Time.deltaTime)
            {
                countdownText.text = $"{Mathf.Ceil(countdown)}";
                yield return null;
            }

            isSpawning = false;
            CancelInvoke("SpawnAlien");

            // 30초 정비시간
            statusText.text = "정비 시간";
            for (float countdown = 30f; countdown > 0; countdown -= Time.deltaTime)
            {
                countdownText.text = $"{Mathf.Ceil(countdown)}";
                yield return null;
            }


            statusText.text = "남은 시간";
            isSpawning = true;
            InvokeRepeating("SpawnFinal", 0f, 3);
            for (float countdown = 60f; countdown > 0; countdown -= Time.deltaTime)
            {
                countdownText.text = $"{Mathf.Ceil(countdown)}";
                yield return null;
            }

            
        }
    }

    public void SpawnAlien()
    {
        if (isSpawning)
        {
            GameObject LV1Monster = Instantiate(Monster, wayPoints[Random.Range(0, wayPoints.Length)].position, Quaternion.identity);
        }
    }

    public void SpawnFinal()
    {
        if (isSpawning)
        {
            GameObject LV1Monster = Instantiate(Monster, wayPoints[Random.Range(0, wayPoints.Length)].position, Quaternion.identity);
            GameObject Four = Instantiate(FourLeg, wayPoints[Random.Range(0, wayPoints.Length)].position, Quaternion.identity);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateGoldText()
    {
        goldText.text = gold.ToString();
    }

    public void BuyM4()
    {
        if (gold >= 20)
        {
            // Deduct the gold
            gold -= 20;
            UpdateGoldText(); // Update the gold text

            // Find the "Glock" object
            GameObject glockObject = GameObject.Find("M4");
            if (glockObject != null)
            {
                // Find the "Gun" script attached to the "Glock" object
                Gun gunScript = glockObject.GetComponent<Gun>();
                if (gunScript != null)
                {
                    // Add 12 rounds to the "Glock" gun
                    gunScript.maxAmmo += 30;

                    gunScript.UpdateAmmoText(); // Update the ammo text on the gun
                    Debug.Log("Purchased 12 rounds of Glock ammo.");
                }
                else
                {
                    Debug.LogWarning("Gun script not found on the Glock object.");
                }
            }
            else
            {
                Debug.LogWarning("Glock object not found.");
            }
        }
        else
        {
            Debug.Log("Not enough gold to buy ammo.");
        }
    }

    public void BuyGl()
    {
        if (gold >= 3)
        {
            // Deduct the gold
            gold -= 3;
            UpdateGoldText(); // Update the gold text

            // Find the "Glock" object
            GameObject glockObject = GameObject.Find("GL");
            if (glockObject != null)
            {
                // Find the "Gun" script attached to the "Glock" object
                Gun gunScript = glockObject.GetComponent<Gun>();
                if (gunScript != null)
                {
                    // Add 12 rounds to the "Glock" gun
                    gunScript.maxAmmo += 12;

                    gunScript.UpdateAmmoText(); // Update the ammo text on the gun
                    Debug.Log("Purchased 12 rounds of Glock ammo.");
                }
                else
                {
                    Debug.LogWarning("Gun script not found on the Glock object.");
                }
            }
            else
            {
                Debug.LogWarning("Glock object not found.");
            }
        }
        else
        {
            Debug.Log("Not enough gold to buy ammo.");
        }
    }

    public void BuyM4A1()
    {
        if (gold >= 1)
        {
            // Deduct the gold
            gold -= 1;
            UpdateGoldText(); // Update the gold text

            // Find the "Glock" object
            GameObject glockObject = GameObject.Find("M4A1");
            gunimage.gameObject.SetActive(true);

            GunController gun = GameObject.Find("Char").GetComponent<GunController>();
            gun.buygun = true;
        }
    }
    
}