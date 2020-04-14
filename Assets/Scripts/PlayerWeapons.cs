﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerWeapons : MonoBehaviour
{
    GameObject weaponsParentObject;
    GameObject[] weaponsObjects;
    ParticleSystem weaponParticleSystem;

    void Start()
    {
        weaponsParentObject = GetChildWithName(gameObject, "Weapons");
        weaponsObjects = GetAllWeapons();
    }

    void Update()
    {
        SetWeaponState(weaponsObjects);
    }

    public GameObject[] GetAllWeapons()
    {
        GameObject[] weaponsObjects = new GameObject[weaponsParentObject.transform.childCount];
        for (int i = 0; i < weaponsParentObject.transform.childCount; i++)
        {
            Transform weaponsObjectTransform = weaponsParentObject.transform;
            Transform weaponTransform = weaponsObjectTransform.GetChild(i);
            weaponsObjects[i] = weaponTransform.gameObject;
        }
        return weaponsObjects;
    }

    private void SetWeaponState(GameObject[] weaponsObjects)
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            ActivateLasers(weaponsObjects);
        }
        else if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            DeactivateLasers(weaponsObjects);
        }
    }



    private void ActivateLasers(GameObject[] weaponsObjects)
    {
        foreach (GameObject weapon in weaponsObjects)
        {
            if (weapon.tag == "Laser") {
                weaponParticleSystem = weapon.GetComponent<ParticleSystem>();
                if (weaponParticleSystem.isStopped)
                {
                    print(weapon.name + " firing");
                    weaponParticleSystem.Play();
                }
            }
        }
    }

    private void DeactivateLasers(GameObject[] weaponSystems)
    {
        foreach (GameObject weapon in weaponSystems)
        {
            if (weapon.tag == "Laser")
            {
                weaponParticleSystem = weapon.GetComponent<ParticleSystem>();
                if (weaponParticleSystem.isPlaying)
                {
                    print(weapon.name + " stopped");
                    weaponParticleSystem.Stop();
                }
            }
        }
    }

    private GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

}