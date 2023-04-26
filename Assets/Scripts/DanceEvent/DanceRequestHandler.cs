using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    public class DanceRequestHandler : MonoBehaviour
    {
        //DanceRequest DanceRequest;
        bool DanceEventRequest = true;
        
        void Awake()
        {
            if (DanceEventRequest)
            {
                DanceEventManager EventManager = GameObject.Find("DanceEventBackground").GetComponent<DanceEventManager>();
                // call on event manager to handle event and ui etc
                EventManager.DisplayUI();
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
