using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public PlayerMovement player;
    public Gun glock;
    public Gun m4a1;
    public Gun hammer;  // Add the hammer
    private Gun currentGun;
    private float lastFireTime;
    private bool isReloading = false;
    public bool buygun = false;
    private enum Guns { Glock, M4A1, Hammer }  // Include Hammer
    private Guns currentGunType = Guns.Glock;

    void Start()
    {
        glock.gameObject.SetActive(true);
        m4a1.gameObject.SetActive(false);
        hammer.gameObject.SetActive(false);  // Set hammer to inactive initially
        currentGun = glock;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && currentGun.currentAmmo > 0 && player.isRightClick && !isReloading)
        {
            if (Time.time >= lastFireTime + currentGun.fireCooldown)
            {
                Shoot();
                lastFireTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && !Input.GetButton("Fire1") && !IsPlayingAnimation("Shoot"))
        {
            StartCoroutine(Reload());
        }

        // Switch between guns
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentGunType != Guns.Glock)
        {
            StartCoroutine(SwitchGun(Guns.Glock));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentGunType != Guns.M4A1 &&buygun == true)
        {
            StartCoroutine(SwitchGun(Guns.M4A1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentGunType != Guns.Hammer)
        {
            StartCoroutine(SwitchGun(Guns.Hammer));  // Switch to Hammer
        }
    }

    void Shoot()
    {
        currentGun.Shoot();
        player.anim.SetTrigger(currentGunType.ToString() + "Shoot");
    }

    IEnumerator Reload()
    {
        isReloading = true;
        player.anim.SetBool("isReloading", true);

        while (player.anim.GetCurrentAnimatorStateInfo(2).normalizedTime < 1)
        {
            yield return null;
        }

        currentGun.Reload();
        Debug.Log("Reloaded");

        player.anim.SetBool("isReloading", false);
        isReloading = false;
    }

    bool IsPlayingAnimation(string animationName)
    {
        return player.anim.GetCurrentAnimatorStateInfo(2).IsName(animationName);
    }

    IEnumerator SwitchGun(Guns gunType)
    {
        if (isReloading)
        {
            yield break; // Don't switch guns while reloading
        }

        // Perform gun switch animation and logic
        switch (gunType)
        {
            case Guns.Glock:
                m4a1.gameObject.SetActive(false);
                hammer.gameObject.SetActive(false);
                glock.gameObject.SetActive(true);
                currentGun = glock;
                player.anim.SetLayerWeight(3, 0);
                player.anim.SetBool("isRifle", false);
                player.anim.SetBool("isHammer", false);  // Ensure hammer animation is turned off
                break;
            case Guns.M4A1:
                glock.gameObject.SetActive(false);
                hammer.gameObject.SetActive(false);
                m4a1.gameObject.SetActive(true);
                currentGun = m4a1;
                player.anim.SetLayerWeight(3, 1);
                player.anim.SetBool("isRifle", true);
                player.anim.SetBool("isHammer", false);  // Ensure hammer animation is turned off
                break;
            case Guns.Hammer:
                glock.gameObject.SetActive(false);
                m4a1.gameObject.SetActive(false);
                hammer.gameObject.SetActive(true);
                currentGun = hammer;
                player.anim.SetLayerWeight(3, 0);  // Assuming hammer uses the same layer as pistol
                player.anim.SetBool("isRifle", false);
                player.anim.SetBool("isHammer", true);  // Ensure hammer animation is turned on
                break;
        }

       
            yield return null;
        

        currentGunType = gunType;
      
        Debug.Log("Switched to " + currentGunType);
    }
}
