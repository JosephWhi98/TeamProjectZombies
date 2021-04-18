using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    public Light flash;
    public Animator firstPersonAnimator;
    public NetworkedAnimatorView thirdPersonAnimator;
    public AudioSource source;
    public AudioClip gunShotClip;

    public void DrawWeaponEffects()
    {
        if (firstPersonAnimator)
            firstPersonAnimator.SetTrigger("Fire");

        if (thirdPersonAnimator)
            thirdPersonAnimator.TriggerAnimaton("Fire");

    }

    IEnumerator DisableWeaponEffects()
    {
        yield return new WaitForSeconds(0.1f);
        flash.enabled = false;
    }

}
