using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public DeathHandler localDeathHandler;
    public static GameObject playerInstance;
    public Transform[] spawnPoints;

    public GameObject gameOverScreen;

    //ActorNumber, Details.
    public Dictionary<int, PlayerDetails> players = new Dictionary<int, PlayerDetails>();

    public float pos;

    private bool gameOver;
    private float gameEndTime;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            playerInstance = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length].position, Quaternion.identity);
            localDeathHandler = playerInstance.GetComponent<DeathHandler>();
        }
    }

    private void Update()
    {
        if (localDeathHandler != null && localDeathHandler.isDead) //needs new death handler for solo queue to just end the game
        {
            if (Input.GetMouseButtonDown(0))
                localDeathHandler.SetNewSpectator(1);
            if (Input.GetMouseButtonDown(1))
                localDeathHandler.SetNewSpectator(-1);
        }


        if (CheckGameOver() && !gameOver)
        {
            gameEndTime = Time.time;
            gameOver = true;
            GameOver();
        }


        if (gameOver && Time.time > (gameEndTime + 5f))
        {
            ExitToMenu();
        }

    }

    public bool CheckGameOver()
    {
        foreach (KeyValuePair<int, PlayerDetails> pD in players)
        {
            if (pD.Value.healthComponent.Health > 0)
                return false;
        }

        return true;
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Debug.LogError("GAME OVER!");
        gameOverScreen.SetActive(true);
    }

    public void ExitToMenu()
    {
        LeaveRoom();
        SceneManager.LoadScene(0);
    }

    public void AddPlayer(Player owner, Transform t)
    {
        if (!players.ContainsKey(owner.ActorNumber))
        {
            HealthComponent health = t.GetComponent<HealthComponent>();

            players.Add(owner.ActorNumber, new PlayerDetails() { username = owner.NickName, transform = t, healthComponent = health } );
            localDeathHandler.AddLivePlayers(owner.ActorNumber);
        }
        Debug.Log("New Player Added: " + owner.NickName, t.gameObject);

    }
    public void OnPlayerDeath(int id)
    {
        localDeathHandler?.RemovePlayer(id);
    }

    public Transform GetClosestPlayerTransform(Vector3 worldPosition)
    {
        float closestDistance = float.MaxValue;
        Transform closestTransform = null;


        foreach (KeyValuePair<int, PlayerDetails> kvp in players)
        {
            float dist = Vector3.Distance(worldPosition, kvp.Value.transform.position);

            if (dist < closestDistance && kvp.Value.transform.gameObject.activeInHierarchy)
            {
                closestDistance = dist;
                closestTransform = kvp.Value.transform;
            }
        }

        return closestTransform;
    }
    public override void OnPlayerLeftRoom(Player p)
    {
        base.OnPlayerLeftRoom(p);
        Debug.Log("Player " + p.NickName + " has left the lobby");
        if (players.TryGetValue(p.ActorNumber, out PlayerDetails v))
        {

            Debug.Log("Player " + v.username + " removed from list");
            Destroy(v.transform.gameObject);
            players.Remove(p.ActorNumber);
        }
    }

    [ContextMenu("Leave")]
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
public class PlayerDetails
{
    public string username;
    public Transform transform;
    public HealthComponent healthComponent;
}
