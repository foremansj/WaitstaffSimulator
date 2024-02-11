using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    Rigidbody rb;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        rb = GetComponent<Rigidbody>();
        destination = agent.destination; 
    }

    private void Update() {
        if(agent.enabled && target != null) {
            //MoveToTarget(target);
        }
    }

    public Transform GetTarget() {
        return target;
    }

    /*public void MoveToTarget(Transform transform) {
        destination = transform.position;
        agent.SetDestination(destination);
        Debug.Log("Re-pathing");
        new WaitForSeconds(2);
    }*/

    //changing movement operation to coroutine so NPC pathing still works if collided with
    public IEnumerator MoveToTarget() {
        yield return null;
    }

    private void ClearTarget() {
        if(target != null) {
            target = null;
        } 
    }

    public void SetTarget(Transform transform) {
        target = transform;
    }

    public NavMeshAgent GetAgent() {
        return agent;
    }

    public void EnableObstacle() {
        agent.enabled = false;
        obstacle.enabled = true;
    }

    public void EnableAgent() {
        obstacle.enabled = false;
        agent.enabled = true;
    }
}
