using Photon.Pun;
using System;
using TMPro;
using UnityEngine;

public class UsernameController : MonoBehaviour
{
    public TMP_InputField input;
    void Start()
    {
        if(PlayerPrefs.HasKey("Username"))
            input.SetTextWithoutNotify(PlayerPrefs.GetString("Username"));
        else
            input.SetTextWithoutNotify("RandomUser" + UnityEngine.Random.Range(0, 10000));
        SetPhotonNickName(input.text);
    }

    public void SetUsername(string name)
    {
        PlayerPrefs.SetString("Username", name);
        SetPhotonNickName(name);
    }

    private void SetPhotonNickName(string name)
    {
        PhotonNetwork.LocalPlayer.NickName = name;
    }
}
