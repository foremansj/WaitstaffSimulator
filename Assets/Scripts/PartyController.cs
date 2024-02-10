using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    List<CustomerController> customers;
    string currentStepOfService;
    bool hasReservation = false;
    public TableController assignedTable = null;
    Waitlist waitlist;
    private void Awake() {
        waitlist = FindObjectOfType<Waitlist>();
    }

    private void Start() {
        waitlist.AddPartyToWaitlist(this);
    }
    public void SetReservationStatus(bool reservationStatus) {
        hasReservation = reservationStatus;
    }
    public bool GetReservationStatus() {
        return hasReservation;
    }

    public int GetPartySize() {
        return this.transform.childCount;
    }

    public string GetCurrentStepOfService() {
        return currentStepOfService;
    }

    public void ChangeCurrentStepOfService(string newStepOfService) {
        currentStepOfService = newStepOfService;
    }

    public void AssignTableToParty(TableController table) {
        assignedTable = table;
    }

    public TableController GetAssignedTable() {
        return assignedTable;
    }

    public List<CustomerController> GetCustomers() {
        return customers; 
    }
}
