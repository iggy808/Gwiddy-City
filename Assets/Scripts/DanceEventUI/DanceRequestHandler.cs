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
		ScenarioController ScenarioController;
		[SerializeField]
		TutorialBridgeController TutorialBridgeController;
		[SerializeField]
		DanceRequestSender DanceSender;

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
		[SerializeField]
		PlayerMovement PlayerMovement;

		GameObject DanceEventUI;
        DanceEventManager DanceEventManager;
        DanceEvent.InputController DanceInputController;	
		DanceEventUITransformController DanceEventUITransformController;
		List<Pose> DanceSequence;
		bool IsSequenceEvent;
		bool IsEnemyTurn;

		int SequenceCoolness;
		int SequenceStaminaCost;

		void Start()
		{
			BattleDanceUI.SetActive(false);
			EnvDanceUI.SetActive(false);
		}

		public void ActivateDanceEvent(DanceRequestContext context, bool IsEnemyTurn = false)
		{
			Context = context;
			if (!IsEventActive)
			{
				IsEventActive = true;
				CurrentSequencePoseIndex = 0;
				SequenceCoolness = 0;
				SequenceStaminaCost = 0;
				if (BattleHandler.BattleIsActive && IsEnemyTurn)
				{

				}
				AssignUIAndManagers();	
				DisableUnwantedChildren();	
				ConfigureQuicktimeEvent(IsEnemyTurn);	
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
			//Debug.Log("Dance event triggered");
			StartCoroutine(DelayQuicktimeEnable());
        }

        IEnumerator DelayQuicktimeEnable()
        {
            yield return new WaitForSeconds(0.5f);
            DanceEventUI.SetActive(true);
            DanceEventManager.enabled = true;
			DanceInputController.enabled = true;
			DanceEventUITransformController.enabled = true;
			PlayerMovement.engaged = true;
			//Debug.Log("Dance event enabled.");
        }

        IEnumerator DelayQuicktimeDisable(bool wasSuccessful)
        {
            yield return new WaitForSeconds(0.5f);
			// If not sequence, disable dance event UI, handle battle single move case
			if (!IsSequenceEvent)//|| CurrentSequencePoseIndex >= Context.DesiredMoves.Count)
			{
				Debug.Log("Went through !IsSequenceEvent block.");
            	DanceEventManager.enabled = false;
				DanceInputController.enabled = false;
				DanceEventUITransformController.enabled = false;
            	DanceEventUI.SetActive(false);
				PlayerMovement.engaged = false;
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
				// If this is the tutorial dance event, ignore remaining handling, progress scenario, and break
				if (Context.IsTutorial)
				{
					Debug.Log("Dance Event Handler : ProgressingScenarion, attempting yield break.");
					ScenarioController.ProgressScenario();
					yield break;
				}

				// If this is a battle sequence event, handle accordingly
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

				// If this is an enevironmental dance event, handle accordingly
				if (Context.Environment == Environment.EnvDance)
				{	
					Debug.Log("Dance handler - collider component name : " + Context.TargetObject.transform.GetChild(0).name);
					// All environmental dances have colliders that hold a dance interactor component
					// DanceEventTrigger object w/ interactor component is always the first child of the environmental object
					if (Context.TargetObject.transform.GetChild(0).TryGetComponent<DanceInteractor>(out DanceInteractor danceInteractor))
					{
						GameObject colliderObject = Context.TargetObject.transform.GetChild(0).gameObject;
						// Disable event trigger - gives enemies permanence 
						Debug.Log("Dance handler allowing interactor processing in the dance sender");
						DanceSender.AllowForInteractorProcessing();
						if (danceInteractor.Type == InteractorType.EnvEnemy)
						{
							if (wasSuccessful)
							{
								colliderObject.SetActive(false);
							}
							else
							{
								//DanceSender.AllowForInteractorProcessing();
							}

							Debug.Log("Dance handler handling environmental enemy victory.");
							// Note: nothing right now, could spice up later
						}
						else if (danceInteractor.Type == InteractorType.TutorialBridge1 || danceInteractor.Type == InteractorType.TutorialBridge2)
						{
							if (wasSuccessful)
							{
								Debug.Log("Dance handler handling tutorial bridge victory.");
								colliderObject.SetActive(false);
								TutorialBridgeController.RaiseBridge(danceInteractor.Type);
							}
							else
							{
								//DanceSender.AllowForInteractorProcessing();	
							}
						}
					}
					else
					{
						Debug.Log("BIG PROBLEM BIG PROBLEM BIG PROBLEM BIG PROBLEM DanceHandler - danceInteractor not found");
					}

					DanceSender.AllowForInteractorProcessing();
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

		void ConfigureQuicktimeEvent(bool IsEnemyTurn = false)
		{	
			// Enable object for configuration
			DanceEventUI.SetActive(true);
			
			// Configure components to dance request context
			DanceEventUITransformController.SetUITransform(Context);			

			// RequestContext, Number of pose in sequence, Staritng limb for input controller
			DanceEventManager.ConfigureDanceEventInternal(
					Context, 
					CurrentSequencePoseIndex, 
					Context.DesiredPoseOrders.ElementAt(CurrentSequencePoseIndex).LimbRotationOrder.ElementAt(0),
					IsEnemyTurn);
			
			// Initialize components and game object to off
			DanceEventManager.enabled = false;
			DanceInputController.enabled = false;
			DanceEventUITransformController.enabled = false;

			// Disable dance event ui to allow for delayed start
			DanceEventUI.SetActive(false);
		}
		
		void DisableUnwantedChildren()
		{
			if (Context.Environment == Environment.BattleDance)
			{
				EnvDanceUI.SetActive(false);
				// Turn off battle dance UI components, but do not diable battle dance UI canvas - canvas holds logic for the battle
				//BattleEventUIComponents.SetActive(false);	
				//BattleManager.BattleUIManager.HideInputPanel();
				//BattleManager.BattleUIManager.ShowBattleStats();
			}
			else if (Context.Environment == Environment.EnvDance)
			{
				BattleDanceUI.SetActive(false);
			}
		}
    }
}
