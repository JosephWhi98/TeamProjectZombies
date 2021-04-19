using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    public Animator firstPersonAnimator;
    public NetworkedAnimatorView thirdPersonAnimator;
    public AudioSource source;
    public AudioClip gunShotClip;

    public void OnEnable()
    {
        Gun g = GetComponentInChildren<Gun>();
        if (g == null)
        { Debug.Log("No gun found, should happen on nonlocal players"); return; }
        g.fired += DrawWeaponEffects;

    }
    public void OnDisable()
    {
        Gun g = GetComponentInChildren<Gun>();
        if (g == null)
        { Debug.Log("No gun found, should happen on nonlocal players"); return; }
        g.fired -= DrawWeaponEffects;
    }

    public void DrawWeaponEffects(FireEvent fireEvent)
    {
        if (firstPersonAnimator)
            firstPersonAnimator.SetTrigger("Fire");

        if (thirdPersonAnimator)
            thirdPersonAnimator.TriggerAnimaton("Fire", onlyOthers: true);
    }

}
