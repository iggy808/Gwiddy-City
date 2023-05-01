using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    public enum Pose
    {
        Splits
    }

    public class GoalPose
    {
        public Quaternion ArmRightGoalRotation;
        public Quaternion LegRightGoalRotation;
        public Quaternion ArmLeftGoalRotation;
        public Quaternion LegLeftGoalRotation;

        GameObject ArmRightGoal;
        GameObject LegRightGoal;
        GameObject ArmLeftGoal;
        GameObject LegLeftGoal;

        Pose CurrentPose;

        public GoalPose(DanceRequestContext context)
        {
            CurrentPose = context.DesiredMove;
			switch (context.Environment)
			{
				case Environment.BattleDance:
            		ArmRightGoal = GameObject.Find("ArmRightGoalB");
            		LegRightGoal = GameObject.Find("LegRightGoalB");
            		ArmLeftGoal = GameObject.Find("ArmLeftGoalB");
            		LegLeftGoal = GameObject.Find("LegLeftGoalB");
					break;
				case Environment.EnvDance:
            		ArmRightGoal = GameObject.Find("ArmRightGoalE");
            		LegRightGoal = GameObject.Find("LegRightGoalE");
            		ArmLeftGoal = GameObject.Find("ArmLeftGoalE");
            		LegLeftGoal = GameObject.Find("LegLeftGoalE");
					break;
				default:
					break;
			}
            SetGoalRotations();
        }

	void SetGoalRotations()
	{ 
            switch (CurrentPose)
            {
                case Pose.Splits:
                    ArmRightGoalRotation = Quaternion.Euler(0f, 0f, 30f);   // ArmRightGoal for splits
                    LegRightGoalRotation = Quaternion.Euler(0f, 0f, 359f);  // LegRightGoal for splits
                    ArmLeftGoalRotation = Quaternion.Euler(0f, 0f, 210f);   // ArmLeftGoal for splits
                    LegLeftGoalRotation = Quaternion.Euler(0f, 0f, 180f);   // LegLeftGoal for splits
                    break;
                default:
                    break;
            }
	}

        public void DisplayGoalRotations()
        {
            ArmRightGoal.transform.rotation = ArmRightGoalRotation;
            LegRightGoal.transform.rotation = LegRightGoalRotation;
            ArmLeftGoal.transform.rotation = ArmLeftGoalRotation;
            LegLeftGoal.transform.rotation = LegLeftGoalRotation;
        }
    }
}
