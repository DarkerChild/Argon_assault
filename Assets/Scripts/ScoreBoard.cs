using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score;
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = 0.ToString();
    }

    public void UpdateScoreDisplay(string currentScore)
    {
        scoreText = GetComponent<Text>();
        scoreText.text = currentScore;
    }
}
