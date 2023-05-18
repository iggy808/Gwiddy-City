using UnityEngine;
using DanceEvent;

public class PoseBattleInfo
{
	public DanceEvent.Pose Pose;
	public int StaminaCost;
	public int CoolnessGain;
	public int ScoreBoost;
	public string Effects;

	public PoseBattleInfo(DanceEvent.Pose pose)
	{
		Pose = pose;
		switch (pose)
		{
			case DanceEvent.Pose.Splits:
				StaminaCost = 1;
				CoolnessGain = 5;
				ScoreBoost = CoolnessGain;
				break;
			case DanceEvent.Pose.Cool:
				StaminaCost = 2;
				CoolnessGain = 10;
				ScoreBoost = CoolnessGain;
				break;
			case DanceEvent.Pose.Sick:
				StaminaCost = 3;
				CoolnessGain = 15;
				ScoreBoost = CoolnessGain;
				break;
			default:
				// Default to showing the sequencer menu pointer if desired
				
				Effects = "Sequence new moves!";
				break;
		}
	}
}
