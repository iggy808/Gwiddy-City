using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent 
{
    public class DanceEventManager : MonoBehaviour
    {
        public Pose DesiredMove = Pose.Splits;
        GoalPose GoalPose;
        GameObject ArmRightPivot;
        GameObject LegRightPivot;
        GameObject ArmLeftPivot;
        GameObject LegLeftPivot;
        InputController InputController;

        float ErrorMargin = 4f;

        bool ArmRightInPlace;
        bool LegRightInPlace;
        bool ArmLeftInPlace;
        bool LegLeftInPlace;

        bool TimerOn;
        float RemainingTime = 10f;

        // Start is called before the first frame update
        void Start()
        {
            ArmRightPivot = GameObject.Find("ArmRightPivot");
            LegRightPivot = GameObject.Find("LegRightPivot");
            ArmLeftPivot = GameObject.Find("ArmLeftPivot");
            LegLeftPivot = GameObject.Find("LegLeftPivot");
            InputController = GameObject.Find("DanceEventBackground").GetComponent<InputController>();

            switch(DesiredMove)
            {
                case Pose.Splits:
                    GoalPose = new GoalPose(Pose.Splits);
                    GoalPose.DisplayGoals();
                    break;
                default:
                    break;
            }

            Debug.Log("Timer started!! Remaining time: " + RemainingTime);
            TimerOn = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!TimerOn)
            {
                // Quicktime event is over - do whatever then disable the object maybe?
                return;
            }
            
            HandleTimer();
            CheckGoalPositions();
            if (ArmRightInPlace && LegRightInPlace && ArmLeftInPlace && LegLeftInPlace)
            {
                Debug.Log("All limbs in position with " + RemainingTime + " seconds to spare!!");
                InputController.enabled = false;
                TimerOn = false;
            }
        }

        void HandleTimer()
        {
            if (TimerOn)
            {
                if (RemainingTime > 0f)
                {
                    RemainingTime -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("Time is up!!");
                    InputController.enabled = false;
                    RemainingTime = 0;
                    TimerOn = false;
                }
            }
        }

        void CheckGoalPositions()
        {
            float armRightRotation = ArmRightPivot.transform.rotation.eulerAngles.z;
            if (armRightRotation > GoalPose.Splits_ArmRightGoal.eulerAngles.z - ErrorMargin
                && armRightRotation < GoalPose.Splits_ArmRightGoal.eulerAngles.z + ErrorMargin)
            {
                ArmRightInPlace = true;
            }
            else
            {
                ArmRightInPlace = false;
            }

            // need to tweak this goal check for the splits
            float legRightRotation = LegRightPivot.transform.rotation.eulerAngles.z;
            if (legRightRotation > GoalPose.Splits_LegRightGoal.eulerAngles.z - ErrorMargin
                && legRightRotation < GoalPose.Splits_LegRightGoal.eulerAngles.z + ErrorMargin)
            {
                LegRightInPlace = true;
            }
            else
            {
                LegRightInPlace = false;
            }

            float armLeftRotation = ArmLeftPivot.transform.rotation.eulerAngles.z;
            if (armLeftRotation > GoalPose.Splits_ArmLeftGoal.eulerAngles.z - ErrorMargin
                && armLeftRotation < GoalPose.Splits_ArmLeftGoal.eulerAngles.z + ErrorMargin)
            {
                ArmLeftInPlace = true;
            }
            else
            {
                ArmLeftInPlace = false;
            }

            float legLeftRotation = LegLeftPivot.transform.rotation.eulerAngles.z;
            if (legLeftRotation > GoalPose.Splits_LegLeftGoal.eulerAngles.z - ErrorMargin
                && legLeftRotation < GoalPose.Splits_LegLeftGoal.eulerAngles.z + ErrorMargin)
            {
                LegLeftInPlace = true;
            }
            else
            {
                LegLeftInPlace = false;
            }
        }

        public void DisplayUI()
        {
            Debug.Log("Dance event UI displayed");
        }
    }
}