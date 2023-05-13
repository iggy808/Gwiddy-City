using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialBridgeController : MonoBehaviour
{
	[SerializeField]
	List<GameObject> Platforms;

	public void RaiseBridge()
	{
		foreach (var platform in Platforms)
		{
			Transform platformTransform = platform.GetComponent<Transform>();
			platformTransform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + 4.91f * 4, platformTransform.position.z);
			//StartCoroutine(RaiseFashionably(platformTransform));
		}
	}

	IEnumerator RaiseFashionably(Transform platformTransform)
	{
		yield return new WaitForSeconds(1f);
		platformTransform.position = new Vector3(platformTransform.position.x, platformTransform.position.y + 4.91f * 4, platformTransform.position.z);
	}
}
