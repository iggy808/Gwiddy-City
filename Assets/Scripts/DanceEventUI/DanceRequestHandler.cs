using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BattleEvent;

namespace DanceEvent
{
    // can disable the HUD/battle dance event canvases on and off from this script
    // i think it would be super cool to have the quick time event in worldspace lol - would be a little tricky, but could make it happen
    public class DanceRequestHandler : MonoBehaviour
    {
        public DanceRequestContext Context;
		public bool IsEventActive;
		public int CurrentBattleMoveCount;
		public int CurrentSequencePoseIndex;
		/*
		if (WasSuccessful)
		{
			CurrentPoseIndex += 1;
		};
		*/
		
		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		BattleEventManager BattleManager;
		[SerializeField]
		GameObject BattleDanceUI;
		[SerializeField]
		GameObject EnvDanceUI;
		[SerializeField] // TEST CHANGE
		DanceUIManager BattleDanceUIManager; // Unnecessary at the moment, but can do something with it later if needed
		[SerializeField]
		DanceUIManager EnvDanceUIManager;
		[SerializeField]
		GameObject BattleEventUI;
		[SerializeField]
		BattleEvent.InputController BattleInputController;

		GameObject DanceEventUI;
        DanceEventManager DanceEventManager;
        DanceEvent.InputController DanceInputController;	
		DanceUIManager DanceUIManager;
		List<Pose> DanceSequence;
		bool IsSequenceEvent;
		bool TerminateSequence;
		
		void Start()
		{
			BattleDanceUI.SetActive(false);
			//BattleEventUI.SetActive(false);
			EnvDanceUI.SetActive(false);
		}

		public void ActivateDanceEvent(DanceRequestContext context)
		{
			Context = context;
			if (!IsEventActive)
			{
				IsEventActive = true;
				CurrentSequencePoseIndex = 0;

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
				DanceInputController = DanceEventUI.GetComponent<InputController>();
				

				DisableUnwantedChildren();	
				ConfigureQuicktimeEvent();	
				TriggerQuicktimeEvent();
			}
			else
			{
				Debug.Log("Dance event already active, dance request canceled.");
			}
		}

		public void ActivateDanceSequenceEvent(DanceRequestContext context)
		{
			IsSequenceEvent = true;
			Context = context;
			DanceSequence = context.DesiredMoves;

			// First go in the sequence, initialize sequence pose index to 0
			if (!IsEventActive)
			{
				Debug.Log("First pose activated");
				IsEventActive = true;	
				CurrentSequencePoseIndex = 0;	
				AssignUIAndManagers();
				DisableUnwantedChildren();	
				ConfigureQuicktimeEvent();
				TriggerQuicktimeEvent();
			}
			else if (IsEventActive && CurrentSequencePoseIndex <= context.DesiredMoves.Count - 1)
			{
				ConfigureQuicktimeEvent();
				TriggerQuicktimeEvent();	
			}
			else if (IsEventActive && CurrentSequencePoseIndex > context.DesiredMoves.Count - 1)
			{
				Debug.Log("No more poses left in sequence.");
				IsEventActive = false;
				Debug.Log("KILL IT");
				TerminateSequence = true;
				// do whateva
			}
		}

        public void EndQuicktimeEvent(bool wasSuccessful)
        {
			DanceInputController.enabled = false;
            StartCoroutine(DelayQuicktimeDisable(wasSuccessful));
        }

        void TriggerQuicktimeEvent()
        {
			Debug.Log("Dance event triggered");
			StartCoroutine(DelayQuicktimeEnable());
        }

        IEnumerator DelayQuicktimeEnable()
        {
            yield return new WaitForSeconds(0.5f);
            DanceEventUI.SetActive(true);
            DanceEventManager.enabled = true;
			DanceInputController.enabled = true;
			DanceUIManager.enabled = true;
			Debug.Log("Dance event enabled.");
        }

        IEnumerator DelayQuicktimeDisable(bool wasSuccessful)
        {
            yield return new WaitForSeconds(0.5f);
			if (!IsSequenceEvent || CurrentSequencePoseIndex >= Context.DesiredMoves.Count)
			{
				Debug.Log("Quicktime disabled (wasSuccessful, IsSequenceEvent) : " + wasSuccessful + ", " +IsSequenceEvent);
            	DanceEventManager.enabled = false;
				DanceInputController.enabled = false;
				DanceUIManager.enabled = false;
            	DanceEventUI.SetActive(false);
				IsEventActive = false;
				IsSequenceEvent = false;
			}

			if (Context != null)
			{
				if (Context.Environment == Environment.BattleDance)
				{
					if (wasSuccessful)
					{	
						Debug.Log("Dance event was successful, inflict damage called");
						BattleManager.InflictDamage(Context.DesiredMoves.ElementAt(CurrentSequencePoseIndex), CurrentSequencePoseIndex == Context.DesiredMoves.Count - 1);
					}

					CurrentSequencePoseIndex += 1;

					if (IsSequenceEvent && CurrentSequencePoseIndex < Context.DesiredMoves.Count)
					{
						Debug.Log("More Dances remain in the sequence, activating a dance sequence event");
						ActivateDanceSequenceEvent(Context);
					}
					// If there are remaining moves in the sequence, activate another dance event
					else
					{
						Debug.Log("No more moves remain in the sequence, resetting battle menu state");
						BattleInputController.ResetMenuState(wasSuccessful);
						BattleEventUI.SetActive(true);
					}
				}
				else if (Context.Environment == Environment.EnvDance && wasSuccessful)
				{
					Context.TargetObject.SetActive(false);
				}
			}
        }

		void AssignUIAndManagers()
		{
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

			DanceEventManager = DanceEventUI.GetComponent<DanceEventManager>();
			DanceInputController = DanceEventUI.GetComponent<InputController>();
		}

		void ConfigureQuicktimeEvent()
		{	
			// Enable object for configuration
			DanceEventUI.SetActive(true);

			// Configure components to dance request context
			DanceUIManager.SetUITransform(Context);			

			// RequestContext, Number of pose in sequence, Staritng limb for input controller
			DanceEventManager.ConfigureDanceEventInternal(
					Context, 
					CurrentSequencePoseIndex, 
					Context.DesiredPoseOrders.ElementAt(CurrentSequencePoseIndex).LimbRotationOrder.ElementAt(0));
			
			// Initialize components and game object to off
			DanceEventManager.enabled = false;
			DanceInputController.enabled = false;
			DanceUIManager.enabled = false;

			// Disable to allow for delayed start
			DanceEventUI.SetActive(false);
			BattleDanceUI.SetActive(false);
		}
		
		void DisableUnwantedChildren()
		{
			if (Context.Environment == Environment.BattleDance)
			{
				EnvDanceUI.SetActive(false);
				BattleEventUI.SetActive(false);	
			}
			else if (Context.Environment == Environment.EnvDance)
			{
				BattleDanceUI.SetActive(false);
			}
		}

    }
}
