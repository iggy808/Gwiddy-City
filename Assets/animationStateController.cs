using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
	Animator animator;
	[SerializeField]
	ScenarioController ScenarioController;

    void Start()
    {
        animator = GetComponent<Animator>(); 
		Debug.Log(animator);
    }

    void Update()
    {
		if (!ScenarioController.IsScenarioActive)
		{
    		if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
				animator.SetBool("isRunning", true);
			else	
				animator.SetBool("isRunning", false);
		}


		if (Input.GetKey("q"))
		{
			int dance = Random.Range(1, 3);
			if (dance == 1)
				animator.SetBool("isDancing1", true);
			else if (dance == 2)
				animator.SetBool("isDancing2", true);
		}
		else
		{
			animator.SetBool("isDancing1", false);
			animator.SetBool("isDancing2", false);
		}

		if (Input.GetKey("e"))
		{
			int dance = Random.Range(1, 3);
			if (dance == 1)
				animator.SetBool("isDancing3", true);
			else if (dance == 2)
				animator.SetBool("isDancing4", true);
		}
		else
		{
			animator.SetBool("isDancing3", false);
			animator.SetBool("isDancing4", false);
		}
    }
}
