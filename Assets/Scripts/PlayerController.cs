using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 3.8f;
    [Tooltip("In m")] [SerializeField] float yRange = 2.75f;

    [Header("Contrl-based rotation")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -30f;

    [Header("Position-based rotation")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float positionPitchFactor = -5f;

    //Non-serialized variables
    float xThrow, yThrow;

    bool controlFrozen = false;

    void Update()
    {
        if (!controlFrozen)
        {
            UpdateShipMovement();
        }
    }

    private void UpdateShipMovement()
    {
        ProcessTranslation();
        ProcessRotation();
    }
    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float NewXPos = Mathf.Clamp(rawXPos, xRange * -1, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float NewYPos = Mathf.Clamp(rawYPos, yRange * -1, yRange);

        transform.localPosition = new Vector3(NewXPos, NewYPos, transform.localPosition.z);
        
    }
    private void ProcessRotation()
    {
        float controlPitch;
        if ((transform.localPosition.y != yRange) && (transform.localPosition.y != yRange*-1))
        {
            controlPitch = CrossPlatformInputManager.GetAxis("Vertical") * controlPitchFactor;
        }
        else
        {
            controlPitch = 0f;
        }
        float positionPitch = transform.localPosition.y * positionPitchFactor;

        float Pitch = positionPitch + controlPitch;
        float Yaw = transform.localPosition.x * positionYawFactor;
        float roll;
        if ((transform.localPosition.x != xRange) && (transform.localPosition.x != xRange*-1))
        {
            roll = CrossPlatformInputManager.GetAxis("Horizontal") * controlRollFactor;
        }
        else
        {
            roll = 0f;
        }
        

        transform.localRotation = Quaternion.Euler(Pitch, Yaw, roll);
    }

    void StopMovement()
    {
        controlFrozen = true;
    }
}
