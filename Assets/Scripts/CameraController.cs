using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject cameraReticle;
    [SerializeField] CinemachineVirtualCamera firstPersonCamera;
    [SerializeField] CinemachineVirtualCamera thirdPersonCamera;
    CinemachineBrain cinemachineBrain;
    LayerMask firstPersonCameraMask;
    LayerMask thirdPersonCameraMask;
    
    float pitch;
    float yaw;

    Image reticleImage;
   
   void Start() 
   {
        Cursor.lockState = CursorLockMode.Locked;
        reticleImage = cameraReticle.GetComponent<Image>();
        cinemachineBrain = GetComponent<CinemachineBrain>();
        firstPersonCamera.enabled = true;
        thirdPersonCamera.enabled = false;
        thirdPersonCameraMask = ~0;
        firstPersonCameraMask = ~LayerMask.GetMask("Player");
   }
    void Update()
    {
        if(firstPersonCamera.enabled){
            HandleCameraRotation();
        }
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

        firstPersonCamera.transform.eulerAngles = new Vector3(-pitch, yaw, 0f);
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

    public void SwitchCameraView() {
        if(firstPersonCamera.enabled) {
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
            Camera.main.cullingMask = thirdPersonCameraMask;
            FindObjectOfType<PlayerInput>().SwitchCurrentActionMap("UI");
        }
        else if(thirdPersonCamera.enabled) {
            thirdPersonCamera.enabled = false;
            firstPersonCamera.enabled = true;
            Camera.main.cullingMask = firstPersonCameraMask;
            FindObjectOfType<PlayerInput>().SwitchCurrentActionMap("Default");
        }
        else {
            thirdPersonCamera.enabled = false;
            firstPersonCamera.enabled = true;
        }
    }

    public void SetCameraFocusTarget(Transform transform) {
        thirdPersonCamera.m_LookAt = transform;
    }
}
