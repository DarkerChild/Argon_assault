using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] int forwardNormalSpeed = 20;
    [Tooltip("In ms^-1")] [SerializeField] int forwardFastSpeed = 40;
    [Tooltip("In ms^-1")] [SerializeField] int forwardSlowSpeed = 10;
    [Tooltip("m")] [SerializeField] int distFromCameraNormal = 5;
    [Tooltip("m")] [SerializeField] int distFromCameraFast = 6;
    [Tooltip("m")] [SerializeField] int distFromCameraSlow = 4;

    [Header("Contrl-based rotation")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -30f;
    [SerializeField] float controlYawFactor = -30f;

    [Header("Position-based rotation")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float positionPitchFactor = -5f;

    //Non-serialized variables
    float xThrow, yThrow;
    bool controlActive = true;
    int currentSpeed;

    private void Start()
    {
        currentSpeed = forwardNormalSpeed;
    }

    void Update()
    {
        if (controlActive)
        {
            UpdateShipMovement();
        }
    }

    private void UpdateShipMovement()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFlightSpeed();
    }

    private void ProcessFlightSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = forwardFastSpeed;
            transform.localPosition = SetDistanceFromCamera(distFromCameraFast);
            //transform.position = new Vector3(transform.position.x,transform.position.y,((distFromCameraFast - transform.position.z) / 2) + transform.position.z);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = forwardSlowSpeed;
            transform.localPosition = SetDistanceFromCamera(distFromCameraSlow);
            //transform.position = new Vector3(transform.position.x, transform.position.y, ((distFromCameraSlow - transform.position.z) / 2) + transform.position.z);
        }
        else
        {
            currentSpeed = forwardNormalSpeed;
            transform.localPosition = SetDistanceFromCamera(distFromCameraNormal);
            //transform.position = new Vector3(transform.position.x, transform.position.y, ((distFromCameraNormal - transform.position.z) / 2) + transform.position.z);
        }
        print(currentSpeed);
    }

    private Vector3 SetDistanceFromCamera(float targetDist)
    {
        return new Vector3(transform.localPosition.x, transform.localPosition.y, ((targetDist - transform.localPosition.z) * Time.deltaTime * 2f) + transform.localPosition.z);
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

    public void SetControlActive(bool isActive)
    {
        controlActive = isActive;
    }
}
