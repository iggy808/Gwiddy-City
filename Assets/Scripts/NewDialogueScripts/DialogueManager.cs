using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;


    public Animator animator;


    // Keep track of sentences for dialogue
    // We might turn this into arrays for each "character"
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("IsOpen", true);
        Debug.Log("Starting conversation with" + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentencesHolder)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    // Trigger this when the player hits the mouse 
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // If sentences remain
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); // Stops displaying charcters one by one 
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
        animator.SetBool("IsOpen", false);

        Debug.Log("End of Conversation");
    }
    
}
