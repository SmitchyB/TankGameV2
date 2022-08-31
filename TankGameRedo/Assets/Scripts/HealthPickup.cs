using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public float healAmount = 50;
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
            Health health = other.GetComponent<Health>();
            health.currentHealth += healAmount;
            health.currentHealth = Mathf.Clamp(health.currentHealth, 0, health.maxHealth);
            // Destroy this pickup
            Destroy(gameObject);
        }
    }
}
