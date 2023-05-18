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
            // Check if it has a DialogueTrigger script...
            // If it does...
            if (gameObject.TryGetComponent(out DialogueTrigger dialogue) ){
                
                if (ScenarioController.CurrentState != ScenarioStates.DanceEventTutorial)
                {
                    if (ScenarioController.CurrentState == ScenarioStates.NotYetEngaged)
                    {
                        ScenarioController.CurrentProgressionStage = 0;
                        ScenarioController.CurrentDialogueInteractionCount = 0;

                        ScenarioController.InitializeScenario(ScenarioController.startingScenario);
                        
                    }
                    if (ScenarioController.CurrentState == ScenarioStates.Over)
                    {
                        ScenarioController.CurrentProgressionStage = 0;
                        ScenarioController.CurrentDialogueInteractionCount = 0;

                        ScenarioController.InitializeScenario(ScenarioController.secondScenario);

                    }
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
                /*
                if (ScenarioController.CurrentState == ScenarioStates.Over)
                {
                    //
                    Debug.Log("Second Dialogue started");
                    Debug.Log("Second Scenario:\t" + ScenarioController.secondScenario);
                    // Scenario must prgress state when dialogue is triggered
                    ScenarioController.CurrentDialogueInteractionCount++;
                    ScenarioController.ProgressScenario();
                    ScenarioController.InitializeScenario(ScenarioController.secondScenario);
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
                */
            }
            
        }

    }

    private void Update()
    {
        Debug.Log("OldMan Position: " + gameObject.transform.position);
    }
}
