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
                    // COLIN: Find a way to get the add force to be disabled. Or to teleport the player to the NPC dialog area
                    gameObject.GetComponent<PlayerMovement>().horitontalEnable = false;
                    gameObject.GetComponent<PlayerMovement>().verticalEnable = false;
                    gameObject.GetComponent<PlayerMovement>().rb.velocity = Vector3.zero;
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
