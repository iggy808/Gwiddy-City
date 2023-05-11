using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
	Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
	Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
	{
		animator.SetBool("isRunning", true);
	}
	if (!Input.GetKey("w"))
	{
		animator.SetBool("isRunning", false);
	}

  	if (Input.GetKey("q"))
	{
		animator.SetBool("isDancing1", true);
	}
	if (!Input.GetKey("q"))
	{
		animator.SetBool("isDancing1", false);
	}
if (Input.GetKey("e"))
	{
		animator.SetBool("isDancing2", true);
	}
	if (!Input.GetKey("e"))
	{
		animator.SetBool("isDancing2", false);
	}
if (Input.GetKey("r"))
	{
		animator.SetBool("isDancing3", true);
	}
	if (!Input.GetKey("r"))
	{
		animator.SetBool("isDancing3", false);
	}
if (Input.GetKey("t"))
	{
		animator.SetBool("isDancing4", true);
	}
	if (!Input.GetKey("t"))
	{
		animator.SetBool("isDancing4", false);
	}







    }
}
