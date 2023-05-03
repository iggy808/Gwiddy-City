using UnityEngine;
using BattleEventUI;

namespace DanceEvent
{
	// Should probably only use this dance request sender for environmental dance event requests
	public class DanceRequestSender : MonoBehaviour
	{
		[SerializeField]
		DanceRequestHandler DanceHandler;
		[SerializeField]
		BattleRequestHandler BattleHandler;
		// For now, implement on dance request sender, but need to have a battle request sender ideally

		void OnTriggerEnter(Collider collider)
		{
			Debug.Log("Trigger entered");
			// test triggers to send new dance requests
			switch (collider.gameObject.name)
			{
				case "BattleDanceEventTrigger":
					BattleHandler.ActivateBattleEvent(new BattleRequestContext
					{
						Enemy = SpecialEnemy.CoolDancer
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