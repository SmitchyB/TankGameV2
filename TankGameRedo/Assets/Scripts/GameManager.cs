using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //player controller prefab
    public GameObject playerControllerPrefab;
    //tank pawn prefab
    public GameObject tankPawnPrefab;
    //transform for player spawn
    public Transform playerSpawnTransform;

    //Creating the lists for the controllers, player controllers, and pawns
    public List<Controller> controllers = new List<Controller>();
    public List<PlayerController> playerControllers = new List<PlayerController>();
    public List<AIController> aIControllers = new List<AIController>();
    public List<Pawn> pawns = new List<Pawn>();

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
        //runs the spawn player function
        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnPlayer()
    {
        //spawns the player at 0,0,0 with no rotation
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        //spawns and connects the pawn to the controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;
        //gets the player controller and pawn components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        //hooks them up
        newController.pawn = newPawn;
    }
}
