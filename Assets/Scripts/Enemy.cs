using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemyHealth = 50f;
    [SerializeField] float enemyValue = 100f;
    [SerializeField] GameObject deathFX = null;
    [SerializeField] GameObject pickUp = null;
    // Start is called before the first frame update

    PlayerStats playerStats;
    float damageReceived;
    bool isAlive = true;

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
        if (other.tag == "Enemy Weapon") return; //Make sure you can't be hit by other enemies!!

        ProcessDamage(other);
        if (enemyHealth <= Mathf.Epsilon)
        {
            //Prevent duplication of this funciton when hit by multiple attacks at the same time.
            if (isAlive)
            {
                isAlive = false;
                KillEnemy();
            }
        }
    }

    private void ProcessDamage(GameObject other)
    {
        damageReceived = other.GetComponent<PlayerWeaponDamage>().damage;
        enemyHealth = enemyHealth - damageReceived;
    }

    public void KillEnemy()
    {
        RunDeathFX();
        DisableRendererAndCollider();
        UpdatePlayerScore();
        Destroy(gameObject, 1f);
        DropPickUp();
    }

    private void DropPickUp()
    {
        if (pickUp)
        {
            float random = UnityEngine.Random.value;
            print(random);
            if (random > 0.7f)
            {
                GameObject pickUpObject = Instantiate(pickUp, transform.position, Quaternion.identity);
            } 
        }
    }

    private void UpdatePlayerScore()
    {
        playerStats.AddLevelScore(enemyValue);
    }

    private void DisableRendererAndCollider()
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
