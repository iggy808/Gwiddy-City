using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionText;
    public Transform TextPosition;
    public Transform InteractedCameraPosition;
    public Camera DialogueCamera;
    public Camera MainCamera;
    public OldManDialogue intro;
    public GameObject dialogueCanvas;
    public bool speaking;
    // Start is called before the first frame update
    void Start()
    {
        speaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(interactionText, TextPosition.position, Quaternion.Euler(0,-90,0), transform);

        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        // Sometimes this doesn't work immediately. Just spam 'F'
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            DialogueCamera.enabled = true; MainCamera.enabled = false;
            DialogueCamera.transform.position = InteractedCameraPosition.position;
            if (gameObject.tag == "OldMan" && speaking == false)
            {
                speaking=true;
                dialogueCanvas.SetActive(true);
                intro.StartDialogue();

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && DialogueCamera.enabled == true && MainCamera.enabled == false)
        {
            DialogueCamera.enabled = false;
            MainCamera.enabled = true;
            dialogueCanvas.SetActive(false);
            speaking = false;
        }

    }
}
