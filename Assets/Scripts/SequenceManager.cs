using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{

    public ItemInteractable block;
    public Material[] notes = new Material[6];
    public Material[] selectedNotes = new Material[6];
    public int notesInputed;
    public Material blank;
    private Queue<GameObject> queue;
    GameObject current;
    public GameObject player;
    public GameObject door;
    public bool opened;
    public bool isEqual;
    public bool failed;
    // Start is called before the first frame update
    void Start()
    {
        failed = false;
        opened = false;
        queue = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        isEqual = Enumerable.SequenceEqual(selectedNotes, notes);
        
        if (block.GetComponent<AudioSource>().isPlaying )
        {
            block.interacted = true;
        }
        if (!block.GetComponent<AudioSource>().isPlaying )
        {
            block.interacted = false;
        }
        // if the audio source is done playing, grey everything in the queue
        if (!block.GetComponent<AudioSource>().isPlaying && queue.Count >0 && !opened)
        {
            // Grey everything in the queue 
            //Debug.Log("Emptying queue");
            current = queue.Dequeue();
            for (int i = 0; i < selectedNotes.Length; i++)
            {
                selectedNotes[i] = null;
            }
            notesInputed = 0;
            StartCoroutine(Grey(current, blank));
        }
        
        // If selectedNotes array is full of materials 
        // and selectedNotes and notes arrays match
        // And the door isn't open
        if (selectedNotes[5] != null && isEqual  && !opened)
        {

            // Open the door 
            //Debug.Log("selected notes at index 5 = " + selectedNotes[5]);
            //Debug.Log("THEY ARE MATCHING");
            //Debug.Log("Opening Door");
            
            door.transform.position += new Vector3(0, 4, 0);
            opened = true;
            player.GetComponent<PlayerOrbCollection>().total += 30;
        }


        // If the sequence fails
        // and the first note exists and the door is still unopened
        if (selectedNotes[notesInputed] != notes[notesInputed] && selectedNotes[0] != null && !opened)
        {
            // clear the selectednotes array
            for(int i = 0; i < selectedNotes.Length; i++)
            {
                selectedNotes[i] = null;
            }
            //Debug.Log("THEY ARE NOT MATCHING");

            // Grey everything
            // For each child of sequencemanager (sequence1-6)
            for (int j = 0; j < this.gameObject.transform.childCount; j++)
            {
                // set it equal to a game object 
                GameObject Go = this.gameObject.transform.GetChild(j).gameObject;
                StartCoroutine(Grey(Go, blank));
            }
            // reset input?
            notesInputed = 0;
            
        }

        // If the sequence matches
        // and the materials between the two arrays match !!!!
        // and the door hasn't been opened 
        if (selectedNotes[notesInputed] == notes[notesInputed] && selectedNotes[0] != null   && !opened)
        {
            //Debug.Log("Im gonna color and notesInputed = " + notesInputed);

            // Get the children game objects 
            // Since the two arrays have identical materials so far  
            for (int i = notesInputed; i < this.gameObject.transform.childCount; i++)
            {
                // Color the children gameobjects what they're supposed to be 
                GameObject Go = this.gameObject.transform.GetChild(i).gameObject;
                StartCoroutine(Color(Go, notes[i]));
                break;
            }
            
            
        }
        
    }

    // Get the children gameobjects of Sequence Manager 
    // Add them to the queue of game objects 
    // color them 
    public void StartSequence()
    {
        for (int i = 0;i < this.gameObject.transform.childCount; i++)
        {
            GameObject Go = this.gameObject.transform.GetChild(i).gameObject;
            queue.Enqueue(Go);
            StartCoroutine(Color(Go, notes[i]));    
            //Debug.Log(Go.name);
        }
    }

    IEnumerator Color(GameObject aBlock, Material m)
    {
        aBlock.GetComponent<MeshRenderer>().material = m;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Grey(GameObject aBlock, Material m)
    {
        aBlock.GetComponent<MeshRenderer>().material = m;
        yield return new WaitForSeconds(1f);
    }
}
