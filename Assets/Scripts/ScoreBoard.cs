using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    float levelScore;
    int lives;
    Text scoreText;
    Text livesText;

    PlayerStats PlayerStats;

    void Start()
    {
        PlayerStats = FindObjectOfType<PlayerStats>();
        scoreText = GetComponent<Text>();
        scoreText.text = PlayerStats.LevelScore.ToString();
        livesText = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        levelScore = PlayerStats.LevelScore;
        int Lives = PlayerStats.currentLives;
        string scoreString = Mathf.FloorToInt(levelScore).ToString();
        scoreText.text = scoreString.ToString();
    }
}
