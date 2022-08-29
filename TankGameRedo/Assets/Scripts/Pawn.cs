using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Mover mover;
    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //if we have a gamemanager
        if (GameManager.instance != null)
        {
            //and tracks the player
            if (GameManager.instance.pawns != null)
            {
                //register with the gamemanager
                GameManager.instance.pawns.Add(this);
            }
        }
        //gets the mover class
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public void OnDestroy()
    {
        //if we have a game manager
        if (GameManager.instance != null)
        {
            //tracks the player
            if (GameManager.instance.pawns != null)
            {
                //deregister with the gamemanager
                GameManager.instance.pawns.Remove(this);
            }
        }
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
}
