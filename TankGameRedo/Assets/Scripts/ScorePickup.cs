using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ScorePickup : Pickup
{
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
            if (GameManager.instance.GetPlayer1Tank() != null)
            {
                if (other.gameObject == GameManager.instance.GetPlayer1Tank())
                {
                    GameManager.instance.Player1Score++;
                }
            }
            if (GameManager.instance.GetPlayer2Tank() != null)
            {
                if (other.gameObject == GameManager.instance.GetPlayer2Tank())
                {
                    GameManager.instance.Player2Score++;
                }
            }
            Pawn pawn = other.GetComponent<Pawn>();
            pawn.addScore();
            // Destroy this pickup
            Destroy(gameObject);
        }
    }
}
