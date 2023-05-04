using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    private Transform target;
    private bool tracking;

    private void FixedUpdate()
    {
        if (tracking)
            agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            target = col.GetComponent<Transform>();
            agent.enabled = true;
            agent.SetDestination(target.position);
            tracking = true;
        }
    }
}
