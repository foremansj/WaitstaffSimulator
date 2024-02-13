using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public List<CustomerController> customers = null;
    string currentStepOfService;
    bool hasReservation = false;
    public TableController assignedTable = null;
    public float partyMoodAggregate;
    
    [Header("Steps of Service")]
    public bool isSeated;
    public bool wasGreeted;
    public bool hasOrdered;
    public bool hasFinishedEating;
    public bool hasPaid;

    [Header("SOS Timers")]
    public float timeEnteredRestaurant; 
    public float timeSeatedAtTable;
    public float timeGreeted;
    public float timeOrderPlaced;
    public float timeFoodDelivered;
    public float timeFinishedEating;
    public float timeCheckPaid;
    public float timeLeftRestaurant;
    
    private void Awake() {
        
    }

    private void Start() {
        SetPartyCustomers();
        CheckInWithHost();
        timeEnteredRestaurant = FindObjectOfType<GameTimer>().GetRunningTime();
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
        if(!isSeated) {
            return "Finding Table";
        }
        else if(!wasGreeted) {
            return "Greeting";
        }
        else if(!hasOrdered) {
            return "Ordering";
        }
        else if(!hasFinishedEating) {
            return "Eating";
        }
        else if(!hasPaid) {
            return "Paying";
        }
        else {
            return "Finished";
        }
    }

    public void AssignTableToParty(TableController table) {
        assignedTable = table;
        table.party = this;
    }

    public TableController GetAssignedTable() {
        return assignedTable;
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

    public List<CustomerController> GetCustomers() {
        return customers; 
    }

    private void CheckInWithHost() {
        customers[0].GetComponent<Pathfinding>().SetTarget(GameObject.FindWithTag("Host").transform);
        for(int i = 1; i < customers.Count; i++) {
            customers[i].GetComponent<Pathfinding>().SetTarget(GameObject.FindWithTag("Waiting Area").transform);
        }
    }

    public void CalculatePartyMood() {
        partyMoodAggregate = 0;
        for(int i = 0; i < customers.Count; i++) {
            partyMoodAggregate += customers[i].GetCustomerMood();
        }
        partyMoodAggregate = partyMoodAggregate / customers.Count;
    }

    public float GetPartyMood() {
        return partyMoodAggregate;
    }
}