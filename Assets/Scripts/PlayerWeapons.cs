using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerWeapons : MonoBehaviour
{
    ParticleSystem leftLaser;
    ParticleSystem rightLaser;

    // Start is called before the first frame update
    void Start()
    {
        leftLaser = GetChildWithName(gameObject, "Left Laser").GetComponent<ParticleSystem>();
        rightLaser = GetChildWithName(gameObject, "Right Laser").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RunWeaponsCheck();
    }

    private void RunWeaponsCheck()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            leftLaser.Play();
            rightLaser.Play();
        }
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            leftLaser.Stop();
            rightLaser.Stop();
        }
    }
    public GameObject GetChildWithName(GameObject obj, string name)
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