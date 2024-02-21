using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public List<CustomerController> customers = null;
    string currentStepOfService;
    bool hasReservation = false;
    public TableController assignedTable = null;
    public float partyMood;
    
    [Header("Steps of Service")]
    public bool isSeated = false;
    public bool wasGreeted = false;
    public bool isReadyToOrder = false;
    public bool hasOrdered = false;
    public bool isEating = false;
    public bool hasFinishedEating = false;
    public bool hasPaid = false;

    [Header("SOS Timers")]
    public float timeEnteredRestaurant; 
    public float timeSeatedAtTable;
    public float timeGreeted;
    public float timeOrderPlaced;
    public float timeFoodDelivered;
    public float timeFinishedEating;
    public float timeCheckPaid;
    public float timeLeftRestaurant;
    [SerializeField] float randomDeliberationDelay;
    [SerializeField] float randomPartyPatienceMultiplier;
    
    private void Awake() {
        
    }

    private void Start() {
        SetPartyCustomers();
        CheckInWithHost();
        timeEnteredRestaurant = FindObjectOfType<GameTimer>().GetRunningTime();
        randomDeliberationDelay = Random.Range(1f, 4f) * customers.Count;
        randomPartyPatienceMultiplier = Random.Range(4f, 7f);
        partyMood = 100f;
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
            return "Waiting for Table";
        }
        else if(!wasGreeted) {
            return "Greeting";
        }
        else if(!hasOrdered) {
            return "Ordering";
        }
        else if(!isEating) {
            return "Waiting for Order";
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

    public float GetDeliberationDelay() {
        return randomDeliberationDelay;
    }

    public float GetPartyPatienceMultiplier() {
        return randomPartyPatienceMultiplier;
    }

    public float GetPartyMood() {
        return partyMood;
    }

    public void AdjustPartyMood(float number) {
        partyMood += number;
    }

    public IEnumerator ReadyToOrder() {
        yield return new WaitForSeconds(randomDeliberationDelay);
        isReadyToOrder = true;
    }
}