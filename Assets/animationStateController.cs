using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
	Animator animator;
	[SerializeField]
	ScenarioController ScenarioController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
		Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
		if (!ScenarioController.IsScenarioActive)
		{
    		if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
			{
				animator.SetBool("isRunning", true);
			}
			else	
			{
				animator.SetBool("isRunning", false);
			}
		}


		if (Input.GetKey("q"))
		{
			animator.SetBool("isDancing1", true);
		}
		else
		{
			animator.SetBool("isDancing1", false);
		}

		if (Input.GetKey("e"))
		{
			animator.SetBool("isDancing2", true);
		}
		else
		{
			animator.SetBool("isDancing2", false);
		}

		if (Input.GetKey("r"))
		{
			animator.SetBool("isDancing3", true);
		}
		else
		{
			animator.SetBool("isDancing3", false);
		}

		if (Input.GetKey("t"))
		{
			animator.SetBool("isDancing4", true);
		}
		else
		{
			animator.SetBool("isDancing4", false);
		}
    }
}
