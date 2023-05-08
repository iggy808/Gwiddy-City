using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCamera : MonoBehaviour
{


    public Camera dialogCameraSelf;
    // Start is called before the first frame update
    void Start()
    {
        /// COLIN: This makes sure that the camera is disabled from the get-go. No conflicting cameras 
        dialogCameraSelf.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
