using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    public class InputController : MonoBehaviour
    {
		// dont think i need context here
		public DanceRequestContext Context;
        // Arm right pivot contains rotation constraints
		[SerializeField]
        public GameObject ArmRightPivot;
		[SerializeField]
        public GameObject LegRightPivot;
		[SerializeField]
        public GameObject ArmLeftPivot;
		[SerializeField]
        public GameObject LegLeftPivot;
		

        float RotationSpeed = 10f; // Breaks at 5 rotation speed lmao- need to implement clamp and see if that fixes
        public Limb CurrentLimb;

        // Start is called before the first frame update
        void Start()
        {
            CurrentLimb = Limb.ArmRight;
        }

        // Update is called once per frame
        void Update()
        {
			/*	
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchLimbs();
            }
			*/
            
            HandleInput();
        }

        // Spencer suggested mathf.clamp here- need to reimplement!
        void HandleInput()
        {
            float currentRotation;
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
