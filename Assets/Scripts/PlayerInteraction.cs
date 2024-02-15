using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float raycastDistance = 2.5f;
    public PlayerInput playerInput;
    bool isInteractPressed;
    FirstPersonCamera firstPersonCamera;
    Transform interactionTarget;
    GreetingInteraction greetingInteraction;
    OrderingInteraction orderingInteraction;
    
    private void Awake() {
        firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
    }
    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        greetingInteraction = GetComponent<GreetingInteraction>();
        orderingInteraction = GetComponent<OrderingInteraction>();
    }

    private void Update() {
        CheckIfInteractable();
    }

    private void OnInteract(InputValue value) {
        isInteractPressed = value.isPressed;
        if(isInteractPressed) {
        }
    }

    private void CheckIfInteractable() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = ~LayerMask.GetMask("Player", "Ignore Raycast"); //ray does not touch player or ignore ray objects
        //consider adding another layer to this, where the reticle turns blue if an object is interactable but too far away
        if (Physics.Raycast(ray, out hit, raycastDistance, mask)) {    
            interactionTarget = hit.transform;
            if(interactionTarget.GetComponent<CustomerController>() != null) {
                string stepOfService = interactionTarget.GetComponentInParent<PartyController>().GetCurrentStepOfService();
                PartyController customerParty = interactionTarget.GetComponentInParent<PartyController>();
                if(stepOfService == "Greeting") {
                        firstPersonCamera.ChangeReticleColor(Color.green);
                        if(isInteractPressed) {
                            greetingInteraction.ClearOldGreetingInteraction();
                            greetingInteraction.SetUpGreetingInteraction(customerParty.GetAssignedTable(), customerParty);
                            StartCoroutine(greetingInteraction.RunGreetingInteraction());
                        }
                }
                else if(stepOfService == "Ordering" && customerParty.isReadyToOrder) {
                        firstPersonCamera.ChangeReticleColor(Color.green);
                        if(isInteractPressed) {
                            orderingInteraction.ClearOldOrderingInteraction();
                            orderingInteraction.SetUpOrderingInteraction(customerParty.GetAssignedTable(), customerParty);
                            orderingInteraction.RunOrderingInteraction();
                        }
                }
                else {
                        firstPersonCamera.ChangeReticleColor(Color.red);
                }
            }
            else if(interactionTarget.GetComponent<TableController>() != null) {
                firstPersonCamera.ChangeReticleColor(Color.red);
                //begin interacting with table
                //either customer interactions
                //or plate interactions
                //or checkbook interactions
            }
            else if(interactionTarget.GetComponent<PlateController>() != null) {
                //begin interacting with plate/food
            }
            else if(interactionTarget.GetComponent<POSController>() != null) {
                //begin interacting with POS
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
}
