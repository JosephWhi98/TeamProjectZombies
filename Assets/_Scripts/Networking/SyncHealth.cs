using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(HealthComponent))]
public class SyncHealth : MonoBehaviour
{
    PhotonView photonView;
    HealthComponent healthComponent;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        healthComponent = GetComponent<HealthComponent>();
    }
    public void SyncHealthEvent(int delta)
    {
        photonView.RPC("ChangeHealth", RpcTarget.Others, delta);
    }

    [PunRPC]
    private void ChangeHealth(int delta)
    {
        healthComponent.ChangeHealth(delta, false);
    }
}
