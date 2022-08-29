using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Variable to hold our Pawn
    public Pawn pawn;

    // Start is called before the first frame update
    public virtual void Start()
    {
        //if we have a game manager
        if (GameManager.instance != null)
        {
            //tracks the player
            if (GameManager.instance.controllers != null)
            {
                //register with the game manager
                GameManager.instance.controllers.Add(this);
            }
        }
    }
    // Update is called once per frame
    public virtual void Update()
    {
    }

    public virtual void OnDestroy()
    {
        //if we have a gamemanager
        if (GameManager.instance != null)
        {
            //tracks the player
            if (GameManager.instance.controllers != null)
            {
                //deregister with the gamemanager
                GameManager.instance.controllers.Remove(this);
            }
        }
    }
}
