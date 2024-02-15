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
    public void SetUpGreetingInteraction(TableController tableController, PartyController partyController) {
        table = tableController;
        party = partyController;
    }

    public void TestGreetingInteraction(){
        Debug.Log("Table Greeted");
    }

    public void ClearOldGreetingInteraction() {
        party = null;
        table = null;
    }

    public IEnumerator RunGreetingInteraction() {
        Debug.Log("Welcome to the restaurant!");
        //the camera needs to switch to the over-the-shoulder 3rd person camera
            //lock in place above player, look at center of table to view entire NPC party
        //player controls need to switch from Movement to Interaction/Notetaking
        //typewriter dialogue box appears on bottom of player's screen 
            //typewriter script writing should be it's own script as it will be used for orders as well
        //after the dialogue is finished, player controls and camera return to normal
            
        party.timeGreeted = FindObjectOfType<GameTimer>().GetRunningTime();
        //adjust party's mood based on difference in time greeted vs time seated
        if(party.timeGreeted - party.timeSeatedAtTable > party.GetPartyPatienceMultiplier() * 5f) {
            party.AdjustPartyMood(-1 * ((party.timeGreeted - party.timeSeatedAtTable) - party.GetPartyPatienceMultiplier() * 10f));
        }
        party.wasGreeted = true;
        yield return new WaitForSecondsRealtime(party.GetDeliberationDelay());
        party.isReadyToOrder = true;
        //put a timer above the table to indicate when they're ready to order
    }
}
