using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] float scorePerSecond = 100f;
    [SerializeField] int startingLives = 3;
    public float LevelScore = 0;
    public float GameScore = 0;
    public int currentLives;

    ScoreBoard scoreBoard;

    void Start()
    {
        bool isSingleton = SingletonCheck();
        if (isSingleton)
        {
            StartUpActions();
        }
    }

    private void StartUpActions()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        currentLives = startingLives;
    }

    private bool SingletonCheck()
    {
        int numScoreObjects = FindObjectsOfType<PlayerStats>().Length;        //if more than music player in scene the ndestroy ourselves
        if (numScoreObjects > 1)
        {
            Destroy(gameObject);
            return false;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            return true;
        }
    }

    void Update()
    {
        LevelScore += Time.deltaTime * scorePerSecond;
    }

    public void AddLevelScore(float scoreModifier)
    {
        LevelScore += scoreModifier;
    }

    public void UpdateLives(int noOfLives)
    {
        currentLives += noOfLives;
    }

    public void ResetLevelScore()
    {
        LevelScore = 0f;
    }

    public void ResetLives()
    {
        currentLives = startingLives;
    }
}
