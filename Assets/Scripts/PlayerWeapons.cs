using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] int aimDistance = 500;
    [SerializeField] int crosshairDistance = 50;

    GameObject weaponsParentObject;
    GameObject[] weaponsObjects;

    GameObject crosshair;

    void Start()
    {
        weaponsParentObject = GetChildWithName(gameObject, "Weapons");
        weaponsObjects = GetAllWeapons();
        crosshair = GameObject.Find("Crosshair");
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

    void Update()
    {
        AimWeapons();
        AimCrosshair();
        SetWeaponState(weaponsObjects);
    }

    private void AimWeapons()
    {
        //Aim the weapons
        foreach(GameObject weapon in weaponsObjects)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Ray ray = new Ray(gameObject.transform.position, Input.mousePosition - gameObject.transform.position);
            //Debug.DrawRay(gameObject.transform.position, Input.mousePosition - gameObject.transform.position);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            weapon.transform.LookAt(ray.GetPoint(aimDistance));
        }
    }

    private void AimCrosshair()
    {
        //Ray ray = new Ray(gameObject.transform.position, Input.mousePosition - gameObject.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 shipPosition = gameObject.transform.position;
        Vector3 rayAimPosition = ray.GetPoint(crosshairDistance);

        crosshair.transform.position = rayAimPosition - cameraPosition + shipPosition;
        crosshair.transform.LookAt(ray.origin);
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
                ParticleSystem.EmissionModule weaponParticleEmision = weapon.GetComponent<ParticleSystem>().emission;
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