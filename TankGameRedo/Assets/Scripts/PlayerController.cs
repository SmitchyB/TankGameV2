using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode clockwise;
    public KeyCode counterClockwise;

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
        }
        if (Input.GetKey(backward))
        {
            pawn.MoveBackward();
        }
        if (Input.GetKey(clockwise))
        {
            pawn.RotateClockwise();
        }
        if (Input.GetKey(counterClockwise))
        {
            pawn.RotateCounterClockwise();
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
