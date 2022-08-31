using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    private int score;
    private bool player1;
    private GameObject playerTank;
    // Start is called before the first frame update
    void Start()
    {
        playerTank = transform.parent.gameObject.transform.parent.gameObject;

        if (GameManager.instance.multiPlayer)
        {
            if (GameManager.instance.GetPlayer1Tank() != null)
            {
                if (playerTank == GameManager.instance.GetPlayer1Tank())
                {
                    score = GameManager.instance.Player1Score;
                    scoreText.text = "Score: " + score;
                    player1 = true;
                }
            }
            if (GameManager.instance.GetPlayer2Tank() != null)
            {
                if (playerTank == GameManager.instance.GetPlayer2Tank())
                {
                    score = GameManager.instance.Player2Score;
                    scoreText.text = "Score: " + score;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player1)
        {
            score = GameManager.instance.Player1Score;
            scoreText.text = "Score: " + score;
        }
        else
        {
            score = GameManager.instance.Player2Score;
            scoreText.text = "Score: " + score;
        }
    }
}
