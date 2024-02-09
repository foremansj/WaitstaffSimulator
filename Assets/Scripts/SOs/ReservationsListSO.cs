using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reservations List", menuName = "Reservation List")]
public class ReservationsListSO : ScriptableObject
{
    public float firstSeatingInSeconds;
    public float secondSeatingInSeconds;
    public float thirdSeatingInSeconds;
    public float fourthSeatingInSeconds;
    public float fifthSeatingInSeconds;
    public float sixthSeatingInSeconds;
    public float seventhSeatingInSeconds;
    public float eighthSeatingInSeconds;
}
