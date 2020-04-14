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
    float damageReceived;

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
        ProcessDamage(other);
        if (enemyHealth <= Mathf.Epsilon)
        {
            KillEnemy();
        }
    }

    private void ProcessDamage(GameObject other)
    {
        damageReceived = other.GetComponent<Weapon>().damage;
        print(gameObject.name + " took " + damageReceived.ToString() + " damage");
        enemyHealth = enemyHealth - damageReceived;
        print(gameObject.name + " health =  " + enemyHealth.ToString());
    }

    public void KillEnemy()
    {
        RunDeathFX();
        DisableRenderedAndCollisions();
        UpdatePlayerScore();
        Destroy(this.gameObject, 1f);
    }

    private void UpdatePlayerScore()
    {
        playerStats.AddLevelScore(enemyValue);
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
