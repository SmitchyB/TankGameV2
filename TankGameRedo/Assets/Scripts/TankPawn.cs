using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private float timeUntilNextEvent;
    private float shotsPerSecond;
    private bool readyToFire;

    public override void Start()
    {
        base.Start();
        //converts seconds per shot to shots per second
        shotsPerSecond = 1 / fireRate;
        timeUntilNextEvent = shotsPerSecond;
        readyToFire = true;
    }

    public override void Update()
    {
        base.Update();
        //addds the delay to shooting
        timeUntilNextEvent -= Time.deltaTime;
        if (timeUntilNextEvent <= 0)
        {
            timeUntilNextEvent = shotsPerSecond;
            readyToFire = true;
        }
    }
    //functions for the respective directions
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }
    public override void Shoot()
    {
        if (readyToFire)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            readyToFire = false;
        }
    }
    public override void RotateTowards(Vector3 targetPosition)
    {
        //allows the AI to rotate towards the target.
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
