using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingInteraction : MonoBehaviour
{
    [SerializeField] PlayerInteraction player; 
    [SerializeField] CameraController playerCameraController;
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

    public IEnumerator RunOrderingInteraction() { 
        Debug.Log("Taking table " + table.GetTableNumber() + "'s order");
        party.timeOrderPlaced = FindObjectOfType<GameTimer>().GetRunningTime();
        //if the player takes longer than the party's patience setting, they will lose mood
        //the time the order was taken minus the time it was greeted minus the delay represents when they were ready to order
        //the patience multiplier represents an appropriate time for the party to wait before ordering after being ready to order
        //will need to be fine-tuned for actual gameplay
        if((party.timeOrderPlaced - party.timeGreeted - party.GetDeliberationDelay()) > (party.GetPartyPatienceMultiplier() * 5f)) {
            party.AdjustPartyMood((party.GetPartyPatienceMultiplier() * 5f) - (party.timeOrderPlaced - party.timeGreeted - party.GetDeliberationDelay()));
        }
        playerCameraController.SwitchCameraView();
        playerCameraController.SetCameraFocusTarget(table.transform);
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < table.GetAssignedParty().GetCustomers().Count; i++) {
            playerCameraController.SetCameraFocusTarget(table.GetAssignedParty().GetCustomers()[i].transform);
            yield return new WaitForSeconds(3f);
        }
        //just need to implement UI elements, buttons and whatnot so player can move to next customer manually
        //will need to re-write this method when that is ready
        playerCameraController.SwitchCameraView();
        party.hasOrdered = true;
    }
}
