using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //player controller prefab
    public GameObject player1ControllerPrefab;
    public GameObject player2ControllerPrefab;
    //tank pawn prefab
    public GameObject tankPawnPrefab;
    public GameObject tankPawnPrefab2;
    //transform for player spawn
    public Transform playerSpawnTransform;

    //Creating the lists for the controllers, player controllers, and pawns
    public List<Controller> controllers = new List<Controller>();
    public List<PlayerController> playerControllers = new List<PlayerController>();
    public List<AIController> aIControllers = new List<AIController>();
    public List<Pawn> pawns = new List<Pawn>();
    public List<Transform> playerSpawnPoints = new List<Transform>();

    public bool multiPlayer;
    public int player1Lives;
    public int player2Lives;
    public float player1RespawnTime;
    private bool player1RespawnTimeActive;
    private float timeBeforePlayer1Respawn;
    public float player2RespawnTime;
    private bool player2RespawnTimeActive;
    private float timeBeforePlayer2Respawn;
    public int Player1Score;
    public int Player2Score;

    private float TimeBeforePlayerSpawn;
    private bool firstSpawn;

    private GameObject player1Tank;
    private GameObject player2Tank;

    private void Awake()
    {
        //if the instance is null(doesnt exist yet)
        if (instance == null)
        {
            //This is the instance
            instance = this;
            //Dont destroy if we load a new scene
            DontDestroyOnLoad(gameObject);
        }
        //else if there is an instance destroy the gameobject
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeBeforePlayerSpawn = Time.time + 0.5f;
        firstSpawn = false;
        Player1Score = 0;
        Player2Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstSpawn)
        {
            if (Time.time > TimeBeforePlayerSpawn)
            {
                spawnPlayer();
                firstSpawn = true;
            }
        }

        if (firstSpawn)
        {
            if (!multiPlayer)
            {
                if (player1Lives > 0)
                {
                    if (player1Tank == null)
                    {
                        respawnPlayer(1);
                    }
                }
            }

            if (multiPlayer)
            {
                if (player1Tank == null)
                {
                    if (player1Lives > 0)
                    {
                        respawnPlayer(1);
                    }
                }

                if (player2Tank == null)
                {
                    if (player2Lives > 0)
                    {
                        respawnPlayer(2);
                    }
                }
            }
        }
    }
    public void respawnPlayer(int player)
    {
        if (player == 1)
        {
            if (!player1RespawnTimeActive)
            {
                timeBeforePlayer1Respawn = Time.time + player1RespawnTime;
                player1RespawnTimeActive = true;
            }
            if (timeBeforePlayer1Respawn < Time.time)
            {
                GameObject newPlayerObj = Instantiate(player1ControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                playerSpawnTransform = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count)];
                player1Tank = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

                PlayerController newController = newPlayerObj.GetComponent<PlayerController>();
                Pawn newPawn = player1Tank.GetComponent<Pawn>();

                newController.pawn = newPawn;

                newPawn.controller = newController;

                player1RespawnTimeActive = false;
                player1Lives--;
            }
        }
        else
        {
            if (!player2RespawnTimeActive)
            {
                timeBeforePlayer2Respawn = Time.time + player2RespawnTime;
                player2RespawnTimeActive = true;
            }
            if (timeBeforePlayer2Respawn < Time.time)
            {
                GameObject newPlayerObj = Instantiate(player2ControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                playerSpawnTransform = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count)];
                player2Tank = Instantiate(tankPawnPrefab2, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

                PlayerController newController = newPlayerObj.GetComponent<PlayerController>();
                Pawn newPawn = player2Tank.GetComponent<Pawn>();

                newController.pawn = newPawn;

                newPawn.controller = newController;

                player2RespawnTimeActive = false;
                player2Lives--;
            }
        }
    }


    public void spawnPlayer()
    {
        GameObject newPlayerObj = Instantiate(player1ControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        playerSpawnTransform = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count)];
        player1Tank = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        PlayerController newController = newPlayerObj.GetComponent<PlayerController>();
        Pawn newPawn = player1Tank.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;

        if (!firstSpawn)
        {
            if (multiPlayer)
            {
                newPlayerObj = Instantiate(player2ControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                playerSpawnTransform = playerSpawnPoints[Random.Range(0, playerSpawnPoints.Count)];
                player2Tank = Instantiate(tankPawnPrefab2, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

                newController = newPlayerObj.GetComponent<PlayerController>();
                newPawn = player2Tank.GetComponent<Pawn>();

                newController.pawn = newPawn;

                newPawn.controller = newController;
            }
        }
    }

    public GameObject GetPlayer1Tank()
    {
        return player1Tank;
    }

    public GameObject GetPlayer2Tank()
    {
        return player2Tank;
    }
}
