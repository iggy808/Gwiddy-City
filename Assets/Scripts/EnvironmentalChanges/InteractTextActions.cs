using UnityEngine;


public class InteractTextActions : MonoBehaviour
{
	public Transform PlayerTransform;
	public GameObject InteractionSubject;
	public Transform TextTransform;

	void Update()
	{
		TextTransform = GetComponent<Transform>();
		TextTransform.position = new Vector3(InteractionSubject.transform.position.x, InteractionSubject.transform.position.y+1.5f, InteractionSubject.transform.position.z);
		TextTransform.LookAt(PlayerTransform);


        if (Input.GetKeyDown(KeyCode.F))
        {
			Debug.Log("Player triggered interaction on environmental interaction text.");
            
		}
		// To restrict axis rotation to y-axis: 
		// UITransform.LookAt(new Vector3(PlayerTransform.position.x, gameObject.transform.y, PlayerTransform.position.z))
		// or
		// UITransform.LookAt(new Vector3(PlayerTransform.position.x, UITransform.transform.y, PlayerTransform.position.z))
	}
}

