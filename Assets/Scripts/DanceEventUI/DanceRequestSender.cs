using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using BattleEvent;

namespace DanceEvent
{
	public class DanceRequestSender : MonoBehaviour
	{
		[SerializeField]
		DanceRequestHandler DanceHandler;
		[SerializeField]
		PlayerDances PlayerDances;
		[SerializeField]
		InteractTextController CurrentTextController;
		
		public GameObject CurrentlyProcessingInteractor;
		public InteractorType CurrentlyProcessingInteractorType;

		bool IsProcessing;
		bool DanceActive;
		PlayerInteract PlayerInteract;

		void Start()
		{
			AllowForInteractorProcessing();
			PlayerInteract = GetComponent<PlayerInteract>();
		}

		public void AllowForInteractorProcessing()
		{
			Debug.Log("Resetting interactor processing");
			CurrentlyProcessingInteractor = null;
			CurrentlyProcessingInteractorType = InteractorType.None;
			CurrentTextController = null;
			IsProcessing = false;
			DanceActive = false;
		}

		void AssignCurrentlyProcessingInteractor(DanceInteractor danceInteractor)
		{
			Debug.Log("Assigning interactor, danceInteractor gameObject name : " + danceInteractor.gameObject.name);
			if (danceInteractor.Type == InteractorType.TutorialBridge)
			{	
				Debug.Log("Assigning interactor, danceInteractor gameObject name (should be eventtrigger): " + danceInteractor.gameObject.name);
			}
			//Debug.Log("Interactor collider's parent object name : " + danceInteractor.transform.parent.transform.gameObject.name);
			CurrentTextController = danceInteractor.transform.parent.transform.gameObject.GetComponent<InteractTextController>();
			CurrentlyProcessingInteractor = danceInteractor.gameObject;
			CurrentlyProcessingInteractorType = danceInteractor.Type;
			IsProcessing = true;
		}



		void OnTriggerStay(Collider collider)
		{
			Debug.Log("Player is currently inside of : " + collider.gameObject.name);

			if (collider.gameObject.TryGetComponent(out DanceInteractor danceInteractor))
			{		
				if (CurrentlyProcessingInteractor == null)
				{	
					AssignCurrentlyProcessingInteractor(danceInteractor);
				}

				switch (CurrentlyProcessingInteractorType)
				{
					/*
					case InteractorType.OldManTutorial:
						CurrentTextController.ActivateInteractText();
						break;
						*/


					case InteractorType.EnvEnemy:
						Debug.Log("Collider object type : " + danceInteractor.Type);
						if (!DanceActive)
						{
							DanceActive = true;
							DanceHandler.ActivateDanceEvent(new DanceRequestContext
							{
								Environment = Environment.EnvDance,
								DesiredMoves = new List<DanceEvent.Pose>() {PlayerDances.Dances.ElementAt(0)},
								TargetObject = collider.transform.parent.transform.gameObject
							});
						}
						break;

					case InteractorType.TutorialBridge:
						Debug.Log("Collider object type : " + danceInteractor.Type);
						// Activate interact text and wait for interact button press...
						Debug.Log("Calling ActivateInteractText from DanceRequestSender");
						CurrentTextController.ActivateInteractText();
						Debug.Log("Waiting for interact key");
						if (PlayerInteract.InteractKeyPressed)
						{
							Debug.Log("Interaact key pressed");
							Debug.Log("Dance active? : " + DanceActive);
							/*
							if (!DanceActive)
							{
							*/
								DanceActive = true;
								CurrentTextController.DisableInteractText();
								Debug.Log("DanceRequestSender logs an interact key press. Triggering the dance event");
								DanceHandler.ActivateDanceSequenceEvent(new DanceRequestContext 
								{
									Environment = Environment.EnvDance,
									DesiredMoves = new List<DanceEvent.Pose>()
									{
										Pose.Splits,
										Pose.Cool
									},
									//TargetObject = collider.gameObject.transform.parent.gameObject
									TargetObject = collider.gameObject
								});
								/*
							}
						*/
						}
						break;

					default:
						break;
				}
			}
			else
			{
				Debug.Log("Dont got a danceInteractor");
			}
		}

		void OnTriggerExit(Collider collider)
		{
			Debug.Log("Trigger exited");
			if (CurrentTextController != null && !DanceActive)
			{
				Debug.Log("Left trigger, allowing for interactor processing");
				AllowForInteractorProcessing();
			}
		}
	}

}
