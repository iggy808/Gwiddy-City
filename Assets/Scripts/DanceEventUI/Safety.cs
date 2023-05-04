using UnityEngine;

public class Safety : MonoBehaviour
{
	Vector3 PlayerStartingPosition;
	
	void Start()
	{
		PlayerStartingPosition = GetComponent<Transform>().position;
	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "SAFETY")
		{
			gameObject.transform.position = PlayerStartingPosition;
		}
	}
}
