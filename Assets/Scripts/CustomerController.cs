using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    Pathfinding pathfinding;
    int seatNumber;


    private void Start() {
        pathfinding = GetComponent<Pathfinding>();
    }
    public void DecideOrder() {

    }

    public void SetAndMoveToSeat(TableController table, int seat) {
        seatNumber  = seat;
        pathfinding.SetTarget(table.transform.GetChild(seatNumber));
    }
}
