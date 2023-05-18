using UnityEngine;
using DanceEvent;

public class DanceModifier
{
	public int CoolnessModifier;
	public int StaminaReplenish;
	public int EnemyEmbarassment;
	public int TurnDuration;
	
	public DanceModifier(DanceEvent.Pose pose)
	{
		switch (pose)
		{
			case DanceEvent.Pose.Splits:
				CoolnessModifier = 0;
				StaminaReplenish = 0;	
				EnemyEmbarassment = 0;
				break;
			case DanceEvent.Pose.Cool:
				CoolnessModifier = 2;
				StaminaReplenish = 0;
				EnemyEmbarassment = 0;
				break;
			case DanceEvent.Pose.Sick:
				CoolnessModifier = 0;
				StaminaReplenish = 0;
				EnemyEmbarassment = 10;
				break;
		}
	}
}
