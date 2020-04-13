using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int LevelScore = 0;
    public int GameScore = 0;

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
        }
    }
}
