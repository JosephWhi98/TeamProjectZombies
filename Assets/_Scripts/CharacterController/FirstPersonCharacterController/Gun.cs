using UnityEngine;
using System.Collections;
using Photon.Pun;
using System;
using Photon.Pun.Demo.Asteroids;

abstract public class Gun : MonoBehaviour
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
    private bool canFire = true;
    private float nextFireTime; 

    public Animator firstPersonAnimator;

    public LayerMask layerMask;

    public event Action<FireEvent> fired;

    void Awake()
    {
        StartCoroutine(Reload());
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && isReloading == false)
        {
            if(Time.time > nextFireTime)
                Shoot();
        }

        if (Input.GetButtonDown("Reload") && isReloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + (1/fireRate); 

        RaycastHit hit;

        if (currentMag > 0)
        {

            currentMag -= 1;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
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
       
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading!");

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