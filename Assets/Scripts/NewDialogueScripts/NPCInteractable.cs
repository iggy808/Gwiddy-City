using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    public Transform InteractedCameraPosition;
    public Camera DialogueCamera;
    public Camera MainCamera;
    public GameObject dialogueCanvas;

    public void Interact()
    {
        // If the target is an NPC...
        if (gameObject.tag == "NPC")
        {
            // Check if it has a DialogueTrigger script...
            // If it does...
            if (gameObject.TryGetComponent(out DialogueTrigger dialogue)){
                
                Debug.Log("Dialogue started");
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
                // Enable the speaking boolean?
                // How to disable?
                // The dialogue Manager takes in the name text and dialogue text, why not the boolean as well
                // because we need this to be generalized. We cannot have a tie to NPC interactable because that's very specific
            }
        }

        // If the target is an item.....
        // COLIN: DO THIS LATER



        // Debug comment
        Debug.Log("Interacted");
    }
}
