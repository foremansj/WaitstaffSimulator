using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public List<CustomerController> customers = null;
    string currentStepOfService;
    bool hasReservation = false;
    public TableController assignedTable = null;
    //Waitlist waitlist;
    
    private void Awake() {
        //waitlist = FindObjectOfType<Waitlist>();
    }

    private void Start() {
        //waitlist.AddPartyToWaitlist(this);
        SetPartyCustomers();
        CheckInWithHost();
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

    private void SetPartyCustomers() {
        if(customers == null) {
            customers = new List<CustomerController>();
            for(int i = 0; i < this.transform.childCount; i++) {
                if(transform.GetChild(i).GetComponent<CustomerController>() != null) {
                    customers.Add(this.transform.GetChild(i).GetComponent<CustomerController>());
                }
            }
        }
    }

    private void CheckInWithHost() {
        customers[0].GetComponent<Pathfinding>().SetTarget(GameObject.FindWithTag("Host").transform);
        for(int i = 1; i < customers.Count; i++) {
            customers[i].GetComponent<Pathfinding>().SetTarget(GameObject.FindWithTag("Waiting Area").transform);
        }
    }
}
