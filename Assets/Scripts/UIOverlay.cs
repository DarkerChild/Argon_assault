using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    GameObject scoreObject;
    GameObject livesObject;

    PlayerStats playerStats;

    int currentScore;
    int currentLives;
    Text scoreText;
    Text livesText;

    void Start()
    {
        CheckIfSingleton();
        GetUIElements();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void CheckIfSingleton()
    {
        int numUIOverlayObjects = FindObjectsOfType<Canvas>().Length;        //if more than music player in scene the ndestroy ourselves
        if (numUIOverlayObjects > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void GetUIElements()
    {
        scoreObject = GameObject.Find("Score Display");
        scoreText = scoreObject.GetComponent<Text>();
        livesObject = GameObject.Find("Lives");
        livesText = livesObject.GetComponent<Text>();
    }

    void Update()
    {
        UpdateUIElements();
    }

    private void UpdateUIElements()
    {
        currentScore = Mathf.FloorToInt(playerStats.LevelScore);
        scoreText.text = currentScore.ToString();

        currentLives = playerStats.currentLives;
        livesText.text = "Lives:" + currentLives.ToString();
    }
}
