using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth = 50f;
    [SerializeField] float enemyValue = 100f;
    [SerializeField] GameObject deathFX;
    // Start is called before the first frame update

    PlayerStats playerStats;

    void Start()
    {
        AddBoxCollider();
        playerStats = FindObjectOfType<PlayerStats>();
    }


    void AddBoxCollider()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        enemyHealth = enemyHealth - enemyHealth;
        if (enemyHealth <= Mathf.Epsilon)
        {
            StartEnemyDeath();
        }
    }

    void StartEnemyDeath()
    {
        RunDeathFX();
        DisableRenderedAndCollisions();
        UpdatePlayerScore();
        Destroy(gameObject, 1f);
    }

    private void UpdatePlayerScore()
    {
        //Give player points
        playerStats.ChangeLevelScore(enemyValue);
    }

    private void DisableRenderedAndCollisions()
    {
        //Disable rendering and collision
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    private void RunDeathFX()
    {
        //Create Death FX
        GameObject explosionObject = Instantiate(deathFX, transform.position, Quaternion.identity);
        explosionObject.transform.parent = gameObject.transform;
    }
}
