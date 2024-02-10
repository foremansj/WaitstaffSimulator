using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySpawner : MonoBehaviour
{
    [Header("Customer Spawning")]
    [SerializeField] GameObject customerPrefab;
    [SerializeField] Vector3 spawnPointOrigin;
    
    public void SpawnNewParty(bool reservationStatus)
    {
        int partySize = Random.Range(2,7);
        GameObject newParty = new GameObject("Party of " + partySize);
        newParty.AddComponent<PartyController>();
        newParty.GetComponent<PartyController>().SetReservationStatus(reservationStatus);
        for(int i = 0; i < partySize; i++)
        {
            Vector3 spawnPoint = new Vector3(spawnPointOrigin.x + i, spawnPointOrigin.y, spawnPointOrigin.z + i);
            GameObject newCustomer = Instantiate(customerPrefab, spawnPoint, Quaternion.identity);
            newCustomer.transform.SetParent(newParty.transform);  
        }
    }
}
