using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    public enum WeaponType
    {
        Glock,
        M4A1
    }

    public WeaponType currentWeapon = WeaponType.Glock;

    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public float bulletSpeed = 20f;
    public float fireCooldown = 0.5f;

    [HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public int maxAmmo;

    // Add a reference to the TextMeshPro Text component
    public TMP_Text ammoText;

    void Start()
    {
        SetMaxAmmo();
        currentAmmo = maxAmmo;
        UpdateAmmoText(); // Initialize the ammo text
    }

    void SetMaxAmmo()
    {
        switch (currentWeapon)
        {
            case WeaponType.Glock:
                maxAmmo = 12;
                break;
            case WeaponType.M4A1:
                maxAmmo = 30;
                break;
            default:
                maxAmmo = 12;
                break;
        }
    }

    public void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Out of Ammo!");
            return;
        }

        Vector3 firePointBetween = firePoint1.position - firePoint2.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint1.position, Quaternion.LookRotation(firePointBetween) * Quaternion.Euler(90, 0, 0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePointBetween.normalized * bulletSpeed;

        currentAmmo--;
        UpdateAmmoText(); // Update the ammo text after shooting
        StartCoroutine(DestroyBullet(bullet, 2.0f));
    }

    public void Reload()
    {
        if (maxAmmo <= 0)
        {
            Debug.Log("No ammo left to reload!");
            return;
        }

        int ammoNeeded = GetMagazineSize() - currentAmmo; // Calculate how much ammo is needed to fill the magazine
        if (maxAmmo >= ammoNeeded)
        {
            currentAmmo += ammoNeeded;
            maxAmmo -= ammoNeeded;
        }
        else
        {
            currentAmmo += maxAmmo;
            maxAmmo = 0;
        }

        UpdateAmmoText(); // Update the ammo text after reloading
    }

    int GetMagazineSize()
    {
        switch (currentWeapon)
        {
            case WeaponType.Glock:
                return 12;
            case WeaponType.M4A1:
                return 30;
            default:
                return 12;
        }
    }

    IEnumerator DestroyBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    // Method to update the ammo text
    public void UpdateAmmoText()
    {
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }

}
