using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent 
{
    // need to work on making the dance events configurable to environment/desired move/moves
    // potentially should be able to hand a list of moves to do in sequence?
    public class DanceEventManager : MonoBehaviour
    {
        public Pose DesiredMove = Pose.Splits;
        GoalPose GoalPose;
        GameObject ArmRightPivot;
        GameObject LegRightPivot;
        GameObject ArmLeftPivot;
        GameObject LegLeftPivot;
        public InputController InputController;
        DanceRequestHandler DanceRequestHandler;
        Limb CurrentLimb;

		DanceRequestContext Context;

        float ErrorMargin = 4f;

        // tweak these depending on desired move accordingly
        bool ArmRightInPlace = false;
        bool LegRightInPlace = false;
        bool ArmLeftInPlace = false;
        bool LegLeftInPlace = false;

        bool TimerOn;
        bool NoDice;
        float RemainingTime = 3f;

		void Awake()
		{
            ArmRightPivot = GameObject.Find("ArmRightPivot");
            LegRightPivot = GameObject.Find("LegRightPivot"); 
            ArmLeftPivot = GameObject.Find("ArmLeftPivot");
            LegLeftPivot = GameObject.Find("LegLeftPivot");	
			// should handle these on fn call at run time
            DanceRequestHandler = GameObject.Find("BattleDanceEventUI").GetComponent<DanceRequestHandler>();
		}
        
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Timer started!! Remaining time: " + RemainingTime);
            TimerOn = true;
			NoDice = false;
        }

		public void ConfigureDanceEventInternal(DanceRequestContext context)
		{
			Context = context;

			switch (Context.Environment)
			{
				case Environment.BattleDance:
            		InputController = GameObject.Find("BattleDanceEvent").GetComponent<InputController>();
					break;
				case Environment.EnvDance:
            		InputController = GameObject.Find("EnvironmentalDanceEvent").GetComponent<InputController>();
					break;
				default:
					break;
			}
			
            switch(Context.DesiredMove)
            {
                case Pose.Splits:
                    GoalPose = new GoalPose(Pose.Splits);
                    break;
                default:
					GoalPose = new GoalPose(Pose.Splits);
                    break;
            }			

			InitializeLimbPosition();
            GoalPose.DisplayGoalRotations(); 
		}

		void InitializeLimbPosition()
		{
			ArmRightPivot.transform.rotation = Quaternion.Euler(0f,0f,0f);
			LegRightPivot.transform.rotation = Quaternion.Euler(0f,0f,270f);
			ArmLeftPivot.transform.rotation = Quaternion.Euler(0f,0f,180f);
			LegLeftPivot.transform.rotation = Quaternion.Euler(0f,0f,270f);
		}
		

        // Update is called once per frame
        void Update()
        {

            if (!TimerOn)
            {
				// Disable timer in event player loses the quicktime event
				if (!NoDice)
				{
                	// Quicktime event is over - do whatever then disable the object maybe?
					DanceRequestHandler.EndQuicktimeEvent();
				}
				// Lock so that the end event coroutine is not called multiple times
				NoDice = true;
                return;
            }

            // Track what limb is currently being rotated
            CurrentLimb = InputController.CurrentLimb;
            // Tell player what button to press for quicktime event
            DisplayInstruction();
            // Decrement timer
            HandleTimer();
            // Check to see if player's limbs are in position
            CheckGoalPositions();
            // If all limbs are in position, dance event over
            if (ArmRightInPlace && LegRightInPlace && ArmLeftInPlace && LegLeftInPlace)
            {
                Debug.Log("All limbs in position with " + RemainingTime + " seconds to spare!!");
                TimerOn = false;
                DanceRequestHandler.EndQuicktimeEvent();
            }
        }

        // Need to implement with UI
        void DisplayInstruction()
        {
            float currentRotation;
            switch (CurrentLimb)
            {
                // Additional logic required for right side of body - Q4->Q1 transition is tricky
                case Limb.ArmRight:
                    currentRotation = ArmRightPivot.transform.rotation.eulerAngles.z;
                    // If in first quadrant
                    if (currentRotation >= 0f && currentRotation < 90f)
                    {
                        if (currentRotation < GoalPose.ArmRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press Q");
                        }
                        else if (currentRotation > GoalPose.ArmRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press E");
                        }
                    }
                    // If in fourth quadrant
                    else 
                    {
                        if (currentRotation > GoalPose.ArmRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press Q");
                        }
                        else if (currentRotation < GoalPose.ArmRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press E");
                        }
                    }
                    break;
                case Limb.LegRight:
                    currentRotation = LegRightPivot.transform.rotation.eulerAngles.z;
                    // If in first quadrant 
                    if (currentRotation >= 0f && currentRotation < 90f)
                    {
                        if (currentRotation < GoalPose.LegRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press Q");
                        }
                        else if (currentRotation > GoalPose.LegRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press E");
                        }
                    }
                    // If in fourth or third quadrant
                    else
                    {
                        if (currentRotation > GoalPose.LegRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press Q");
                        }
                        else if (currentRotation < GoalPose.LegRightGoalRotation.eulerAngles.z)
                        {
                            //Debug.Log("Press E");
                        }
                    }
                    break;
                case Limb.LegLeft:
                    currentRotation = LegLeftPivot.transform.rotation.eulerAngles.z;
                    if (currentRotation > GoalPose.LegLeftGoalRotation.eulerAngles.z)
                    {
                        //Debug.Log("Press Q");
                    }
                    else if (currentRotation < GoalPose.LegLeftGoalRotation.eulerAngles.z)
                    {
                        //Debug.Log("Press E");
                    }
                    break;
                case Limb.ArmLeft:
                    currentRotation = ArmLeftPivot.transform.rotation.eulerAngles.z;
                    if (currentRotation < GoalPose.ArmLeftGoalRotation.eulerAngles.z)
                    {
                        //Debug.Log("Press Q");
                    }
                    else if (currentRotation > GoalPose.ArmLeftGoalRotation.eulerAngles.z)
                    {
                        //Debug.Log("Press E");
                    }
                    break;
                default:
                    break;
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
            if (armRightRotation > GoalPose.ArmRightGoalRotation.eulerAngles.z - ErrorMargin
                && armRightRotation < GoalPose.ArmRightGoalRotation.eulerAngles.z + ErrorMargin
                && !ArmRightInPlace)
            {
                ArmRightInPlace = true;
                GoNext();
            }

            // need to tweak this goal check for the splits
            float legRightRotation = LegRightPivot.transform.rotation.eulerAngles.z;
            if (legRightRotation > GoalPose.LegRightGoalRotation.eulerAngles.z - ErrorMargin
                && legRightRotation < GoalPose.LegRightGoalRotation.eulerAngles.z + ErrorMargin
                && !LegRightInPlace)
            {
                LegRightInPlace = true;
                GoNext();
            }


            float armLeftRotation = ArmLeftPivot.transform.rotation.eulerAngles.z;
            if (armLeftRotation > GoalPose.ArmLeftGoalRotation.eulerAngles.z - ErrorMargin
                && armLeftRotation < GoalPose.ArmLeftGoalRotation.eulerAngles.z + ErrorMargin
                && !ArmLeftInPlace)
            {
                ArmLeftInPlace = true;
                GoNext();
            }

            float legLeftRotation = LegLeftPivot.transform.rotation.eulerAngles.z;
            if (legLeftRotation > GoalPose.LegLeftGoalRotation.eulerAngles.z - ErrorMargin
                && legLeftRotation < GoalPose.LegLeftGoalRotation.eulerAngles.z + ErrorMargin
                && !LegLeftInPlace)
            {
                LegLeftInPlace = true;
                GoNext();
            }
        }

		// Not ideal implementation - need to think real hard ab this lmao
        void GoNext()
        {
            switch (CurrentLimb)
            {
                case Limb.ArmRight:
                    InputController.CurrentLimb = Limb.LegRight;
                    break;
                case Limb.LegRight:
                    InputController.CurrentLimb = Limb.LegLeft;
                    break;
                case Limb.LegLeft:
                    InputController.CurrentLimb = Limb.ArmLeft;
                    break;
                case Limb.ArmLeft:
                    InputController.CurrentLimb = Limb.ArmRight;
                    break;
                default:
                    break;
            }
        }

        // TODO: Handle displaying the UI through here
        public void DisplayUI()
        {
            Debug.Log("Dance event UI displayed");
        }
    }
}
