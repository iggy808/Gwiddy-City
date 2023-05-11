using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum MouseState
{
	Environmetal,
	BattleUI
}

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    private float xRotation;
    private float yRotation;
	MouseState currentMouseState;


	[SerializeField]
	ThirdPersonCamera ThirdPersonCamera;
	[SerializeField]
	CinemachineBrain CameraBrain;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		currentMouseState = MouseState.Environmetal;
    }

    private void Update()
    {
		/*
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
		*/
    }

    public void SwitchMouseControls()
    {
		switch (currentMouseState)
		{
			case MouseState.Environmetal:
        		Cursor.lockState = CursorLockMode.Confined;
        		Cursor.visible = true;
				currentMouseState = MouseState.BattleUI;
        		//this.enabled = false;
				ThirdPersonCamera.enabled = false;	
				CameraBrain.enabled = false;
				break;
			case MouseState.BattleUI:
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				currentMouseState = MouseState.Environmetal;
				ThirdPersonCamera.enabled = true;
				CameraBrain.enabled = true;
				break;
			default:
				break;
		}
    }
}
