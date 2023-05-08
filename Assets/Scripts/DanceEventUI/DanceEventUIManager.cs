using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DanceEvent
{
	public class DanceEventUIManager : MonoBehaviour
	{
		[SerializeField]
		GameObject ArmRightGoal;
		[SerializeField]
		GameObject LegRightGoal;
		[SerializeField]
		GameObject ArmLeftGoal;
		[SerializeField]
		GameObject LegLeftGoal;

		public void HighlightGoalLimb(Limb goalLimb)
		{
			// Find current goal arm gameobject
			switch (goalLimb)
			{
				case Limb.ArmRight:
					// Highlight armright goal here - maybe particle effect?
					// ArmRightGoal.ParticlesOn
					
					break;	
				case Limb.LegRight:
					break;
				case Limb.ArmLeft:
					break;
				case Limb.LegLeft:
					break;
			}
			
		}
	}
}
