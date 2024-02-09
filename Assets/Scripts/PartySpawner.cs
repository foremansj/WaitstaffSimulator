using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySpawner : MonoBehaviour
{
    [Header("Customer Spawning")]
    [SerializeField] GameObject customerPrefab;
    public Vector3 spawnPointOrigin;
    public GameObject exitPoint;
    public float customerSpawnDelay;
    Coroutine createParty;

    [SerializeField] bool isOpenForBusiness = true;
    //Waitlist waitlist;
    //ReservationsList reservationsList;
    //UIController uIController;

    private void Awake() {
        //uIController = FindObjectOfType<UIController>();
    }
    IEnumerator CreateNewParty()
    {
        do
        {
            startOver:
                int partySize = Random.Range(2,7);
                GameObject newParty = new GameObject("Party of " + partySize);
                //newParty.AddComponent<PartyController>();
                for(int i = 0; i < partySize; i++)
                {
                    Vector3 spawnPoint = new Vector3(spawnPointOrigin.x + i, spawnPointOrigin.y, spawnPointOrigin.z + i);
                    GameObject newCustomer = Instantiate(customerPrefab, spawnPoint, Quaternion.identity);
                    newCustomer.transform.SetParent(newParty.transform);  
                }
                
                yield return new WaitForSeconds(customerSpawnDelay);
                goto startOver;
        } while(isOpenForBusiness);
    }
    /*void SetCustomerSpawnSpeed()
    {
        switch(uIController.GetWorldTime())
        {
            case float n when (n <= 6):
                customerSpawnDelay = 40f;
                //isOpenForBusiness = true;
                break;

            case float n when (n > 6 && n <= 7):
                customerSpawnDelay = 50f;
                //isOpenForBusiness = true;
                break;
            
            case float n when (n > 7 && n <= 8):
                customerSpawnDelay = 30f;
                //isOpenForBusiness = true;
                break;
            
            case float n when (n > 8 && n <= 8.5):
                customerSpawnDelay = 40f;
                //isOpenForBusiness = true;
                break;
            
            case float n when (n > 8.5 && n <= 9):
                customerSpawnDelay = 50f;
                //isOpenForBusiness = false;
                break;
            
            case float n when (n > 9 && n <= 10):
                customerSpawnDelay = 70f;
                //isOpenForBusiness = false;
                break;
            case float n when (n > 10 && n <= 10.5):
                customerSpawnDelay = 90f;
                //isOpenForBusiness = false;
                break;

            default:
                StopCoroutine(createParty);
                customerSpawnDelay = 100000f;
                break;
        }
    }*/








    public bool GetIsOpenForBusiness()
    {
        return isOpenForBusiness;
    }
}
