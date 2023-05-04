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
			//Debug.Log("Trigger entered");
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
				case "WalkablockaDanceEventTriggerCool":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Cool,
						TargetObject = GameObject.Find("Walkablocka (1)") 
						// Note: can probably grab gameobject from OnTriggerEnter, and store it in the dancerequest sender as TargetObject or something to avoid find
					});
					break;
				case "WalkablockaDanceEventTriggerSplits":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Splits,
						TargetObject = GameObject.Find("Walkablocka (2)")
					});
					break;
				case "WalkablockaDanceEventTriggerSplitsAgain":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMove = Pose.Splits,
						TargetObject = GameObject.Find("Walkablocka (3)")
					});
					break;
				default:
					break;
			}
		}
	}
}
