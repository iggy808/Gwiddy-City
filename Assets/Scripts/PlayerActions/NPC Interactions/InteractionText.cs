using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    public GameObject interactionText;
    public Transform TextPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    


    // COLIN: Displays Text when inflated trigger collider collides with the Player 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(interactionText, TextPosition.position, Quaternion.Euler(0, -90, 0), transform);

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
