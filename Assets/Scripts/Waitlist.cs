using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waitlist : MonoBehaviour
{
    public List<PartyController> walkinsWaiting;
    List<PartyController> reservationsWaiting;

    private void Awake() {
        walkinsWaiting = new List<PartyController>();
        reservationsWaiting = new List<PartyController>();
    }

    public void AddPartyToWaitlist(PartyController party) {
        if(party.GetReservationStatus() == true) {
            reservationsWaiting.Add(party);
        }
        else {
            walkinsWaiting.Add(party);
        }
    }

    public void RemovePartyFromWaitlist(PartyController party) {
        if(reservationsWaiting != null && party.GetReservationStatus() == true) {
            reservationsWaiting.Remove(party);
        }
        else if (walkinsWaiting != null) {
            walkinsWaiting.Remove(party);
        }
    }

    public List<PartyController> GetReservationsList() {
        return reservationsWaiting;
    }

    public List<PartyController> GetWalkinsList() {
        return walkinsWaiting;
    }
}
