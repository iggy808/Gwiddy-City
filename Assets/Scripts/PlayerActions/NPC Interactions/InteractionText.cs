using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    public GameObject interactionTextPrefab;
    public Transform TextPosition;

    public GameObject interactionTextObject;
	bool IsTextInstantiated;

	void Awake()
	{

		IsTextInstantiated = false;
        //interactionTextObject = Instantiate(interactionTextPrefab, TextPosition.position, Quaternion.Euler(0, -90, 0), transform) as GameObject;
		interactionTextObject.SetActive(false);
	}
    // Start is called before the first frame update
    void Start()
    {
    }
    


    // COLIN: Displays Text when inflated trigger collider collides with the Player 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !IsTextInstantiated)
        {
			IsTextInstantiated = true;
			interactionTextObject.SetActive(true);
			Debug.Log("Text activated.");
        }

    }

	void OnTriggerExit(Collider other)
	{
		Debug.Log("OnTriggerExit called.");
		if (other.tag == "Player" && IsTextInstantiated)
		{
			interactionTextObject.SetActive(false);
			IsTextInstantiated = false;
			Debug.Log("Text disabled.");
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
