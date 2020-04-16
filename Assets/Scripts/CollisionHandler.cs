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
    BoxCollider BoxCollider;
    MeshRenderer MeshRenderer;



    private void Start()
    {
        PlayerStats = FindObjectOfType<PlayerStats>();
        BoxCollider = gameObject.GetComponent<BoxCollider>();
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
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
        StartCoroutine(PlayerInvulnerable(2f, .2f));
    }

    IEnumerator PlayerInvulnerable(float duration, float blinkTime)
    {
        while (duration > 0f)
        {
            BoxCollider.enabled = false;
            duration -= (Time.deltaTime + blinkTime);
            print(duration.ToString());

            //toggle renderer
            MeshRenderer.enabled = !MeshRenderer.enabled;

            //wait for a bit
            yield return new WaitForSeconds(blinkTime);
        }

        //make sure renderer is enabled when we exit
        BoxCollider.enabled = true;
        MeshRenderer.enabled = true;
    }

    private void StartDeathSequence()
    {
        SendMessage("StopMovement");
        deathFX.SetActive(true);
        MeshRenderer.enabled = false;
        Invoke("RestartLevel", loadLevelDelay);
        //Reset level score
        PlayerStats.ResetLevelScore();
        PlayerStats.ResetLives();
    }

    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
