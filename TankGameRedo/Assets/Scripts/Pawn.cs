using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public PlayerController controller;
   
    public Mover mover;
    public Shooter shooter;
    // Variable for move speed
    public float moveSpeed;
    private float speedBoostDuration;
    private float tmpSpeed;
    private bool speedBoostAdded;
    // Variable for turn speed
    public float turnSpeed;

    public GameObject shellPrefab;
    // Variable for our firing force
    public float fireForce;
    // Variable for our damage done
    public float damageDone;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;

    public float fireRate;
    private int score;

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
        shooter = GetComponent<Shooter>();
        tmpSpeed = moveSpeed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (speedBoostAdded)
        {
            if (Time.time > speedBoostDuration)
            {
                moveSpeed = tmpSpeed;
                speedBoostAdded = false;
            }
        }
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
    public void setSpeedBoost(bool speedBoost, float duration, float speedToAdd)
    {
        speedBoostAdded = speedBoost;
        speedBoostDuration = Time.time + duration;
        moveSpeed += speedToAdd;
    }
    public void addScore()
    {
        score++;
    }

    public int getScore()
    {
        return score;
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
