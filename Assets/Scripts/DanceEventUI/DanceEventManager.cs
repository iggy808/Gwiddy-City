using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DanceEvent 
{
    // need to work on making the dance events configurable to environment/desired move/moves
    // potentially should be able to hand a list of moves to do in sequence?
    public class DanceEventManager : MonoBehaviour
    {
		// tweak these depending on desired move accordingly public bool ArmRightInPlace = false;
        public bool LegRightInPlace = false;
        public bool ArmLeftInPlace = false;
        public bool LegLeftInPlace = false;
		public bool ArmRightInPlace = false;
        public InputController InputController;
        public DanceRequestHandler DanceRequestHandler;
		public GoalPose GoalPose; 

		// Game object references for checking limbs for goal position
		[SerializeField]
        GameObject ArmRightPivot;
		[SerializeField]
        GameObject LegRightPivot;
		[SerializeField]
        GameObject ArmLeftPivot;
		[SerializeField]
        GameObject LegLeftPivot;
		[SerializeField]
		DanceEventUIManager DanceEventUIManager;

		// QTE control variables
		Limb CurrentLimb; 
		int CurrentLimbCount;
		DanceRequestContext Context; 
		float ErrorMargin = 4f;
		// QTE state variables
        bool TimerOn;
        bool WasSuccessful;
        float RemainingTime;
		int CurrentSequencePoseIndex;

		bool IsEnemyQuicktimeEvent;

		public void ConfigureDanceEventInternal(DanceRequestContext context, int currentSequencePoseIndex, Limb startingLimb, bool IsEnemyTurn = false)
		{
			IsEnemyQuicktimeEvent = IsEnemyTurn;
			Context = context;
			CurrentSequencePoseIndex = currentSequencePoseIndex;
			CurrentLimbCount = 0;

			GoalPose.SetGoalRotations(context.DesiredMoves.ElementAt(currentSequencePoseIndex));
			InitializeLimbPosition();
            GoalPose.DisplayGoalRotations(); 

			InputController.CurrentLimb = startingLimb;
			InputController.InitializeInputController(IsEnemyTurn);
			InitializeEvent();
		}

		void InitializeLimbPosition()
		{
			ArmRightPivot.transform.localRotation = Quaternion.Euler(0f,0f,0f);
			LegRightPivot.transform.localRotation = Quaternion.Euler(0f,0f,270f);
			ArmLeftPivot.transform.localRotation = Quaternion.Euler(0f,0f,180f);
			LegLeftPivot.transform.localRotation = Quaternion.Euler(0f,0f,270f);
		}

		void InitializeEvent()
		{
			RemainingTime = 10f;
			WasSuccessful = false;
			CurrentLimbCount = 0;
			ArmRightInPlace = false;
			LegRightInPlace = false;
			ArmLeftInPlace = false;
			LegLeftInPlace = false;
			TimerOn = true;
		}
		

        // Update is called once per frame
        void Update()
        {

            if (!TimerOn)
            {
				// Disable timer in event player loses the quicktime event
				if (!WasSuccessful)
				{
                	// Quicktime event is over - do whatever then disable the object maybe?
					DanceRequestHandler.EndQuicktimeEvent(WasSuccessful);
				}
				// Lock "WasSuccessful" when timer is not on to ensure that the end event coroutine is only called once
				WasSuccessful = true;
                return;
            }
			
			if (CurrentLimbCount < Context.DesiredPoseOrders.ElementAt(CurrentSequencePoseIndex).LimbRotationOrder.Count)
			{
            	InputController.CurrentLimb = Context.DesiredPoseOrders.ElementAt(CurrentSequencePoseIndex).LimbRotationOrder.ElementAt(CurrentLimbCount);
				//Highlight currently controlled limb
				DanceEventUIManager.HighlightGoalLimb(InputController.CurrentLimb);
			}

            // Tell player what button to press for quicktime event
			if (!IsEnemyQuicktimeEvent)
			{
            	DisplayInstruction();
			}

            // Decrement timer
            HandleTimer();
            
			// Check to see if player's limbs are in position
            CheckGoalPositions();

            // If all limbs are in position, dance event over
            if (ArmRightInPlace && LegRightInPlace && ArmLeftInPlace && LegLeftInPlace)
            {
                Debug.Log("All limbs in position with " + RemainingTime + " seconds to spare!!");
                TimerOn = false;
				WasSuccessful = true;
                DanceRequestHandler.EndQuicktimeEvent(WasSuccessful);
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

        void GoNext()
        {
			CurrentLimbCount += 1;
			if (CurrentLimbCount < Context.DesiredPoseOrders.ElementAt(CurrentSequencePoseIndex).LimbRotationOrder.Count)
			{
				InputController.CurrentLimb = Context.DesiredPoseOrders.ElementAt(CurrentSequencePoseIndex).LimbRotationOrder.ElementAt(CurrentLimbCount);
			}
        }

        // TODO: Handle displaying the UI through here
        public void DisplayUI()
        {
            //Debug.Log("Dance event UI displayed");
        }
    }
}
