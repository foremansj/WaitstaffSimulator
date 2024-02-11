using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    Pathfinding pathfinding;
    int seatNumber;
    bool isSeatedAtTable;

    private void Start() {
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Update() {
        /*if(pathfinding.GetTarget() != null)
        {
            pathfinding.EnableAgent();
            pathfinding.MoveToTarget();
        }*/
    }
    public void SetAndMoveToSeat(TableController table, int seat) {
        seatNumber  = seat;
        pathfinding.SetTarget(table.transform.GetChild(seatNumber));
    }

    public int GetSeatNumber() {
        return seatNumber;
    }

    public void DecideOrder() {

    }

    public void SeatAtTable() {
        isSeatedAtTable = true;
        
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Rigidbody>().isKinematic = true;
        
        pathfinding.GetAgent().speed = 0f;
        transform.position = GetComponent<Pathfinding>().target.position;
        pathfinding.target = null;
        
        pathfinding.EnableObstacle();
    }
}
