using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialBridgeController : MonoBehaviour
{
	[SerializeField]
	List<GameObject> Bridge1Platforms;
	[SerializeField]
	List<GameObject> Bridge2Platforms;

	float Bridge1HeightOffset = 2f;
	float Bridge2HeightOffset = 1.5f;

	public void RaiseBridge(InteractorType interaction)
	{
		// If function called w respect to first tutorial bridge, raise those platforms
		if (interaction == InteractorType.TutorialBridge1)
		{
			int i = 1;
			foreach (var platform in Bridge1Platforms)
			{
				Transform platformTransform = platform.GetComponent<Transform>();
				platformTransform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + 4.91f * 4 + (i*Bridge1HeightOffset), platformTransform.position.z);
				//StartCoroutine(RaiseFashionably(platformTransform));
				i++;
			}
		}
		// If function called w respect to second tutorial bridge, raise those platforms
		else if (interaction == InteractorType.TutorialBridge2)
		{
			Debug.Log("Moving tutorial bridge 2");
			int i = 1;
			foreach (var platform in Bridge2Platforms)
			{
				/*
				Transform platformTransform = platform.GetComponent<Transform>();
				platformTransform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + 2.85f * 5 + (i*Bridge2HeightOffset), platformTransform.position.z);
				*/
				Debug.Log("Platform name : name = " + platform.name);
				platform.SetActive(true);

				i++;
			}
		}
	}

	IEnumerator RaiseFashionably(Transform platformTransform)
	{
		yield return new WaitForSeconds(1f);
		platformTransform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + 4.91f * 4, platformTransform.position.z);
	}
}
