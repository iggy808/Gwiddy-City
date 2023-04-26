using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    public enum Limb
    {
        ArmRight,
        LegRight,
        ArmLeft,
        LegLeft
    }

    public class InputController : MonoBehaviour
    {
        // Arm right pivot contains rotation constraints
        GameObject ArmRightPivot;
        GameObject LegRightPivot;
        GameObject ArmLeftPivot;
        GameObject LegLeftPivot;

        float RotationSpeed = 2f;
        Limb CurrentLimb;

        // Start is called before the first frame update
        void Start()
        {
            ArmRightPivot = GameObject.Find("ArmRightPivot");
            LegRightPivot = GameObject.Find("LegRightPivot");
            ArmLeftPivot = GameObject.Find("ArmLeftPivot");
            LegLeftPivot = GameObject.Find("LegLeftPivot");

            CurrentLimb = Limb.ArmRight;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchLimbs();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log(LegRightPivot.transform.rotation.eulerAngles.z);
            }
            
            HandleInput();
        }

        void HandleInput()
        {
            float currentRotation;
            switch (CurrentLimb)
            {
                case Limb.ArmRight:
                    currentRotation = ArmRightPivot.transform.rotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation + RotationSpeed < 90f || currentRotation + RotationSpeed > 270f))
                    {
                        ArmRightPivot.transform.rotation = Quaternion.Euler(0f,0f,currentRotation + RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && (currentRotation - RotationSpeed < 90f || currentRotation - RotationSpeed > 270f - RotationSpeed))
                    {
                        ArmRightPivot.transform.rotation = Quaternion.Euler(0f,0f,currentRotation - RotationSpeed);
                    }
                    break;
                case Limb.LegRight:
                    currentRotation = LegRightPivot.transform.rotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation - RotationSpeed > 225f - RotationSpeed || currentRotation - RotationSpeed < 45f))
                    {
                        LegRightPivot.transform.rotation = Quaternion.Euler(0f,0f, currentRotation - RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && ((currentRotation + RotationSpeed < 360f && currentRotation + RotationSpeed > 225f) || (currentRotation + 2*RotationSpeed > 0f && currentRotation + RotationSpeed < 45f)))
                    {
                        LegRightPivot.transform.rotation = Quaternion.Euler(0f,0f, currentRotation + RotationSpeed);
                    }
                    break;
                case Limb.ArmLeft:
                    currentRotation = ArmLeftPivot.transform.rotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation + RotationSpeed < 270f))
                    {
                        ArmLeftPivot.transform.rotation = Quaternion.Euler(0f,0f, currentRotation + RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && (currentRotation - RotationSpeed > 90f - RotationSpeed))
                    {
                        ArmLeftPivot.transform.rotation = Quaternion.Euler(0f,0f, currentRotation - RotationSpeed);
                    }
                    break;
                case Limb.LegLeft:
                    currentRotation = LegLeftPivot.transform.rotation.eulerAngles.z;
                    if (Input.GetKey(KeyCode.Q) && (currentRotation - RotationSpeed > 135f))
                    {
                        LegLeftPivot.transform.rotation = Quaternion.Euler(0f,0f, currentRotation - RotationSpeed);
                    }
                    if (Input.GetKey(KeyCode.E) && (currentRotation + RotationSpeed < 315f - RotationSpeed))
                    {
                        LegLeftPivot.transform.rotation = Quaternion.Euler(0f,0f, currentRotation + RotationSpeed);
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
            Debug.Log(CurrentLimb);
        }
    }
}