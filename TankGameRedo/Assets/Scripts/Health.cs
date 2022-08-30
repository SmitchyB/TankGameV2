using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        if (currentHealth <= 0)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            die(source);
        }
    }

    public void die(Pawn source)
    {
        Destroy(gameObject);
    }
}
