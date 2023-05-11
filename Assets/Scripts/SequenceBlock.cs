using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceBlock : MonoBehaviour
{

    SequenceManager sequenceManager;
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
        if (other.gameObject.tag == "Player")
        {
            sequenceManager = GameObject.FindGameObjectWithTag("MusicPuzzle").GetComponent<SequenceManager>();
            sequenceManager.StartSequence();
        }
    }
}
