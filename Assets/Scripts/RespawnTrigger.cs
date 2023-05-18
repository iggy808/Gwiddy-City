using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
	public Transform respawnPoint;
	public DangerBlock DangerBlock;
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{	
			DangerBlock.respawnPoint = respawnPoint;
		}
	}
}
