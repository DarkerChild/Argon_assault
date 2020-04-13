using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float loadLevelDelay = 1f;
    [Tooltip("Death Effects Object")] [SerializeField] GameObject deathFX;

    bool isDead = false;


    void OnTriggerEnter(Collider collider)
    {
        if (!isDead)
        {
            StartDeathSequence();
        }
        
    }

    private void StartDeathSequence()
    {
        isDead = true;
        SendMessage("StopMovement");
        deathFX.SetActive(true);
        Invoke("RestartLevel", loadLevelDelay);

        //Reset level score
        GameObject PlayerStats = GameObject.Find("PlayerStats");
        PlayerStats playerStats = PlayerStats.GetComponent<PlayerStats>();
        playerStats.LevelScore = 0;
        print(playerStats.LevelScore);
    }

    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
