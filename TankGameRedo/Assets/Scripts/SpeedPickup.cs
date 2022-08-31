using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : Pickup
{
    public float speedAmount;
    public float speedBoostDuration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        // If the other object has a PowerupController
        if (other.tag == "Tank")
        {
            Pawn pawn = other.GetComponent<Pawn>();
            pawn.setSpeedBoost(true, speedBoostDuration, speedAmount);
            // Destroy this pickup
            Destroy(gameObject);
        }
    }
}