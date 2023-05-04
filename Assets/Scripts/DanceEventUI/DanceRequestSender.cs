using UnityEngine;
using BattleEvent;

namespace DanceEvent
{
	public class DanceRequestSender : MonoBehaviour
	{
		[SerializeField]
		DanceRequestHandler DanceHandler;

		void OnTriggerEnter(Collider collider)
		{
			Debug.Log("Trigger entered");
			// test triggers to send new dance requests
			switch (collider.gameObject.name)
			{
				case "EnvironmentalDanceEventTrigger":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Splits,
						TargetObject = collider.gameObject
					});
					break;
				case "WalkablockaDanceEventTrigger":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Cool,
						TargetObject = GameObject.Find("Walkablocka")
					});
					break;
				default:
					break;
			}
		}
	}
}
