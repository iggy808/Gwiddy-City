using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Activates when the trigger for npc interaction activates (press 'f' to talk)
public class DialogueTrigger : MonoBehaviour
{
    // Access Dialogue.cs
    public Dialogue dialogue;

    public void TriggerDialogueManager()
    {
        // Call the StartDialogue
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
