using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingInteraction : MonoBehaviour
{
    [SerializeField] PlayerInteraction player; 
    [SerializeField] FirstPersonCamera playerCamera;
    [SerializeField] Camera thirdPersonCamera;
    [SerializeField] UIController uIController;
    public PartyController party;
    public TableController table;
    
    
    

    // if the script lives on the player, we can assign certain variables once and be done with it (player, cameras)
    // then the only vars that need to change are the Party and Table that are being interacted with

    //so the first thing that happens when a table is greeted is that that the party and table variables need to be set
    public void SetUpGreeting(TableController tableController, PartyController partyController) {
        table = tableController;
        party = partyController;
    }

    

    public void TestGreetingInteraction(){
        Debug.Log("Table Greeted");
    }

    public void ClearOldGreeting() {
        party = null;
        table = null;
    }

    public void RunGreetingDialogue() {
        Debug.Log("Welcome to the restaurant!");
        party.wasGreeted = true;
        //the camera needs to switch to the over-the-shoulder 3rd person camera
            //lock in place above player, look at center of table to view entire NPC party
        //player controls need to switch from Movement to Interaction/Notetaking
        //typewriter dialogue box appears on bottom of player's screen 
            //typewriter script writing should be it's own script as it will be used for orders as well
        //after the dialogue is finished, player controls and camera return to normal
        //the party is marked as having been greeted
        //the customers of the party will now generate their food order
        //need to start a delay/countdown until the party is ready to order
            //put a timer above the table?
        party.timeGreeted = FindObjectOfType<GameTimer>().GetRunningTime();
    }
}
