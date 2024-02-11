using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HostStand : MonoBehaviour
{
    [SerializeField] bool isOpenForBusiness;
    Waitlist waitlist;
    [SerializeField] List<TableController> openTables;
    List<TableController> occupiedTables;
    [SerializeField] float waitlistDelay = 10f;
    [SerializeField] GameObject waitingArea;
    Collider hostCollider; 
    
    private void Awake() {
        waitlist = FindObjectOfType<Waitlist>();
    }
    private void Start() {
        occupiedTables = new List<TableController>();
        hostCollider = GetComponentInChildren<BoxCollider>();
        StartCoroutine(SeatFromWaitList());
    }
    public void CheckWaitlist(PartyController party){
        if(party.GetAssignedTable() == null) {
            if(party.GetReservationStatus() == true) {
                if(waitlist.GetReservationsList().Count == 0) {
                    FindOpenTable(party);
                }
                else {
                    waitlist.AddPartyToWaitlist(party);
                }
            }
            else {
                if(waitlist.GetWalkinsList().Count == 0) {
                    FindOpenTable(party);
                }

                else {
                    waitlist.AddPartyToWaitlist(party);
                }
            }
        }
    }

    public void SeatParty(PartyController party) {
        for(int i = 0; i < party.GetPartySize(); i++) {
            party.transform.GetChild(i).GetComponent<CustomerController>().SetAndMoveToSeat(party.GetAssignedTable(), i);
        }
    }

    public void FindOpenTable(PartyController party) {
        int partySize = party.GetPartySize();
        Debug.Log("Looking for a table");
        for(int n = 0; n < openTables.Count; n++) {
            if(partySize == openTables[n].GetTotalSeats() || partySize == openTables[n].GetTotalSeats() - 1) {
                party.AssignTableToParty(openTables[n]);
                occupiedTables.Add(openTables[n]);
                openTables.Remove(openTables[n]);
                SeatParty(party);
                return;
            }
        }
    }

    IEnumerator SeatFromWaitList(){
        while(isOpenForBusiness) {
            List<PartyController> reservationsList = waitlist.GetReservationsList();
            List<PartyController> walkInsList = waitlist.GetWalkinsList();
            Debug.Log("Checking Waitlist");
            if(reservationsList.Count > 0) {
                for(int i = 0; i < reservationsList.Count; i++) { 
                    FindOpenTable(reservationsList[i]);
                    if(reservationsList[i].GetAssignedTable())
                    {
                        SeatParty(reservationsList[i]);
                        waitlist.RemovePartyFromWaitlist(reservationsList[i]);
                        yield return new WaitForSeconds(waitlistDelay);
                    }
                }
            }
            else if(walkInsList.Count > 0) {
                Debug.Log("Checking Walk-ins List");
                for(int i = 0; i < walkInsList.Count; i++) { 
                    FindOpenTable(walkInsList[i]);
                    if(walkInsList[i].GetAssignedTable())
                    {
                        SeatParty(walkInsList[i]);
                        waitlist.RemovePartyFromWaitlist(walkInsList[i]);
                        yield return new WaitForSeconds(waitlistDelay);
                    }
                }
            }
            else {
                yield return new WaitForSeconds(waitlistDelay / 4f);
            }
            //if no party could be sat, begin delay until next seating
            yield return null;
        }
    }

    public bool GetOpenOrClosed() {
        return isOpenForBusiness;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Customer" && other.transform.parent.GetComponent<PartyController>().GetAssignedTable() == null) {
            other.GetComponent<Pathfinding>().SetTarget(GameObject.FindWithTag("Waiting Area").transform);
            CheckWaitlist(other.transform.parent.GetComponent<PartyController>()); 
        }
    }

    public GameObject GetWaitingArea() {
        return waitingArea;
    }
}
