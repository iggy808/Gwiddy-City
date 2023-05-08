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

		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		BattleEventManager BattleManager;
		[SerializeField]
		GameObject BattleDanceUI;
		[SerializeField]
		GameObject EnvDanceUI;
		[SerializeField] // TEST CHANGE
		DanceEventUITransformController BattleDanceUITransformController; // Unnecessary at the moment, but can do something with it later if needed
		[SerializeField]
		DanceEventUITransformController EnvDanceUITransformController;
		[SerializeField]
		GameObject BattleEventUI;
		[SerializeField]
		GameObject BattleEventUIComponents;
		[SerializeField]
		BattleEvent.InputController BattleInputController;

		GameObject DanceEventUI;
        DanceEventManager DanceEventManager;
        DanceEvent.InputController DanceInputController;	
		DanceEventUITransformController DanceEventUITransformController;
		List<Pose> DanceSequence;
		bool IsSequenceEvent;

		int SequenceCoolness;
		int SequenceStaminaCost;

		void Start()
		{
			BattleDanceUI.SetActive(false);
			EnvDanceUI.SetActive(false);
		}

		public void ActivateDanceEvent(DanceRequestContext context)
		{
			Context = context;
			if (!IsEventActive)
			{
				IsEventActive = true;
				CurrentSequencePoseIndex = 0;
				SequenceCoolness = 0;
				SequenceStaminaCost = 0;

				AssignUIAndManagers();	
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

			// First go in the sequence, initialize sequence tracking variables
			// CurrentSequencePoseIndex tracks the current index of the current pose in the list of poses required in the sequence
			//     Note: DanceEventManager handles the internal configuration for dance events and poses
			//
			// TotalSequenceCoolness tracks the total coolness successfully obtained from the entire sequence
			//     Note: Total stamina cost / net coolness gain computed at the end of sequence
			if (!IsEventActive)
			{
				IsEventActive = true;	
				CurrentSequencePoseIndex = 0;	
				SequenceCoolness = 0;
				SequenceStaminaCost = 0;

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
			DanceEventUITransformController.enabled = true;
			Debug.Log("Dance event enabled.");
        }

        IEnumerator DelayQuicktimeDisable(bool wasSuccessful)
        {
            yield return new WaitForSeconds(0.5f);
			if (!IsSequenceEvent)//|| CurrentSequencePoseIndex >= Context.DesiredMoves.Count)
			{
				Debug.Log("WEnt through !IsSequenceEvent blovk");
            	DanceEventManager.enabled = false;
				DanceInputController.enabled = false;
				DanceEventUITransformController.enabled = false;
            	DanceEventUI.SetActive(false);
				IsEventActive = false;
				IsSequenceEvent = false;
				if (Context != null)
				{
					if (Context.Environment == Environment.BattleDance)
					{
						Debug.Log("Handled sequence stats. Sequence Coolness: " + SequenceCoolness);
						if (wasSuccessful)
						{
							SequenceCoolness += BattleManager.GetPoseCoolness(Context.DesiredMoves.ElementAt(CurrentSequencePoseIndex));		
						}
						SequenceStaminaCost += BattleManager.GetPoseStaminaCost(Context.DesiredMoves.ElementAt(CurrentSequencePoseIndex));
						BattleManager.HandleSequenceStats(SequenceCoolness, SequenceStaminaCost);
					}
				}
			}

			if (Context != null)
			{
				if (Context.Environment == Environment.BattleDance && IsSequenceEvent)
				{
					if (wasSuccessful)
					{	
						Debug.Log("Dance event was successful, inflict damage called");
						// Increment sequence damage in batch and apply at the end
						// Note: batch handling of sequence events allows for groove meter to increase coolness?
						SequenceCoolness += BattleManager.GetPoseCoolness(Context.DesiredMoves.ElementAt(CurrentSequencePoseIndex));
					}

					SequenceStaminaCost += BattleManager.GetPoseStaminaCost(Context.DesiredMoves.ElementAt(CurrentSequencePoseIndex));

					CurrentSequencePoseIndex += 1;

					if (IsEventActive && CurrentSequencePoseIndex < Context.DesiredMoves.Count)
					{
						Debug.Log("More Dances remain in the sequence, activating a dance sequence event");
						ActivateDanceSequenceEvent(Context);
					}
					// If there are remaining moves in the sequence, activate another dance event
					else if (IsEventActive && CurrentSequencePoseIndex >= Context.DesiredMoves.Count)
					{
						Debug.Log("No remaining moves in the sequence. Ending Dance Event.");
            			DanceEventManager.enabled = false;
						DanceInputController.enabled = false;
						DanceEventUITransformController.enabled = false;
            			DanceEventUI.SetActive(false);
						IsEventActive = false;
						IsSequenceEvent = false;
						
						Debug.Log("Handling Sequence Stats");
						BattleManager.HandleSequenceStats(SequenceCoolness, SequenceStaminaCost);
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
					DanceEventUITransformController = BattleDanceUITransformController;
					break;
				case Environment.EnvDance:
					DanceEventUI = EnvDanceUI;
					DanceEventUITransformController = EnvDanceUITransformController;
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
			
			//
			// Configure components to dance request context
			DanceEventUITransformController.SetUITransform(Context);			

			// RequestContext, Number of pose in sequence, Staritng limb for input controller
			DanceEventManager.ConfigureDanceEventInternal(
					Context, 
					CurrentSequencePoseIndex, 
					Context.DesiredPoseOrders.ElementAt(CurrentSequencePoseIndex).LimbRotationOrder.ElementAt(0));
			
			// Initialize components and game object to off
			DanceEventManager.enabled = false;
			DanceInputController.enabled = false;
			DanceEventUITransformController.enabled = false;

			// if this is a battle, disable the UI components
			if (Context.Environment == Environment.BattleDance)
			{
				BattleEventUIComponents.SetActive(false);
			}

			// Disable dance event ui to allow for delayed start
			DanceEventUI.SetActive(false);
		}
		
		void DisableUnwantedChildren()
		{
			if (Context.Environment == Environment.BattleDance)
			{
				EnvDanceUI.SetActive(false);
				// Turn off battle dance UI components, but do not diable battle dance UI canvas - canvas holds logic for the battle
				BattleEventUIComponents.SetActive(false);	
			}
			else if (Context.Environment == Environment.EnvDance)
			{
				BattleDanceUI.SetActive(false);
			}
		}
    }
}
