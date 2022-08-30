using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode clockwise;
    public KeyCode counterClockwise;
    public KeyCode shootKey;

    public bool isMoving;

    public float volumeDistance;
    public float movingVolumeDistance;

    public override void Start()
    {
        //runs the base start function
        base.Start();
        //if we have a game manager
        if (GameManager.instance != null)
        {
            //tracks the player
            if (GameManager.instance.playerControllers != null)
            {
                //register with the gamemanager
                GameManager.instance.playerControllers.Add(this);
            }
        }
    }

    public override void Update()
    {
        //runs the provess inputs function
        processInputs();
    }
    //function for key inputs and then runs the respective functions from the pawn class
    public void processInputs()
    {
        if (Input.GetKey(forward))
        {
            pawn.MoveForward();
            isMoving = true;
        }
        if (Input.GetKey(backward))
        {
            pawn.MoveBackward();
            isMoving = true;
        }
        if (Input.GetKey(clockwise))
        {
            pawn.RotateClockwise();
            isMoving = true;
        }
        if (Input.GetKey(counterClockwise))
        {
            pawn.RotateCounterClockwise();
            isMoving = true;
        }
        if (!Input.GetKey(forward) && !Input.GetKey(backward) && !Input.GetKey(clockwise) && !Input.GetKey(counterClockwise))
        {
            isMoving = false;
        }

        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
          
        
        }
    }
    public override void OnDestroy()
    {
        //if we have a game manager
        if (GameManager.instance != null)
        {
            //tracks the player
            if (GameManager.instance.playerControllers != null)
            {
                //deregister with the gamemanager
                GameManager.instance.playerControllers.Remove(this);
            }
        }
    }
}

    
