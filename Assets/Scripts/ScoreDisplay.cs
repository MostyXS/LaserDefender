using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreText = GetComponent<Text>();

    }
    private void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();

    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
