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

		[SerializeField]
		GameObject BattleDanceUI;
		[SerializeField]
		GameObject EnvDanceUI;

		GameObject DanceEventUI;
        DanceEventManager DanceEventManager;
        InputController InputController;	
		
        void Awake()
        {	
        }

		void Start()
		{
			// Maybe not necessary now that i am assigning objects in the inspector
			BattleDanceUI.SetActive(false);
			EnvDanceUI.SetActive(false);
		}

		public void ActivateDanceEvent(DanceRequestContext context)
		{
			Context = context;
			if (Context.Environment == Environment.BattleDance)
			{
				DanceEventUI = BattleDanceUI;
			}
			else if (Context.Environment == Environment.EnvDance)
			{
				DanceEventUI = EnvDanceUI;
			}

			DanceEventManager = DanceEventUI.GetComponent<DanceEventManager>();
			InputController = DanceEventUI.GetComponent<InputController>();

			DisableUnwantedChildren();	
			ConfigureDanceEvent();	
			TriggerDanceEvent();
		}

		void ConfigureDanceEvent()
		{	
			// Enable object for configuration
			DanceEventUI.SetActive(true);

			DanceEventManager.ConfigureDanceEventInternal(Context);
			
			// Initialize components and game object to off
			DanceEventManager.enabled = false;
			InputController.enabled = false;

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

        public void TriggerDanceEvent()
        {
			Debug.Log("Dance event triggered");
			StartCoroutine(DelayEnable());
        }

        IEnumerator DelayDisable()
        {
            yield return new WaitForSeconds(2f);
            DanceEventManager.enabled = false;
			InputController.enabled = false;
            DanceEventUI.SetActive(false);
        }

        IEnumerator DelayEnable()
        {
            yield return new WaitForSeconds(2f);
            DanceEventUI.SetActive(true);
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
