using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TableController : MonoBehaviour
{
    int seats;
    bool hasParty;
    bool hasPlates;
    public PartyController party;
    [SerializeField] int tableNumber;
    
    private void Start() {
        SetNumberOfSeats();
    }

    private void SetNumberOfSeats(){
        seats = this.transform.childCount;
    }

    public int GetTotalSeats() {
        return seats;
    }

    public bool GetHasParty() {
        return hasParty;
    }

    public bool GetHasPlates() {
        return hasPlates;
    }

    public PartyController GetAssignedParty() {
        return party;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Customer" && other.transform.parent.GetComponent<PartyController>().GetAssignedTable() == this && 
                    other.GetComponent<Pathfinding>().target != null) {
            other.GetComponent<CustomerController>().SeatAtTable();
            //other.transform.parent.GetComponent<PartyController>().isSeated = true;
        }
    }
}