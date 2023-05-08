using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image charImage;
    public Animator animator;
    public Camera DialogueCamera;
    public Camera MainCamera;
    public GameObject dialogueCanvas;

    // Keep track of sentences for dialogue
    // We might turn this into arrays for each "character"
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        
        animator.SetBool("IsOpen", false);
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        Debug.Log("Starting conversation with" + dialogue.name);
        nameText.text = dialogue.name;
        // Clear the sentences queue
        sentences.Clear();

        // Enqueue the next sentence in the sentencesHolder
        foreach(string sentence in dialogue.sentencesHolder)
        {
            sentences.Enqueue(sentence);
        }

        // Needs to be called right off the bat to display the sentence
        DisplayNextSentence();
    }


    // Trigger this when the player hits the mouse 
    public void DisplayNextSentence()
    {
        // If sentences is empty 
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // If sentences remain
        // Dequeue the next sentence
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        // Display the next sentence
        StartCoroutine(TypeSentence(sentence));
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; // wait a single frame
        }
    }

    void EndDialogue()
    {
        //gameObject.GetComponent<DialogueTrigger>().speaking = false;
        animator.SetBool("IsOpen", true);
        DialogueCamera.enabled = false; MainCamera.enabled = true;
        dialogueCanvas.SetActive(false);

        Debug.Log("End of Conversation");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
}
