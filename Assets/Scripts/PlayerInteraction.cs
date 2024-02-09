using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float raycastDistance = 2.5f;
    public PlayerInput playerInput;
    bool isInteractPressed;
    FirstPersonCamera firstPersonCamera;
    
    private void Awake() {
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
    }
    private void Start() {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update() {
        CheckIfInteractable();
    }

    void OnInteract(InputValue value) {
        isInteractPressed = value.isPressed;
    }

    void CheckIfInteractable() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = ~LayerMask.GetMask("Player", "Ignore Raycast"); //ray does not touch player or ignore ray objects
        
        if (Physics.Raycast(ray, out hit, raycastDistance, mask)) {    
            Transform objectHit = hit.transform;
            if(objectHit.GetComponent<InteractableObject>() != null) {
                
                if(objectHit.GetComponent<InteractableObject>() == true) {
                    firstPersonCamera.ChangeReticleColor(Color.green);
                    if(isInteractPressed)
                    {
                        Debug.Log("Trying to interact");
                    }
                }    

                else {
                    firstPersonCamera.ChangeReticleColor(Color.red);
                }
            }
            
            else {
                firstPersonCamera.ChangeReticleColor(Color.red);
                return;
            }
        }

        else {
            firstPersonCamera.ChangeReticleColor(Color.red);
                return;
        }
    }
}
