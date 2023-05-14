using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialBridgeController : MonoBehaviour
{
	[SerializeField]
	List<GameObject> Platforms;

	float heightOffset = 2f;
	public void RaiseBridge()
	{
		int i = 1;
		foreach (var platform in Platforms)
		{
			Transform platformTransform = platform.GetComponent<Transform>();
			platformTransform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + 4.91f * 4 + (i*heightOffset), platformTransform.position.z);
			//StartCoroutine(RaiseFashionably(platformTransform));
			i++;
		}
	}

	IEnumerator RaiseFashionably(Transform platformTransform)
	{
		yield return new WaitForSeconds(1f);
		platformTransform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + 4.91f * 4, platformTransform.position.z);
	}
}
