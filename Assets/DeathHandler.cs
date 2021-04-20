using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public Camera playerCam;
    private Transform audioListener;

    private DeathHandler spectating;
    [SerializeField] private PhotonView photonView;
    public bool isDead = false;

    [SerializeField]
    private int spectatingPlayer = 0;
    [SerializeField] List<int> livePlayers;

    private void Start()
    {
        spectating = this;

        if (photonView.IsMine)
        {
            audioListener = FindObjectOfType<AudioListener>().transform;
            MoveAudioListenerToSpectating();
        }
    }

    public void AddLivePlayers(int id)
    {
        if (!livePlayers.Contains(id) && id != PhotonNetwork.LocalPlayer.ActorNumber)
            livePlayers.Add(id);
    }
    public void RemovePlayer(int id)
    {
        livePlayers.Remove(id);

        if (spectating != this)
        {
            SetNewSpectator(0);
        }
    }
    public void Die()
    {
        isDead = true;
        GameManager.instance.OnPlayerDeath(photonView.OwnerActorNr);
        if (photonView.IsMine)
            SetNewSpectator();

        gameObject.SetActive(false);
    }

    public void SetNewSpectator(int delta = 1)
    {
        spectatingPlayer += delta;

        if (spectatingPlayer >= livePlayers.Count)
            spectatingPlayer = 0;
        else if (spectatingPlayer < 0)
            spectatingPlayer = livePlayers.Count - 1;

        if (livePlayers.Count == 0)
        { EndGame(); return; }

        spectating.playerCam.enabled = false;

        spectating = GameManager.instance.players[livePlayers[spectatingPlayer]].transform.GetComponent<DeathHandler>();

        spectating.playerCam.enabled = true;

        MoveAudioListenerToSpectating();
    }
    public void MoveAudioListenerToSpectating()
    {
        audioListener.parent = spectating.playerCam.transform;
        audioListener.localPosition = Vector3.zero;
        audioListener.localRotation = Quaternion.identity;
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
    }
}
