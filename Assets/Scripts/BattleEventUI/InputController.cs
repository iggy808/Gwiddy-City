using UnityEngine;
using DanceEvent;

namespace BattleEvent
{
	public class InputController : MonoBehaviour
	{
		[SerializeField]
		DanceRequestHandler DanceHandler;

		// Will need to find a way to accept poses as input from the panel
		//
		// Also need to figure out how to change from using the cursor for 
		// looking around to using the cursor for clicking menu buttons,
		// because this does not work currently
		public void DanceAttackClicked()
		{
			DanceHandler.ActivateDanceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMove = DanceEvent.Pose.Splits,
				TargetObject = null
			});	
		}
	}
}
