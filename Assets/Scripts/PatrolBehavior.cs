using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public DanceRange danceRng;
    public EnemyAnimationController animControl;
    private Transform target;
    private bool tracking;

    private void FixedUpdate()
    {
        if (tracking && !danceRng.dancing && !animControl.fought)
            agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !tracking)
        {
            target = col.GetComponent<Transform>();
            agent.enabled = true;
            agent.SetDestination(target.position);
            tracking = true;
        }
    }
}
