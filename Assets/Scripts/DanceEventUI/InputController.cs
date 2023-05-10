using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DanceEvent
{
    public class InputController : MonoBehaviour
    {
		// dont think i need context here
		public DanceRequestContext Context;
		public bool IsEnemyQuicktimeEvent;
        // Arm right pivot contains rotation constraints
		[SerializeField]
        public GameObject ArmRightPivot;
		[SerializeField]
        public GameObject LegRightPivot;
		[SerializeField]
        public GameObject ArmLeftPivot;
		[SerializeField]
        public GameObject LegLeftPivot;
		
		[SerializeField]
		DanceEventManager DanceManager;

		GoalPose GoalPose;


        float RotationSpeed = 10f; // Breaks at 5 rotation speed lmao- need to implement clamp and see if that fixes
        public Limb CurrentLimb;

		public void InitializeInputController(bool IsEnemyTurn)
		{
			IsEnemyQuicktimeEvent = IsEnemyTurn;
			if (IsEnemyQuicktimeEvent)
			{	
				GoalPose = DanceManager.GoalPose;
			}
		}

        // Update is called once per frame
        void Update()
        {   
            HandleInput();
        }

        // Spencer suggested mathf.clamp here- need to reimplement!
        void HandleInput()
        {
            float currentRotation;
			if (IsEnemyQuicktimeEvent)
			{
				Debug.Log("Handling enemy quicktime event input.");
				Debug.Log("Enemy CurrentLimb : " + CurrentLimb);
				switch (CurrentLimb)
				{
					case Limb.ArmRight:
						// Ideally, rotate limbs towards goal position automatically for enemy
						//Vector3 goalRotation = GoalPose.ArmRightGoalRotation.eulerAngles;
						//ArmRightPivot.transform.localRotation = Quaternion.Euler(goalRotation.x, goalRotation.y, goalRotation.z - currentRotation + RotationSpeed);
						// Alternatively, immediately set rotation to goal rotation
						ArmRightPivot.transform.localRotation = GoalPose.ArmRightGoalRotation;
						break;
					case Limb.LegRight:
						LegRightPivot.transform.localRotation = GoalPose.LegRightGoalRotation;
						break;
					case Limb.ArmLeft:
						ArmLeftPivot.transform.localRotation = GoalPose.ArmLeftGoalRotation;
						break;
					case Limb.LegLeft:
						LegLeftPivot.transform.localRotation = GoalPose.LegLeftGoalRotation;
						break;
					default:
						break;
				}
				return;
			}
            switch (CurrentLimb)
            {
                case Limb.ArmRight:
                    currentRotation = ArmRightPivot.transform.localRotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation + RotationSpeed < 90f || currentRotation + RotationSpeed > 270f))
                    {
                        ArmRightPivot.transform.localRotation = Quaternion.Euler(0f,0f,currentRotation + RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && (currentRotation - RotationSpeed < 90f || currentRotation - RotationSpeed > 270f - RotationSpeed))
                    {
                        ArmRightPivot.transform.localRotation = Quaternion.Euler(0f,0f,currentRotation - RotationSpeed);
                    }
                    break;
                case Limb.LegRight:
                    currentRotation = LegRightPivot.transform.localRotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation - RotationSpeed > 225f - RotationSpeed || currentRotation - RotationSpeed < 45f))
                    {
                        LegRightPivot.transform.localRotation = Quaternion.Euler(0f,0f, currentRotation - RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && ((currentRotation + RotationSpeed < 360f && currentRotation + RotationSpeed > 225f) || (currentRotation + 2*RotationSpeed > 0f && currentRotation + RotationSpeed < 45f)))
                    {
                        LegRightPivot.transform.localRotation = Quaternion.Euler(0f,0f, currentRotation + RotationSpeed);
                    }
                    break;
                case Limb.ArmLeft:
                    currentRotation = ArmLeftPivot.transform.localRotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation + RotationSpeed < 270f))
                    {
                        ArmLeftPivot.transform.localRotation = Quaternion.Euler(0f,0f, currentRotation + RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && (currentRotation - RotationSpeed > 90f - RotationSpeed))
                    {
                        ArmLeftPivot.transform.localRotation = Quaternion.Euler(0f,0f, currentRotation - RotationSpeed);
                    }
                    break;
                case Limb.LegLeft:
                    currentRotation = LegLeftPivot.transform.localRotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation - RotationSpeed > 135f))
                    {
                        LegLeftPivot.transform.localRotation = Quaternion.Euler(0f,0f, currentRotation - RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && (currentRotation + RotationSpeed < 315f - RotationSpeed))
                    {
                        LegLeftPivot.transform.localRotation = Quaternion.Euler(0f,0f, currentRotation + RotationSpeed);
                    }
                    break;
                default:
                    break;
            }
        }

        void SwitchLimbs()
        {
            switch (CurrentLimb)
            {
                case Limb.ArmRight:
                    CurrentLimb = Limb.LegRight;
                    break;
                case Limb.LegRight:
                    CurrentLimb = Limb.LegLeft;
                    break;
                case Limb.LegLeft:
                    CurrentLimb = Limb.ArmLeft;
                    break;
                case Limb.ArmLeft:
                    CurrentLimb = Limb.ArmRight;
                    break;
                default:
                    break;
            }
        }
    }
}
