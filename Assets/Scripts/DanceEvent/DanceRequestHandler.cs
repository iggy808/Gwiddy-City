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
		public bool IsEventActive;

		[SerializeField]
		GameObject BattleDanceUI;
		[SerializeField]
		GameObject EnvDanceUI;
		[SerializeField] // TEST CHANGE
		DanceUIManager BattleDanceUIManager; // Unnecessary at the moment, but can do something with it later if needed
		[SerializeField]
		DanceUIManager EnvDanceUIManager;

		GameObject DanceEventUI;
        DanceEventManager DanceEventManager;
        InputController InputController;	
		DanceUIManager DanceUIManager;
		
		void Start()
		{
			// Maybe not necessary now that i am assigning objects in the inspector
			BattleDanceUI.SetActive(false);
			EnvDanceUI.SetActive(false);
		}

		public void ActivateDanceEvent(DanceRequestContext context)
		{
			Context = context;
			if (!IsEventActive)
			{
				IsEventActive = true;

				switch (Context.Environment)
				{
					case Environment.BattleDance:
						DanceEventUI = BattleDanceUI;
						DanceUIManager = BattleDanceUIManager;
						break;
					case Environment.EnvDance:
						DanceEventUI = EnvDanceUI;
						DanceUIManager = EnvDanceUIManager;
						break;
					default:
						break;
				}
				
				// Assign references to appropriate UI
				DanceEventManager = DanceEventUI.GetComponent<DanceEventManager>();
				InputController = DanceEventUI.GetComponent<InputController>();

				DisableUnwantedChildren();	
				ConfigureDanceEvent();	
				TriggerDanceEvent();
			}
			else
			{
				Debug.Log("Dance event already active, dance request canceled.");
			}
		}

        public void EndQuicktimeEvent()
        {
			InputController.enabled = false;
            StartCoroutine(DelayDisable());
        }

        void TriggerDanceEvent()
        {
			Debug.Log("Dance event triggered");
			StartCoroutine(DelayEnable());
        }

        IEnumerator DelayEnable()
        {
            yield return new WaitForSeconds(0.5f);
            DanceEventUI.SetActive(true);
            DanceEventManager.enabled = true;
			InputController.enabled = true;
			DanceUIManager.enabled = true;
        }

        IEnumerator DelayDisable()
        {
            yield return new WaitForSeconds(0.5f);
            DanceEventManager.enabled = false;
			InputController.enabled = false;
			DanceUIManager.enabled = false;
            DanceEventUI.SetActive(false);
			IsEventActive = false;
        }

		void ConfigureDanceEvent()
		{	
			// Enable object for configuration
			DanceEventUI.SetActive(true);

			// Configure components to dance request context
			DanceUIManager.SetUITransform(Context);			
			DanceEventManager.ConfigureDanceEventInternal(Context);
			
			// Initialize components and game object to off
			DanceEventManager.enabled = false;
			InputController.enabled = false;
			DanceUIManager.enabled = false;

			// Disable to allow for delayed start
			DanceEventUI.SetActive(false);
		}
		
		void DisableUnwantedChildren()
		{
			if (Context.Environment == Environment.BattleDance)
			{
				EnvDanceUI.SetActive(false);
			}
			else if (Context.Environment == Environment.EnvDance)
			{
				BattleDanceUI.SetActive(false);
			}
		}

    }
}
