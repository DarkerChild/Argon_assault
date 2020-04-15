using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerWeapons : MonoBehaviour
{
    GameObject weaponsParentObject;
    GameObject[] weaponsObjects;


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
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            SetLasersActive(weaponsObjects, true);
        }
        else
        {
            SetLasersActive(weaponsObjects, false);
        }
    }

    private void SetLasersActive(GameObject[] weaponsObjects, bool isActive)
    {
        foreach (GameObject weapon in weaponsObjects)
        {
            if (weapon.tag == "Laser") {
                var weaponParticleEmision = weapon.GetComponent<ParticleSystem>().emission;
                weaponParticleEmision.enabled = isActive;
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