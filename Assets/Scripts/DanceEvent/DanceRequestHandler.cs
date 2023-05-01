using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    // can disable the HUD/battle dance event canvases on and off from this script
    // i think it would be super cool to have the quick time event in worldspace lol - would be a little tricky, but could make it happen
    public class DanceRequestHandler : MonoBehaviour
    {
        // DanceRequest should contain a lil packet of info that allows us to configure the event for the appropriate scenario
        public DanceRequestContext Context;
        bool DanceEventRequest = true;
        GameObject DanceEvent;
        DanceEventManager DanceEventManager;
        InputController InputController;

        void Awake()
        {
			// receive an object holding info describing type of ui required
			Context = new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMove = Pose.Splits,
                TargetUI = "BattleDanceEvent"
			};
			
			DisableUnwantedChildren();
			
			ConfigureDanceEvent(Context);
        }

        void Start()
        {
            TriggerDanceEvent(); 
        }

		void DisableUnwantedChildren()
		{
			// get children of the ui
			List<Component> directChildren = new List<Component>();
 			foreach(Transform go in gameObject.transform)
			{  
        		Component c = go.gameObject.GetComponent<Component>();
        		directChildren.Add(c);
 			}

			Debug.Log("Children count:" + directChildren.Count);
			
			// disable unwanted children
			foreach (var child in directChildren)
			{
				if (child.gameObject.name != Context.TargetUI)
				{
					child.gameObject.SetActive(false);
				}
			}	
		}

		void ConfigureDanceEvent(DanceRequestContext context)
		{
			// Configure dance event manager for the required ui
			switch (context.Environment)
			{
                case Environment.BattleDance:
                    DanceEvent =  GameObject.Find("BattleDanceEvent");
                    break;
				case Environment.EnvDance:
					DanceEvent = GameObject.Find("EnvironmentalDanceEvent");
					break;
                default:
                    break;
			}

			// Assign components to be handled throughout the course of the event		
			DanceEventManager = DanceEvent.GetComponent<DanceEventManager>();	
			InputController = DanceEvent.GetComponent<InputController>();
			
			// Initialize components and game object to off
			DanceEventManager.enabled = false;
			InputController.enabled = false;
			DanceEvent.SetActive(false);
		}

        void TriggerDanceEvent()
        {
            switch(Context.Environment)
            {
                case Environment.BattleDance:
                    Debug.Log("BattleDance requested");
                    StartCoroutine(DelayEnable());
                    break;
                case Environment.EnvDance:
                    Debug.Log("EnvDance requested");
                    StartCoroutine(DelayEnable());
                    break;
            }
        }

        public void EndQuicktimeEvent()
        {
			InputController.enabled = false;
            StartCoroutine(DelayDisable());
        }

        IEnumerator DelayDisable()
        {
            yield return new WaitForSeconds(2f);
            DanceEventManager.enabled = false;
			InputController.enabled = false;
            DanceEvent.SetActive(false);
        }

        IEnumerator DelayEnable()
        {
            yield return new WaitForSeconds(2f);
            DanceEvent.SetActive(true);
            DanceEventManager.enabled = true;
			InputController.enabled = true;
        }
    }
}
