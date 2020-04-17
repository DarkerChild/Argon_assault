using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    //[Tooltip("In m")] [SerializeField] float xRange = 3.8f;
    //[Tooltip("In m")] [SerializeField] float yRange = 2.75f;

    [Header("Contrl-based rotation")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -30f;
    [SerializeField] float controlYawFactor = -30f;

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
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        transform.localPosition += new Vector3(xThrow, yThrow, 0) * Time.deltaTime * controlSpeed;

        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void ProcessRotation()
    {
        float controlPitch = CrossPlatformInputManager.GetAxis("Vertical") * controlPitchFactor;
        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float Pitch = positionPitch + controlPitch;

        float controlYaw = CrossPlatformInputManager.GetAxis("Horizontal") * controlYawFactor;
        float positionYaw = transform.localPosition.x * positionYawFactor; ;
        float Yaw = controlYaw + positionYaw;

        float roll = CrossPlatformInputManager.GetAxis("Horizontal") * controlRollFactor;

        
        transform.localRotation = Quaternion.Euler(Pitch, Yaw, roll);
    }
}
