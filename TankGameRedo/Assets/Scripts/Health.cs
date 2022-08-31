using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int) maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth((int) currentHealth);
    }

    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        healthBar.SetHealth((int)currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            die(source);

        }
    }

    public void die(Pawn source)
    {
        if (GameManager.instance.GetPlayer1Tank() != null)
        {
            if (source.gameObject == GameManager.instance.GetPlayer1Tank())
            {
                GameManager.instance.Player1Score++;
            }
        }
        if(GameManager.instance.GetPlayer2Tank() != null)
        {
            if(source.gameObject == GameManager.instance.GetPlayer2Tank())
            {
                GameManager.instance.Player2Score++;
            }
        }

        Pawn pawn = GetComponent<Pawn>();
        if (pawn.controller != null)
        {
            Destroy(pawn.controller.gameObject);
        }
        Destroy(gameObject);
    }
}
