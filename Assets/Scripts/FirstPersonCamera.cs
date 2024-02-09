using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject cameraReticle;
    
    float pitch;
    float yaw;

    Image reticleImage;
   
   void Start() 
   {
        Cursor.lockState = CursorLockMode.Locked;
        reticleImage = cameraReticle.GetComponent<Image>();
   }
    void Update()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        //need to add exception for when mouse is not moving to reduce/eliminate accidental camera shaking
        pitch += rotationSpeed * Input.GetAxis("Mouse Y");
        yaw += rotationSpeed * Input.GetAxis("Mouse X");

        pitch = Mathf.Clamp(pitch, -60f, 30f);

        while (yaw < 0f)
        {
            yaw += 360f;
        }
        while (yaw >= 360f)
        {
            yaw -= 360f;
        }

        transform.eulerAngles = new Vector3(-pitch, yaw, 0f);
    }

    public void ChangeCameraLock()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        else if(Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ChangeRotationSensitivity(float value)
    {
        rotationSpeed = value;
    }

    public void ChangeReticleColor(Color color) {
        reticleImage.color = color;
    }
}
