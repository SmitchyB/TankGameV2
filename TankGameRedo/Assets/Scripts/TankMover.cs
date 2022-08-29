using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody rb;

    // Start is called before the first frame update
    public override void Start()
    {
        //gets the rigidbody from the tank pawn
        rb = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        //moves the tank based on speed and time.deltatime instead of every frame
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);
    }

    public override void Rotate(float speed)
    {
        //rotates the tank based on speed * time.deltatime instead of over frame
        gameObject.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
