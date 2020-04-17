using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float loadLevelDelay = 1f;
    [Tooltip("Death Effects Object")] [SerializeField] GameObject deathFX = null;

    float invulnerabilityTimeOnHit = 3f;
    int currentLives;

    PlayerStats playerStats;
    PlayerController playerController;
    BoxCollider BoxCollider;
    MeshRenderer MeshRenderer;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerController = FindObjectOfType<PlayerController>();
        BoxCollider = gameObject.GetComponent<BoxCollider>();
        MeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.tag + " - OnParticleCollision");
        if (collider.gameObject.tag == "Pick Up") return;

        currentLives = playerStats.currentLives;
        if (currentLives>0)
        {
            PlayerDamagedSequence();
        }
        else
        {
            StartDeathSequence();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.gameObject.tag + " - OnParticleCollision");
        if (collision.collider.gameObject.tag == "Pick Up") return;

        currentLives = playerStats.currentLives;
        if (currentLives > 0)
        {
            PlayerDamagedSequence();
        }
        else
        {
            StartDeathSequence();
        }
    }

    
    void OnParticleCollision(GameObject other)
    {
        print(other.tag + " - OnParticleCollision");
        if ((other.tag == "Pick Up") || (other.tag == "Laser")) return;

        currentLives = playerStats.currentLives;
        if (currentLives > 0)
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
        StartCoroutine(PlayerInvulnerable(invulnerabilityTimeOnHit, .2f));
        playerStats.UpdateLives(-1);
    }

    IEnumerator PlayerInvulnerable(float duration, float blinkTime)
    {
        while (duration > 0f)
        {
            BoxCollider.enabled = false;
            duration -= (Time.deltaTime + blinkTime);

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
        playerController.SetControlActive(false);
        deathFX.SetActive(true);
        MeshRenderer.enabled = false;
        StartCoroutine(RestartLevel1(loadLevelDelay));


    }

    IEnumerator RestartLevel1(float loadLevelDelay)
    {
        while (true)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            yield return new WaitForSeconds(loadLevelDelay);
            SceneManager.LoadScene(currentSceneIndex);

            //Reset level score
            playerStats.ResetLevelScore();
            playerStats.ResetLives();
        }
    }
}
