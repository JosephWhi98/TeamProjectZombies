using UnityEngine;
using System.Collections;
using Photon.Pun;
using System;
using Photon.Pun.Demo.Asteroids;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 1;

    public int totalAmmo = 10;
    public int currentMag = 0;
    public int maxMag = 10;
    public float reloadTime = 1f;

    public Camera fpsCam;

    private bool isReloading = false;

    public Animator firstPersonAnimator;

    public event Action<FireEvent> fired;

    void Awake()
    {
        StartCoroutine(Reload());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload") && isReloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (currentMag > 0)
        {
            if (firstPersonAnimator)
                firstPersonAnimator.SetTrigger("Fire");


            currentMag -= 1;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Zombie zombie = hit.transform.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }
                fired?.Invoke(new FireEvent() { hitLocation = hit.point });
            }
            else
                fired?.Invoke(new FireEvent() { hitLocation = null });
            Debug.Log("Current: " + currentMag + "  " + "Total Ammo: " + totalAmmo);

        }
        else
        {
            Reload();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        if (totalAmmo <= 0)
        {
            yield break;
        }

        int ammoToLoad = maxMag - currentMag;
        if (ammoToLoad > totalAmmo)
        {
            currentMag += totalAmmo;
            totalAmmo = 0;
        }

        else
        {
            currentMag = maxMag;
            totalAmmo -= ammoToLoad;
        }

        isReloading = false;
    }
}

public class FireEvent
{
    public Vector3? hitLocation;
}