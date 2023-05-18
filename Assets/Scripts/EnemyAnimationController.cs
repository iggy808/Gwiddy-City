using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
	public UnityEngine.AI.NavMeshAgent agent;
	public PlayerMovement playerCon;
	public DanceRange danceRng;
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (agent.enabled)
			animator.SetBool("isRunning", true);
		else
			animator.SetBool("isRunning", false);

		if (!agent.enabled && playerCon.engaged && danceRng.dancing)
        {
			int dance = Random.Range(1, 5);
			if (dance == 1)
				animator.SetBool("isDancing1", true);
			else if (dance == 2)
				animator.SetBool("isDancing2", true);
			else if (dance == 3)
				animator.SetBool("isDancing3", true);
			else if (dance == 4)
				animator.SetBool("isDancing4", true);
        }
		else
        {
			animator.SetBool("isDancing1", false);
			animator.SetBool("isDancing2", false);
			animator.SetBool("isDancing3", false);
			animator.SetBool("isDancing4", false);
		}
	}
}