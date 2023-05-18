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
			int dance = Random.Range(1, 35);
			if (dance == 1)
				animator.SetBool("isDancing1", true);
			else if (dance == 2)
				animator.SetBool("isDancing2", true);
			else if (dance == 3)
				animator.SetBool("isDancing3", true);
			else if (dance == 4)
				animator.SetBool("isDancing4", true);
			else if (dance == 5)
				animator.SetBool("isDancing5", true);
			else if (dance == 6)
				animator.SetBool("isDancing6", true);
			else if (dance == 7)
				animator.SetBool("isDancing7", true);
			else if (dance == 8)
				animator.SetBool("isDancing8", true);
			else if (dance == 9)
				animator.SetBool("isDancing9", true);
			else if (dance == 10)
				animator.SetBool("isDancing10", true);
			else if (dance == 11)
				animator.SetBool("isDancing11", true);
			else if (dance == 12)
				animator.SetBool("isDancing12", true);
			else if (dance == 13)
				animator.SetBool("isDancing13", true);
			else if (dance == 14)
				animator.SetBool("isDancing14", true);
			else if (dance == 15)
				animator.SetBool("isDancing15", true);
			else if (dance == 16)
				animator.SetBool("isDancing16", true);
			else if (dance == 17)
				animator.SetBool("isDancing17", true);
			else if (dance == 18)
				animator.SetBool("isDancing18", true);
			else if (dance == 19)
				animator.SetBool("isDancing19", true);
			else if (dance == 20)
				animator.SetBool("isDancing20", true);
			else if (dance == 21)
				animator.SetBool("isDancing21", true);
			else if (dance == 22)
				animator.SetBool("isDancing22", true);
			else if (dance == 23)
				animator.SetBool("isDancing23", true);
			else if (dance == 24)
				animator.SetBool("isDancing24", true);
			else if (dance == 25)
				animator.SetBool("isDancing25", true);
			else if (dance == 26)
				animator.SetBool("isDancing26", true);
			else if (dance == 27)
				animator.SetBool("isDancing27", true);
			else if (dance == 28)
				animator.SetBool("isDancing28", true);
			else if (dance == 29)
				animator.SetBool("isDancing29", true);
			else if (dance == 30)
				animator.SetBool("isDancing30", true);
			else if (dance == 31)
				animator.SetBool("isDancing31", true);
			else if (dance == 32)
				animator.SetBool("isDancing32", true);
			else if (dance == 33)
				animator.SetBool("isDancing33", true);
			else if (dance == 34)
				animator.SetBool("isDancing34", true);
		}
		else
		{
			animator.SetBool("isDancing1", false);
			animator.SetBool("isDancing2", false);
			animator.SetBool("isDancing3", false);
			animator.SetBool("isDancing4", false);
			animator.SetBool("isDancing5", false);
			animator.SetBool("isDancing6", false);
			animator.SetBool("isDancing7", false);
			animator.SetBool("isDancing8", false);
			animator.SetBool("isDancing9", false);
			animator.SetBool("isDancing10", false);
			animator.SetBool("isDancing11", false);
			animator.SetBool("isDancing12", false);
			animator.SetBool("isDancing13", false);
			animator.SetBool("isDancing14", false);
			animator.SetBool("isDancing15", false);
			animator.SetBool("isDancing16", false);
			animator.SetBool("isDancing17", false);
			animator.SetBool("isDancing18", false);
			animator.SetBool("isDancing19", false);
			animator.SetBool("isDancing20", false);
			animator.SetBool("isDancing21", false);
			animator.SetBool("isDancing22", false);
			animator.SetBool("isDancing23", false);
			animator.SetBool("isDancing24", false);
			animator.SetBool("isDancing25", false);
			animator.SetBool("isDancing26", false);
			animator.SetBool("isDancing27", false);
			animator.SetBool("isDancing28", false);
			animator.SetBool("isDancing29", false);
			animator.SetBool("isDancing30", false);
			animator.SetBool("isDancing31", false);
			animator.SetBool("isDancing32", false);
			animator.SetBool("isDancing33", false);
			animator.SetBool("isDancing34", false);
		}

		if (Input.GetKey("e"))
		{
			int dance = Random.Range(1, 34);
			if (dance == 1)
				animator.SetBool("isDancing35", true);
			else if (dance == 2)
				animator.SetBool("isDancing36", true);
			else if (dance == 3)
				animator.SetBool("isDancing37", true);
			else if (dance == 4)
				animator.SetBool("isDancing38", true);
			else if (dance == 5)
				animator.SetBool("isDancing39", true);
			else if (dance == 6)
				animator.SetBool("isDancing40", true);
			else if (dance == 7)
				animator.SetBool("isDancing41", true);
			else if (dance == 8)
				animator.SetBool("isDancing42", true);
			else if (dance == 9)
				animator.SetBool("isDancing43", true);
			else if (dance == 10)
				animator.SetBool("isDancing44", true);
			else if (dance == 11)
				animator.SetBool("isDancing45", true);
			else if (dance == 12)
				animator.SetBool("isDancing46", true);
			else if (dance == 13)
				animator.SetBool("isDancing47", true);
			else if (dance == 14)
				animator.SetBool("isDancing48", true);
			else if (dance == 15)
				animator.SetBool("isDancing49", true);
			else if (dance == 16)
				animator.SetBool("isDancing50", true);
			else if (dance == 17)
				animator.SetBool("isDancing51", true);
			else if (dance == 18)
				animator.SetBool("isDancing52", true);
			else if (dance == 19)
				animator.SetBool("isDancing53", true);
			else if (dance == 20)
				animator.SetBool("isDancing54", true);
			else if (dance == 21)
				animator.SetBool("isDancing55", true);
			else if (dance == 22)
				animator.SetBool("isDancing56", true);
			else if (dance == 23)
				animator.SetBool("isDancing57", true);
			else if (dance == 24)
				animator.SetBool("isDancing58", true);
			else if (dance == 25)
				animator.SetBool("isDancing59", true);
			else if (dance == 26)
				animator.SetBool("isDancing60", true);
			else if (dance == 27)
				animator.SetBool("isDancing61", true);
			else if (dance == 28)
				animator.SetBool("isDancing62", true);
			else if (dance == 29)
				animator.SetBool("isDancing63", true);
			else if (dance == 30)
				animator.SetBool("isDancing64", true);
			else if (dance == 31)
				animator.SetBool("isDancing65", true);
			else if (dance == 32)
				animator.SetBool("isDancing66", true);
			else if (dance == 33)
				animator.SetBool("isDancing67", true);
		}
		else
		{
			animator.SetBool("isDancing35", false);
			animator.SetBool("isDancing36", false);
			animator.SetBool("isDancing37", false);
			animator.SetBool("isDancing38", false);
			animator.SetBool("isDancing39", false);
			animator.SetBool("isDancing40", false);
			animator.SetBool("isDancing41", false);
			animator.SetBool("isDancing42", false);
			animator.SetBool("isDancing43", false);
			animator.SetBool("isDancing44", false);
			animator.SetBool("isDancing45", false);
			animator.SetBool("isDancing46", false);
			animator.SetBool("isDancing47", false);
			animator.SetBool("isDancing48", false);
			animator.SetBool("isDancing49", false);
			animator.SetBool("isDancing50", false);
			animator.SetBool("isDancing51", false);
			animator.SetBool("isDancing52", false);
			animator.SetBool("isDancing53", false);
			animator.SetBool("isDancing54", false);
			animator.SetBool("isDancing55", false);
			animator.SetBool("isDancing56", false);
			animator.SetBool("isDancing57", false);
			animator.SetBool("isDancing58", false);
			animator.SetBool("isDancing59", false);
			animator.SetBool("isDancing60", false);
			animator.SetBool("isDancing61", false);
			animator.SetBool("isDancing62", false);
			animator.SetBool("isDancing63", false);
			animator.SetBool("isDancing64", false);
			animator.SetBool("isDancing65", false);
			animator.SetBool("isDancing66", false);
			animator.SetBool("isDancing67", false);
		}
    }
}
