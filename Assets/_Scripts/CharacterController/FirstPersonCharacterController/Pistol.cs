using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pistol : Gun
{

    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        range = 100f;
        fireRate = 4;

        totalAmmo = 45;
        currentMag = 0;
        maxMag = 15;
        reloadTime = 1f;
    }
}
