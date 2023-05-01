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
			// test triggers to send new dance requests
			switch (collider.gameObject.name)
			{
				case "BattleDanceEventTrigger":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.BattleDance,
						DesiredMove = Pose.Splits,
						TargetUI = "BattleDanceEvent"
					});
					break;
				case "EnvironmentalDanceEventTrigger":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Splits,
						TargetUI = "EnvironmentalDanceEvent"
					});
					break;
				default:
					break;
			}
		}
	}
}
