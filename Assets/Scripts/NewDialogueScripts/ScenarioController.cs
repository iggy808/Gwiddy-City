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
	InteractTextController TextController;

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


	// Initialize game in tutorial scenario - movement locked and speaking to old man
	void Awake()
	{
		Scenario startingScenario = new Scenario(ScenarioType.TutorialOldMan);
		Debug.Log("Awakening scenario...");
		Debug.Log("Oldman remake object : " + OldManTextPrompt.transform.parent.transform.gameObject.name);
		Debug.Log("Object w interact text controller : ");
		TextController = OldManTextPrompt.transform.parent.transform.gameObject.GetComponent<InteractTextController>();
		PlayerMovement = Player.GetComponent<PlayerMovement>();

		InitializeScenario(startingScenario);
	}

	// General initialize function for future scenarios
	public void InitializeScenario(Scenario scenario)
	{
		TextController.ActivateInteractText();
		CurrentScenario = scenario;
		CurrentState = CurrentScenario.States.First();
		PlayerMovement.enabled = false;
		CurrentProgressionStage = 0;
		CurrentDialogueInteractionCount = 0;
		ScenarioTotalInteractionCount = CurrentScenario.DialogueInteractionCount;	

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
						//OldManTextPrompt.SetActive(false);
						TextController.DisableInteractText();
						break;
					case ScenarioStates.DanceEventTutorial:
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
						//OldManTextPrompt.SetActive(true);
						TextController.ActivateInteractText();
						break;
					case ScenarioStates.InteractedTwice:
						// Disable interact text above old man head
						//OldManTextPrompt.SetActive(false);
						TextController.DisableInteractText();
						break;
					case ScenarioStates.Over:
						// PUFF OF SMOKE ETC.
						ParticleHandler.PUFF_O_SMOKE();
						Debug.Log("OLD MAN GONE, PLAYER CAN MOVE!");
						OldManObject.SetActive(false);
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
			Debug.Log("Scenario is over.");
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
