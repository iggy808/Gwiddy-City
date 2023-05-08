using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    // Call the interact function
                    npc.Interact();
                }
            }
        }
    }
}
