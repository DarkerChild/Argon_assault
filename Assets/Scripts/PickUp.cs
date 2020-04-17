using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    enum PickUpType { extraLife };

    [SerializeField] PickUpType pickUpType = PickUpType.extraLife;

    PlayerStats playerStats;

    private void OnTriggerEnter(Collider other)
    {
        if (pickUpType == PickUpType.extraLife)
        {
            playerStats = FindObjectOfType<PlayerStats>();
            playerStats.UpdateLives(1);
            Destroy(gameObject);
        }
    }
}
