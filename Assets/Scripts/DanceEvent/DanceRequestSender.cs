using UnityEngine;

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
				case "BattleDanceEventTrigger":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.BattleDance,
						DesiredMove = Pose.Splits,
						TargetUI = "BattleDanceEvent",
						TargetObject = null
					});
					break;
				case "EnvironmentalDanceEventTrigger":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Splits,
						TargetUI = "EnvironmentalDanceEvent",
						TargetObject = collider.gameObject
					});
					break;
				case "WalkablockaDanceEventTrigger":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Splits,
						TargetUI = "EnvironmentalDanceEvent",
						TargetObject = GameObject.Find("Walkablocka")
					});
					break;
				default:
					break;
			}
		}
	}
}
