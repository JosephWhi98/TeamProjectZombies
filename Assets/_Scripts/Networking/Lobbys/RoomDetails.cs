using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NetworkingSystems
{
    public class RoomDetails : MonoBehaviour
    {
        byte lastmaxPlayers;
        bool lastIsOpen = false;
        public TMP_InputField maxPlayersInput;
        public Toggle isOpenInput;
        public TMP_Dropdown mapInput;
        public Button startButton;
        public
        enum Maps
        {
            Casa = 0,
            CasaCurtis = 1,
            CasaDale = 2,
            CasaLiam = 3,
        }
        string sceneName;

        private void Start()
        {
            this.sceneName = ((Maps)0).ToString();

        }
        public void OnEnable()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                maxPlayersInput.interactable = false;
                isOpenInput.interactable = false;
                mapInput.interactable = false;
                startButton.interactable = false;
            }
        }
        public void SetPublic(bool isPublic)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            PhotonNetwork.CurrentRoom.IsVisible = isPublic;
        }
        public void SetScene(int sceneID)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            this.sceneName = ((Maps)sceneID).ToString();
        }
        public void SetMaxPlayers(string maxPlayers)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            if (int.TryParse(maxPlayers, out int v))
                PhotonNetwork.CurrentRoom.MaxPlayers = (byte)v;
        }
        public void StartGame()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            SceneManager.LoadScene(sceneName);
        }
        private void Update()
        {
            if (PhotonNetwork.IsMasterClient)
                return;
            if (PhotonNetwork.CurrentRoom.MaxPlayers != lastmaxPlayers)
            {
                maxPlayersInput.SetTextWithoutNotify(PhotonNetwork.CurrentRoom.MaxPlayers.ToString());
                lastmaxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
            }
            if (PhotonNetwork.CurrentRoom.IsVisible != lastIsOpen)
            {
                isOpenInput.isOn = PhotonNetwork.CurrentRoom.IsVisible;
                lastIsOpen = PhotonNetwork.CurrentRoom.IsVisible;
            }
        }
    }
}