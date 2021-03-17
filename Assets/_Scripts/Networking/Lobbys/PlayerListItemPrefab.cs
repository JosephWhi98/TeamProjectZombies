using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace NetworkingSystems
{
    public class PlayerListItemPrefab : MonoBehaviour
    {
        public TextMeshProUGUI nameField;
        public void Setup(Player player)
        {
            nameField.SetText(player.NickName);
        }
    }
}