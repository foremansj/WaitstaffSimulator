using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkinHandler : MonoBehaviour
{
    [SerializeField] float walkinSpawnDelay = 30f;
    PartySpawner partySpawner;
    HostStand hostStand;
    private void Awake() {
        hostStand = FindObjectOfType<HostStand>();
        partySpawner = FindObjectOfType<PartySpawner>();
    }

    private void Start() {
        StartCoroutine(SpawnNewWalkinParty());
    }

    IEnumerator SpawnNewWalkinParty() {
        while (hostStand.GetOpenOrClosed()) {
            partySpawner.SpawnNewParty(false);
            yield return new WaitForSeconds(walkinSpawnDelay);
        }
    }
}