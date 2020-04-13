using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float scorePerSecond = 100f;
    public float LevelScore = 0;
    public float GameScore = 0;

    ScoreBoard scoreBoard;

    void Start()
    {
        int numScoreObjects = FindObjectsOfType<PlayerStats>().Length;        //if more than music player in scene the ndestroy ourselves
        if (numScoreObjects > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            scoreBoard = FindObjectOfType<ScoreBoard>();
        }
    }

    void Update()
    {
        LevelScore += Time.deltaTime * scorePerSecond;
        string scoreString = Mathf.FloorToInt(LevelScore).ToString();
        scoreBoard.UpdateScoreDisplay(scoreString);
    }

    public void ChangeLevelScore(float scoreModifier)
    {
        LevelScore += scoreModifier;
    }
}
