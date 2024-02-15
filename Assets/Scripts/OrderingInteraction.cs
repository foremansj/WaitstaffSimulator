using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingInteraction : MonoBehaviour
{
    [SerializeField] PlayerInteraction player; 
    [SerializeField] FirstPersonCamera playerCamera;
    [SerializeField] Camera thirdPersonCamera;
    [SerializeField] UIController uIController;
    public PartyController party;
    public TableController table;

    public void SetUpOrderingInteraction(TableController tableController, PartyController partyController) {
        table = tableController;
        party = partyController;
        foreach(CustomerController customer in party.GetCustomers()) {
            if(customer.appetizerOrder == null) {
                customer.DecideAppetizer();
            }
        }
    }

    public void ClearOldOrderingInteraction() {
        party = null;
        table = null;
    }

    public void RunOrderingInteraction() { 
        Debug.Log("Taking table " + table.GetTableNumber() + "'s order");
        //move camera to third person camera
        //player controls need to switch from Movement to Interaction/Notetaking
        //for each customer in the party, lock camera while they are ordering
            //give order via typewriter dialogue
            //after they are finished speaking, enable button to move to next customer or previous customer
        party.hasOrdered = true;
        party.timeOrderPlaced = FindObjectOfType<GameTimer>().GetRunningTime();
        //adjust party's mode based on when they were ready to order and when their order was taken
    }
}
