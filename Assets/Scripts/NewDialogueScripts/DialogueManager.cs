using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    /// <summary>
    /// You can't have an array of queues 
    /// Best we can do is probably do an array of arrays 
    /// But even then, we'd have to have specific indecies that cannot be 
    /// generalized thru this script. We'd have to functions in here to describe
    /// which texts to pull and display and which to pull from a boolean condition 
    /// or something like that 
    /// 
    /// I think it's best to simply create multiple prefabs containing different dialogues 
    /// OR 
    /// we have a another script specific to the NPC to empty the string array 
    /// hold the dialogues and replace it with desired lines, depending on a boolean
    /// or integer value depending on where the player is in the story. 
    /// 
    /// </summary>




    // Dialogue Canvas Components
    public GameObject dialogueCanvas;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public Button mouseButton;
    public Animator animator;

    // Cameras
    public Camera DialogueCamera;
    public Camera MainCamera;

    // Player Character 
    public GameObject playerCharacter;

    // Empty Queue to put a sentence in
    private Queue<string> sentences;
    // Sentence to enqueue
    string sentence;
    // Boolean to figure out if the player skipped passed dialogue
    bool clicked;


	[SerializeField]
	ScenarioController ScenarioController;


    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        mouseButton.gameObject.SetActive(false);
        animator.SetBool("IsOpen", false);
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        // Bring up dialogue canvas, fill out canvas components
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        characterImage.sprite = dialogue.characterSprite;
        // Clear the sentences queue if it's not already empty
        sentences.Clear();

        // Enqueue each sentence into the sentencesHolder
        foreach(string sentence in dialogue.sentencesHolder)
        {
            sentences.Enqueue(sentence);
        }

        // Needs to be called right off the bat to display the sentence
        DisplayNextSentence();
    }

    // Trigger this when the player hits the mouse button
    public void DisplayNextSentence()
    {
        // If sentences queue is empty (reached end of sentence holder from Dialogue.cs
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // If sentences remain
        // Dequeue to the the next sentence
        sentence = sentences.Dequeue();
        
        // Display the next sentence
        StartCoroutine(TypeSentence(sentence));
    }

    private void Update()
    {
        // If the button is clicked during a frame...
        if (Input.GetMouseButtonDown(0))
        {
            // If the text is incomplete, complete the text and display the button
            if (dialogueText.text != sentence)
            {
                clicked = true;
                StopAllCoroutines();
                dialogueText.text = sentence;
                mouseButton.gameObject.SetActive(true);

            }

        }
        // If the text is completed, display mouse button
        if (dialogueText.text == sentence)
        {
            mouseButton.gameObject.SetActive(true); 
        }
        // If the text is not completed, don't display mouse button
        if (dialogueText.text != sentence)
        {
            mouseButton.gameObject.SetActive(false);
        }
    }

    // Coroutine to type the sentence's characters
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.05f); 
        }
    }


    void EndDialogue()
    {
		if (!(ScenarioController.CurrentDialogueInteractionCount < ScenarioController.ScenarioTotalInteractionCount))
		{
			Debug.Log("PlayerMovement enabled from dialogue manager.");
        	// Enable player movement if the scenario is over
			ScenarioController.ProgressScenario();
        	playerCharacter.GetComponent<PlayerMovement>().enabled = true;
		}
		else
		{
			ScenarioController.ProgressScenario();
		}

        // Get rid of dialogue canvas
        animator.SetBool("IsOpen", true);
        dialogueCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Display main camera , hide dialogue camera 
        DialogueCamera.enabled = false; MainCamera.enabled = true;
    }
    
}
