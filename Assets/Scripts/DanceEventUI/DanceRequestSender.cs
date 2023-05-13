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
			if (collider.gameObject.TryGetComponent(out DanceInteractor danceInteractor))
			{
				switch (danceInteractor.Type)
				{
					case InteractorType.EnvEnemy:
						Debug.Log("Collider object type : " + danceInteractor.Type);
						DanceHandler.ActivateDanceEvent(new DanceRequestContext
						{
							Environment = Environment.EnvDance,
							DesiredMoves = new List<DanceEvent.Pose>()
							{
								DanceEvent.Pose.Cool	
							},
							TargetObject = collider.gameObject
						});
						break;
					case InteractorType.TutorialBridge:
						Debug.Log("Collider object type : " + danceInteractor.Type);
						DanceHandler.ActivateDanceEvent(new DanceRequestContext
						{
							Environment = Environment.EnvDance,
							DesiredMoves = new List<DanceEvent.Pose>()
							{
								DanceEvent.Pose.Splits	
							},
							TargetObject = collider.gameObject
						});
						break;
					default:
						break;
				}
			}
		}
	}
}
