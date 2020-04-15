using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score;
    Text scoreText;

    PlayerStats PlayerStats;

    void Start()
    {
        PlayerStats = FindObjectOfType<PlayerStats>();
        scoreText = GetComponent<Text>();
        scoreText.text = PlayerStats.LevelScore.ToString();
    }

    private void Update()
    {
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        float LevelScore = PlayerStats.LevelScore;
        string scoreString = Mathf.FloorToInt(LevelScore).ToString();
        scoreText.text = scoreString.ToString();
    }
}
