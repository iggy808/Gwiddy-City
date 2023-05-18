using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
 
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
    public Queue<string> sentences;
    // Sentence to enqueue
    string sentence;
    // Boolean to figure out if the player skipped passed dialogue
    bool clicked;
    Rigidbody rb;

    [SerializeField]
    ScenarioController ScenarioController;


    // Start is called before the first frame update
    void Start()
    {
        rb = playerCharacter.GetComponent<Rigidbody>();

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
        Debug.Log("Current sentences.Count == " + sentences.Count);
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
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX | ~RigidbodyConstraints.FreezePositionY | ~RigidbodyConstraints.FreezePositionZ;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationX | ~RigidbodyConstraints.FreezeRotationY | ~RigidbodyConstraints.FreezeRotationZ;

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

    public void OldManTalks(ScenarioType scenarioType, ScenarioStates scenarioStates)
    {
        // COLIN: Here is a template for a dialogue line
        // sentences.Enqueue("\"\"");

        //Debug.Log("Made it into the OldManTalks function");
        if (scenarioType == ScenarioType.TutorialOldMan)
        {
            //Debug.Log("I learned we are in the Tutorial Old Man scenario");
            if (scenarioStates == ScenarioStates.InteractedOnce)
            {
                sentences.Clear();
                Debug.Log("I learned we've interacted Once");
                sentences.Enqueue("\"Hello There\"");
                sentences.Enqueue("\"I am known as Old Man\"");
                sentences.Enqueue("\"Welcome...to the world of many a name\"");
                sentences.Enqueue("\"Once known as the the Dance Central...\"");
                sentences.Enqueue("\"Or sung as DanceTron...\"");
                sentences.Enqueue("\"Or called The Land Of A Thousand Dances...\"");
                sentences.Enqueue("\"Or referred to as some other name the developers couldn't decide on\"");
                sentences.Enqueue("\"BUT!!!\"");
                sentences.Enqueue("\"Those days are long gone...\"");
                sentences.Enqueue("\"We are now both trapped in the horrible shadow of...\"");
                sentences.Enqueue("\"Gwiddy City\"");
                sentences.Enqueue("\"Yes...'tis an awful name and age for us dancers\"");
                sentences.Enqueue("\"We are scattered across this dying world, and separated into graceless factions\"");
                sentences.Enqueue("\"BUT!!\"");
                sentences.Enqueue("\"YOU ARE THE HERO WE NEED IN THIS DARKEST HOUR\"");
                sentences.Enqueue("\"YOU WILL DANCE US TO OUR SALVATION!\"");
                sentences.Enqueue("\"WE ARE FINALLY BLESSED TO BE IN YOUR PRESE-\"");
                sentences.Enqueue("\"....what?\"");
                sentences.Enqueue("\"You don't know how to dance?!?!?!\"");
                sentences.Enqueue("\"Like...\"");
                sentences.Enqueue("\"Not even a side step?!\"");
                sentences.Enqueue("\"Or a waltz?\"");
                sentences.Enqueue("\"You mean to tell me you don't know how to cha cha real smooth?\"");
                sentences.Enqueue("\"Or mambo?\"");
                sentences.Enqueue("\"NO WORRIES!!!!\"");
                sentences.Enqueue("\"I WILL TEACH YOU NOW!!!\"");
            }

            // Have some text pop up explaining controls.
            // Probably move it to OldMan/TextPos
            // Might have to do it in the NPC interactable or do it somewhere else 


            if (scenarioStates == ScenarioStates.InteractedTwice)
            {
                sentences.Clear();
                sentences.Enqueue("\"Okay good you're a fast learner\"");
                sentences.Enqueue("\"From here you must wonder the lengths of Gwiddy City\"");
                sentences.Enqueue("\"Although in pieces...\"");
                sentences.Enqueue("\"The world will be moved by your...moves\"");
                sentences.Enqueue("\"Dance moves. I mean..\"");
                sentences.Enqueue("\"AND!!!!\"");
                sentences.Enqueue("\"You will find other Dancers of various creeds and genres\"");
                sentences.Enqueue("\"Use what I have taught you\"");
                sentences.Enqueue("\"Dance your way to victory!\"");
                sentences.Enqueue("\"AND SAVE US ALL!!!\"");
            }
        }
    }
}
