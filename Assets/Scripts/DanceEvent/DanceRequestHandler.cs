using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    // can disable the HUD/battle dance event canvases on and off from this script
    // i think it would be super cool to have the quick time event in worldspace lol - would be a little tricky, but could make it happen
    // NOTE: MUST ENSURE THAT ONLY ONE DANCE UI IS PRESENT AT ANY GIVEN MOMENT
    // CANNOT DO MORE THAN ONE DANCE UI AT A TIME LOL
    public class DanceRequestHandler : MonoBehaviour
    {
        // DanceRequest should contain a lil packet of info that allows us to configure the event for the appropriate scenario
        //DanceRequest DanceRequest; 
        bool DanceEventRequest = true;
        GameObject BattleDanceEvent;
        DanceEventManager EventManager;
        
        void Awake()
        {
            if (DanceEventRequest)
            {
                // Might need to get this from a different UI component
                BattleDanceEvent =  GameObject.Find("BattleDanceEvent");
                EventManager = BattleDanceEvent.GetComponent<DanceEventManager>();
                EventManager.enabled = false;
                BattleDanceEvent.active = false;
                // call on event manager to handle event and ui etc
                //EventManager.DisplayUI();
            }
        }

        void Start()
        {
            StartCoroutine(DelayEnable());
        }

        public void EndQuicktimeEvent()
        {
            //BattleDanceEvent.active = false;
            //EventManager.enabled = false;
            StartCoroutine(DelayDisable());
        }

        IEnumerator DelayDisable()
        {
            yield return new WaitForSeconds(2f);
            EventManager.enabled = false;
            BattleDanceEvent.active = false;
        }

        IEnumerator DelayEnable()
        {
            yield return new WaitForSeconds(2f);
            BattleDanceEvent.active = true;
            EventManager.enabled = true;
        }
    }
}
