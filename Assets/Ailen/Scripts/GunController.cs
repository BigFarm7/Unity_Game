using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject bulletPrefab;
    public Transform firePoint1; 
    public Transform firePoint2;
    public float bulletSpeed = 20f; 
    public int maxAmmo = 12; 

    private int currentAmmo; 
    private float lastFireTime; 
    private float fireCooldown = 0.5f;
    private bool isReloading = false;
    public TMP_Text bulletText;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && currentAmmo > 0 && player.isRightClick && !isReloading)
        {
            if (Time.time >= lastFireTime + fireCooldown)
            {
                Shoot();
                player.anim.SetTrigger("isShot");
                lastFireTime = Time.time;
            }
          
        }
        if(Input.GetKeyDown(KeyCode.R) && !isReloading && !Input.GetButton("Fire1")&& !IsPlayingAnimation("Shoot"))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
       
        Vector3 firePointBetween = firePoint1.position - firePoint2.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, Quaternion.LookRotation(firePointBetween)*Quaternion.Euler(90,0,0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePointBetween * bulletSpeed; 

        StartCoroutine(DestroyBullet(bullet,2.0f));

        currentAmmo--;
        bulletText.text = currentAmmo + "/" + maxAmmo;

        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
        }
    }
    IEnumerator DestroyBullet(GameObject bullet,float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
  
    IEnumerator Reload()
    {
        isReloading = true;
        player.anim.SetBool("isReloading", true); 

        while(player.anim.GetCurrentAnimatorStateInfo(2).normalizedTime<1)
        {
            yield return null;
        }

        currentAmmo = maxAmmo;
        Debug.Log("Reloaded");

        player.anim.SetBool("isReloading", false); 
        isReloading = false;
        bulletText.text = currentAmmo + "/" + maxAmmo;
    }

    bool IsPlayingAnimation(string animationName)
    {
        return player.anim.GetCurrentAnimatorStateInfo(2).IsName(animationName);
    }
}
