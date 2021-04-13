using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    public Light flash;
    public void DrawWeaponEffects()
    {
        flash.enabled = true;
        StartCoroutine(DisableWeaponEffects());
    }
    IEnumerator DisableWeaponEffects()
    {
        yield return new WaitForSeconds(0.1f);
        flash.enabled = false;
    }

}
