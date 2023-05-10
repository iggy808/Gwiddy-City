using BattleEvent;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayNote : MonoBehaviour
{
    public SequenceManager sequenceManager;
    public AudioSource source;
    public AudioClip clip;
    public Material blank;
    public Material colored;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            source.PlayOneShot(clip);
            gameObject.GetComponent<MeshRenderer>().material = colored;
            if (colored.name != "black")
            {
                for (int i = 0; i < sequenceManager.selectedNotes.Length; i++)
                {
                    if (sequenceManager.selectedNotes[i] == null)
                    {
                        if (sequenceManager.notesInputed < 6)
                        {
                            sequenceManager.notesInputed = i;
                            Debug.Log(sequenceManager.notesInputed);

                        }
                        // put a material in the selectedNotes array
                        sequenceManager.selectedNotes[i] = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                        Debug.Log(sequenceManager.selectedNotes[i]);
                        Debug.Log(sequenceManager.notes[i]);
                        break;
                    }
                }
            }
            

            // Depress cube
            //gameObject.transform.position = new Vector3(transform.position.x, -2.42f, -1.709323f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<MeshRenderer>().material = blank;

            // Un-depress cube
            //gameObject.transform.position = new Vector3(transform.position.x, -2.12f, -1.709323f);
        }
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
