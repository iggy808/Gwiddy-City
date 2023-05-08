using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerInteract : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                // If the collider has the NPCInteractable script...
                if(collider.TryGetComponent(out NPCInteractable npc))
                {
                    Debug.Log(npc);
                  
                    if(npc.DialogueCamera.enabled == false)
                    {
                        // Call the interact function
                        npc.Interact();
                    }
                    
                }
            }
        }
    }
}
