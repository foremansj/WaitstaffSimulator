using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    Rigidbody rb;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        destination = agent.destination;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if(target != null) {
            if (Vector3.Distance(destination, target.position) > 1.0f) {
            destination = target.position;
            agent.destination = destination;
            }
            
            else {
            agent.speed = 0f;
            this.transform.position = target.position;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            target = null;
            }
        }
    }

    public void SetTarget(Transform transform) {
        target = transform;
    }
}
