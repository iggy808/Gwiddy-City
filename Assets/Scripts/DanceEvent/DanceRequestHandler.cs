using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
    // can disable the HUD/battle dance event canvases on and off from this script
    // i think it would be super cool to have the quick time event in worldspace lol - would be a little tricky, but could make it happen
    public class DanceRequestHandler : MonoBehaviour
    {
        public DanceRequestContext Context;
        GameObject DanceEvent;
        DanceEventManager DanceEventManager;
        InputController InputController;

		GameObject BattleDanceUI;
		GameObject EnvDanceUI;

		
        void Awake()
        {	
			BattleDanceUI = GameObject.Find("BattleDanceEvent");
			EnvDanceUI = GameObject.Find("EnvironmentalDanceEvent");
        }

		void Start()
		{
			BattleDanceUI.SetActive(false);
			EnvDanceUI.SetActive(false);
		}

		public void ActivateDanceEvent(DanceRequestContext context)
		{
			Context = context;
			DisableUnwantedChildren();	
			ConfigureDanceEvent();	
			TriggerDanceEvent();
		}


		void ConfigureDanceEvent()
		{	
			// Configure dance event manager for the required ui
			switch (Context.Environment)
			{
                case Environment.BattleDance:
                    DanceEvent = BattleDanceUI;  
                    break;
				case Environment.EnvDance:
					DanceEvent = EnvDanceUI; 
					break;
                default:
                    break;
			}

			// Enable object for configuration
			DanceEvent.SetActive(true);

			// Assign components to be handled throughout the course of the event		
			DanceEventManager = DanceEvent.GetComponent<DanceEventManager>();	
			InputController = DanceEvent.GetComponent<InputController>();
			DanceEventManager.InputController = InputController;
			
			DanceEventManager.ConfigureDanceEventInternal(Context);
			
			// Initialize components and game object to off
			DanceEventManager.enabled = false;
			InputController.enabled = false;

			// Disable to allow for delayed start
			DanceEvent.SetActive(false);
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

        public void TriggerDanceEvent()
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

        public void EndQuicktimeEvent()
        {
			InputController.enabled = false;
            StartCoroutine(DelayDisable());
        }
    }
}
