using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkWeaponFire : MonoBehaviour
{
    public PhotonView photonView;
    public WeaponEffects effects;
    public void Enable()
    {
        effects = GetComponentInChildren<WeaponEffects>();
        Gun g = GetComponentInChildren<Gun>();
        if (g == null)
        { Debug.Log("No gun found, should happen on nonlocal players"); return; }
        g.fired += TriggerWeaponFire;

    }
    public void OnDisable()
    {
        Gun g = GetComponentInChildren<Gun>();
        if (g == null)
        { Debug.Log("No gun found, should happen on nonlocal players"); return; }
        g.fired -= TriggerWeaponFire;
    }
    public void TriggerWeaponFire(FireEvent e)
    {
        FireEffects();
        //photonView.RPC("FireEffects", RpcTarget.Others);
    }

    [PunRPC]
    public void FireEffects()
    {
        //effects.DrawWeaponEffects();
    }
}
