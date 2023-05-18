using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCInteractable : MonoBehaviour
{
    public Transform InteractedCameraPosition;
    public Camera DialogueCamera;
    public Camera MainCamera;
    public GameObject dialogueCanvas;

	[SerializeField]
	ScenarioController ScenarioController;

    public void Interact()
    {
        // If the target is an NPC...
        if (gameObject.tag == "NPC")
        {
            Debug.Log("Interacting with NPC. : " + gameObject.name);

            // Check if it has a DialogueTrigger script...
            // If it does...
            if (gameObject.TryGetComponent(out DialogueTrigger dialogue))
            {


                if (ScenarioController.CurrentState != ScenarioStates.DanceEventTutorial)
                {


                    Debug.Log("Interacting with old man. Progressing scenario");
                    // Refresh scenario controller component
                    ScenarioController = GameObject.FindWithTag("Player").transform.parent.gameObject.GetComponent<ScenarioController>();
                    // Scenario must prgress state when dialogue is triggered
                    ScenarioController.CurrentDialogueInteractionCount++;
                    ScenarioController.ProgressScenario();
                    // Begin dialogue 
                    dialogue.TriggerDialogueManager();
                    // Disable Cameras
                    DialogueCamera.enabled = true; MainCamera.enabled = false;
                    // Move camera to NPC-specific position
                    DialogueCamera.transform.position = InteractedCameraPosition.position;
                    // Enable the Dialogue HUD 
                    dialogueCanvas.SetActive(true);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }

            }

        }

    }
    private void Update()
    {
        Debug.Log("OldMan Position: " + gameObject.transform.position);
    }
}
