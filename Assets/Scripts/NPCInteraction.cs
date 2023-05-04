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

    // Start is called before the first frame update
    void Start()
    {
        
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
            print("Test Text Should Show");

        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            print("HELLO");
            DialogueCamera.enabled = true; MainCamera.enabled = false;
            DialogueCamera.transform.position = InteractedCameraPosition.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && DialogueCamera.enabled == true && MainCamera.enabled == false)
        {
            DialogueCamera.enabled = false;
            MainCamera.enabled = true;  
        }
    }
}
