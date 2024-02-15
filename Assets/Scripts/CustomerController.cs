using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    Pathfinding pathfinding;
    PartyController partyController;
    MenuController menuController;
    public MenuItemSO appetizerOrder;
    
    public int seatNumber;
    public bool isSeatedAtTable;
    public bool hasOrdered;
    public bool hasFinishedEating;

    private void Awake() {
        menuController = FindObjectOfType<MenuController>();
    }
    private void Start() {
        pathfinding = GetComponent<Pathfinding>();
        partyController = GetComponentInParent<PartyController>();
    }

    private void Update() {
        
    }

    public void SetAndMoveToSeat(TableController table, int seat) {
        seatNumber  = seat;
        pathfinding.SetTarget(table.transform.GetChild(seatNumber));
    }

    public int GetSeatNumber() {
        return seatNumber;
    }

    public void DecideAppetizer() {
        appetizerOrder = menuController.GetRandomAppetizer();
    }

    public void SeatAtTable() {
        isSeatedAtTable = true;
        
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
        pathfinding.GetAgent().speed = 0f;
        transform.position = GetComponent<Pathfinding>().target.position;
        pathfinding.target = null;
        
        pathfinding.EnableObstacle();
        for(int i = 0; i <partyController.customers.Count; i++) {
            if(!partyController.customers[i].isSeatedAtTable)
            {
                partyController.isSeated = false;
                return;
            }
            else {
                partyController.isSeated = true;
                partyController.timeSeatedAtTable = FindObjectOfType<GameTimer>().GetRunningTime();
            }
        }
    }
}