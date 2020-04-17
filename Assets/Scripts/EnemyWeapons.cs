using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    [SerializeField] public float damage = 50f;
    //[SerializeField] GameObject enemyWeaponPrefab = null;
    [Tooltip("m")] [SerializeField] int playerSpottedDistance = 300;

    Transform playerShip;

    GameObject[] enemyWeaponsArray;

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.Find("Player Ship").transform;
        AddWeaponsToArray();
    }

    private void AddWeaponsToArray()
    {
        enemyWeaponsArray = new GameObject[transform.childCount];
        enemyWeaponsArray[0] = transform.GetChild(0).gameObject;
        enemyWeaponsArray[1] = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        AimAtPlayer();
        ShootAtPlayer();
    }

    private void AimAtPlayer()
    {
        foreach (GameObject weapon in enemyWeaponsArray)
        {
            weapon.transform.LookAt(playerShip.position);
        }
    }

    private void ShootAtPlayer()
    {
        if (PlayerIsInRange())
        {
            SetWeaponsActive(true);
        }
        else
        {
            SetWeaponsActive(false);
        }
    }

    private void SetWeaponsActive(bool isActive)
    {
        foreach (GameObject weapon in enemyWeaponsArray)
        {
            ParticleSystem particleSystem = weapon.GetComponent<ParticleSystem>();
            ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
            emissionModule.enabled = isActive;
        }
    }

    private bool PlayerIsInRange()
    {
        float distToPlayer = Vector3.Distance(transform.position, playerShip.position);
        return (distToPlayer < playerSpottedDistance);
    }
}
