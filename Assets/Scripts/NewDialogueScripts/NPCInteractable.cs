using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public Transform InteractedCameraPosition;
    public Camera DialogueCamera;
    public Camera MainCamera;
    public GameObject dialogueCanvas;


    // Let's do something with this
    public bool speaking;

    private void Start()
    {
        {
            speaking = false;
        }
    }
    public void Interact()
    {
        // If the target is an NPC...
        if (gameObject.tag == "NPC")
        {
            // Check if it has a DialogueTrigger script...
            // If it does...
            if (gameObject.TryGetComponent(out DialogueTrigger dialogue)){
                // Begin dialogue 
                dialogue.TriggerDialogueManager();
                // Disable Cameras
                DialogueCamera.enabled = true; MainCamera.enabled = false;
                // Move camera to NPC-specific position
                DialogueCamera.transform.position = InteractedCameraPosition.position;
                // Enable the Dialogue HUD 
                dialogueCanvas.SetActive(true);
            }
        }

        // If the target is an item.....
        // COLIN: DO THIS LATER



        // Debug comment
        Debug.Log("Interacted");
    }
}
