using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    public class GoalPose : MonoBehaviour
    {	
		// Dance event manager uses these goal rotations to determine whether or not the
		// player has accurately placed a limb
        public Quaternion ArmRightGoalRotation;
        public Quaternion LegRightGoalRotation;
        public Quaternion ArmLeftGoalRotation;
        public Quaternion LegLeftGoalRotation;
        public Pose CurrentPose;
		
		// References to the game objects that display the goals on the UI
		[SerializeField]
        GameObject ArmRightGoal;
		[SerializeField]
        GameObject LegRightGoal;
		[SerializeField]
        GameObject ArmLeftGoal;
		[SerializeField]
        GameObject LegLeftGoal;

		public void SetGoalRotations(Pose goalPose)
		{ 
            switch (goalPose)
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
