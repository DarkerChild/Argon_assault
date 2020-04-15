using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float loadLevelDelay = 1f;
    [Tooltip("Death Effects Object")] [SerializeField] GameObject deathFX;

    float invulnerabilityTimeOnHit = 3f;
    int currentLives;

    PlayerStats PlayerStats;


    private void Start()
    {
        PlayerStats = FindObjectOfType<PlayerStats>();
    }


    void OnTriggerEnter(Collider collider)
    {
        currentLives = PlayerStats.currentLives;
        if (currentLives>0)
        {
            PlayerDamagedSequence();
        }
        else
        {
            StartDeathSequence();
        }
        
    }

    private void PlayerDamagedSequence()
    {
        PlayerStats.UpdateLives(-1);
    }

    private void StartDeathSequence()
    {
        SendMessage("StopMovement");
        deathFX.SetActive(true);
        Invoke("RestartLevel", loadLevelDelay);

        //Reset level score
        PlayerStats.ResetLevelScore();
    }

    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        //PlayerWeapons playerWeapons = GetComponent<PlayerWeapons>();
        //playerWeapons.GetAllWeapons();
    }
}
