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
        public Quaternion Splits_ArmRightGoal;
        public Quaternion Splits_LegRightGoal;
        public Quaternion Splits_ArmLeftGoal;
        public Quaternion Splits_LegLeftGoal;

        GameObject ArmRightGoal;
        GameObject LegRightGoal;
        GameObject ArmLeftGoal;
        GameObject LegLeftGoal;

        Pose CurrentPose;

        public GoalPose(Pose pose)
        {
            CurrentPose = pose;

            ArmRightGoal = GameObject.Find("ArmRightGoal");
            LegRightGoal = GameObject.Find("LegRightGoal");
            ArmLeftGoal = GameObject.Find("ArmLeftGoal");
            LegLeftGoal = GameObject.Find("LegLeftGoal");

            switch (pose)
            {
                case Pose.Splits:
                    Splits_ArmRightGoal = Quaternion.Euler(0f, 0f, 30f);
                    Splits_LegRightGoal = Quaternion.Euler(0f, 0f, 0f);
                    Splits_ArmLeftGoal = Quaternion.Euler(0f, 0f, 210f);
                    Splits_LegLeftGoal = Quaternion.Euler(0f, 0f, 180f);
                    break;
                default:
                    break;
            }
        }

        public void DisplayGoals()
        {
            switch (CurrentPose)
            {
                case Pose.Splits:
                    ArmRightGoal.transform.rotation = Splits_ArmRightGoal;
                    LegRightGoal.transform.rotation = Splits_LegRightGoal;
                    ArmLeftGoal.transform.rotation = Splits_ArmLeftGoal;
                    LegLeftGoal.transform.rotation = Splits_LegLeftGoal;
                    break;
                default:
                    break;
            }

        }
    }
}