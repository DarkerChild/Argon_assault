using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float Speed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 4f;
    [Tooltip("In m")] [SerializeField] float yRange = 4f;
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;

    void Update()
    {        
        UpdateShipMovement();
    }

    private void UpdateShipMovement()
    {
        ProcessTranslation();
        ProcessRotation();
    }
    private void ProcessTranslation()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * Time.deltaTime * Speed;
        float rawXPos = transform.localPosition.x + xOffset;
        float NewXPos = Mathf.Clamp(rawXPos, xRange * -1, xRange);

        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * Time.deltaTime * Speed;
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


}
