using UnityEngine;

namespace DanceEvent
{
	public class DanceRequestSender : MonoBehaviour
	{
		DanceRequestHandler DanceHandler;

		void Awake()
		{
			DanceHandler = GameObject.Find("BattleDanceEventUI").GetComponent<DanceRequestHandler>();
		}

		void OnTriggerEnter(Collider collider)
		{
			Debug.Log("Trigger entered");
			DanceHandler.ActivateDanceEvent(new DanceRequestContext
			{
				Environment = Environment.BattleDance,
				DesiredMove = Pose.Splits,
				TargetUI = "BattleDanceEvent"
			});
		}
	}
}
