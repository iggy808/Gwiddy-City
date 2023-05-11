using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    SequenceManager sequenceManager;

    public bool interacted;
    // Start is called before the first frame update
    void Start()
    {
        interacted = false;
    }

    public void Interact()
    {
        if (gameObject.tag == "Button" && !interacted)
        {
            Debug.Log("Button Pressed");
            AudioSource source = gameObject.GetComponent<AudioSource>();
            AudioClip clip = source.clip;
            source.PlayOneShot(clip);
            sequenceManager = GameObject.FindGameObjectWithTag("MusicPuzzle").GetComponent<SequenceManager>();
            sequenceManager.StartSequence();

        }
        
    }


    
    // Update is called once per frame
    void Update()
    {

    }
}
