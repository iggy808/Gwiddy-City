using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DanceEvent;


public class ScenarioController : MonoBehaviour
{
	public Scenario CurrentScenario;
	public ScenarioStates CurrentState;
	public int CurrentDialogueInteractionCount;
	public int ScenarioTotalInteractionCount;
	int CurrentProgressionStage;

	[SerializeField]
	DanceRequestSender DanceSender;
	[SerializeField]
	animationStateController AnimationInputController;


	[SerializeField]
	GameObject Player;
	PlayerMovement PlayerMovement;
	[SerializeField]
	GameObject OldManObject;
	[SerializeField]
	GameObject OldManTextPrompt;
	[SerializeField]
	DanceRequestHandler DanceHandler;
	[SerializeField]
	ParticleHandler ParticleHandler;

	public bool IsScenarioActive;
	public bool INSP_CANCELINTROSCENARIO;

	// Initialize game in tutorial scenario - movement locked and speaking to old man
	void Awake()
	{
		/*
		if (INSP_CANCELINTROSCENARIO)
		{
			AnimationInputController.enabled = true;
			return;
		}
		*/

		Scenario startingScenario = new Scenario(ScenarioType.TutorialOldMan);
		PlayerMovement = Player.GetComponent<PlayerMovement>();
		// Throughout the intro, disable the general dance event sender
		// Or control with IsSCenarioActive?
		//DanceSender.enabled = false;

		InitializeScenario(startingScenario);
	}

	// General initialize function for future scenarios
	public void InitializeScenario(Scenario scenario)
	{
		// Configure variables for internal use during scenario
		IsScenarioActive = true;
		CurrentScenario = scenario;
		CurrentState = CurrentScenario.States.First();
		CurrentProgressionStage = 0;
		CurrentDialogueInteractionCount = 0;
		ScenarioTotalInteractionCount = CurrentScenario.DialogueInteractionCount;	
		
		// Restrict player movement and animation at beginning of scenario
		PlayerMovement.enabled = false;
		//AnimationInputController.enabled = false;

		Debug.Log("Scenario initialized, CurrentState = " + CurrentState + ", CurrentProgressionStage = " + CurrentProgressionStage);
	}

	void ProgressState() 
	{
		switch (CurrentScenario.Type)
		{
			case ScenarioType.TutorialOldMan:
				switch (CurrentState)
				{
					case ScenarioStates.InteractedOnce:
						OldManTextPrompt.SetActive(false);
						break;
					case ScenarioStates.DanceEventTutorial:
						AnimationInputController.enabled = true;
						// Activate a dance event with tutorial intructions and long timer
						DanceHandler.ActivateDanceEvent(new DanceRequestContext
						{
							Environment = Environment.EnvDance,
							DesiredMoves = new List<DanceEvent.Pose>()
							{
								DanceEvent.Pose.Splits
							},
							TargetObject = OldManObject,
							IsTutorial = true
						});
						break;
					case ScenarioStates.DanceEventTutorialOver:
						// Enable interact text above old man head
						OldManTextPrompt.SetActive(true);
						break;
					case ScenarioStates.InteractedTwice:
						// Disable interact text above old man head
						OldManTextPrompt.SetActive(false);
						break;
					case ScenarioStates.Over:
						// PUFF OF SMOKE ETC.
						ParticleHandler.PUFF_O_SMOKE();
						Debug.Log("OLD MAN GONE, PLAYER CAN MOVE!");
						OldManObject.SetActive(false);
						// Enable the general environmental dance event sender
						//DanceSender.enabled = true;
						IsScenarioActive = false;
						break;
				}
				break;
			default:
				break;
		}

	}

	public void ProgressScenario()
	{
		CurrentProgressionStage++;

		if (CurrentProgressionStage > CurrentScenario.States.Count)
		{
			CurrentState = ScenarioStates.Over;
			Debug.Log("Out of bounds - oopsie was made somewhere.");
			return;
		}
		else
		{
			Debug.Log("Scenario progressed, current progression stage : " + CurrentProgressionStage);
			CurrentState = CurrentScenario.States.ElementAt(CurrentProgressionStage);
			Debug.Log("Scenario progressed, current state : " + CurrentState);
			ProgressState();
		}
	}
}
