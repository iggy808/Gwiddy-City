using UnityEngine;
using System.Collections.Generic;
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
						DesiredMoves = new List<Pose>()
						{
							Pose.Splits,
							Pose.Cool
						},
						TargetObject = collider.gameObject
					});
					break;
				case "WalkablockaDanceEventTriggerCool":
					Debug.Log("Cool walkablocka encountered");
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMoves = new List<Pose>
						{
							Pose.Cool
						},
						TargetObject = GameObject.Find("Walkablocka (1)") 
						// Note: can probably grab gameobject from OnTriggerEnter, and store it in the dancerequest sender as TargetObject or something to avoid find
					});
					break;
				case "WalkablockaDanceEventTriggerSplits":
					Debug.Log("Splits walkablocka encountered");
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMoves = new List<Pose>
						{
							Pose.Splits
						},
						TargetObject = GameObject.Find("Walkablocka (2)")
					});
					break;
				case "WalkablockaDanceEventTriggerSplitsAgain":
					DanceHandler.ActivateDanceEvent(new DanceRequestContext
					{
						Environment = Environment.EnvDance,
						DesiredMoves = new List<Pose>
						{
							Pose.Splits
						},
						TargetObject = GameObject.Find("Walkablocka (3)")
					});
					break;
				default:
					break;
			}
		}
	}
}
