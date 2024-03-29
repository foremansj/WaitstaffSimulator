using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] float walkSpeed;
    CameraController cameraController;
    PlayerInput playerInput;
    Vector2 moveInput;
    
    private void Start() 
    {
        playerInput = GetComponent<PlayerInput>();
        cameraController = GetComponent<CameraController>();
        playerInput.SwitchCurrentActionMap("Default");
    }
    private void Update() 
    {
        if(playerInput.currentActionMap.name == "Default"){
            HandleMovement();
            HandleRotation();
        }
        
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    void HandleMovement()
    {
        float xSpeed = moveInput.x * walkSpeed * Time.deltaTime;
        float zSpeed = moveInput.y * walkSpeed * Time.deltaTime;

        if(moveInput.magnitude > Mathf.Epsilon)
        {
            playerRigidbody.transform.Translate(xSpeed, 0, zSpeed);
        }

        else
        {
            playerRigidbody.position = new Vector3(playerRigidbody.position.x, 0, playerRigidbody.position.z);
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    void HandleRotation()
    {
        // removed Time.deltaTime, was origninally to try and smooth camera rotation, not sure this is in right place, or is right at all
        Quaternion cameraRotation = Camera.main.transform.rotation; 
        gameObject.transform.rotation = new Quaternion(0, cameraRotation.y, 0, cameraRotation.w);
    }
}
