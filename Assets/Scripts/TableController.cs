using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    int seats;
    bool hasParty;
    

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
}
