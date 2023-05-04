using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceRange : MonoBehaviour
{
	private void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			UnityEngine.AI.NavMeshAgent agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
			agent.enabled = false;
			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			UnityEngine.AI.NavMeshAgent agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
			agent.enabled = true;
		}
	}
}
