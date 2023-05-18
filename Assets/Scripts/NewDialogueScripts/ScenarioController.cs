using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DanceEvent;
using BattleEvent;


public class ScenarioController : MonoBehaviour
{
	public Scenario CurrentScenario;
	public ScenarioStates CurrentState;
	public int CurrentDialogueInteractionCount;
	public int ScenarioTotalInteractionCount;
	public int CurrentProgressionStage;

	[SerializeField]
	BattleRequestHandler BattleHandler;

	[SerializeField]
	DanceRequestSender DanceSender;
	[SerializeField]
	animationStateController AnimationInputController;


	[SerializeField]
	GameObject Player;
	[SerializeField]
	PlayerStats PlayerStats;
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
	Rigidbody rb;

	public Scenario startingScenario;
	public Scenario secondScenario;
	public DialogueManager DialogueManagerAccessor;
	// Initialize game in tutorial scenario - movement locked and speaking to old man
	void Awake()
	{
		
		if (INSP_CANCELINTROSCENARIO)
		{
			AnimationInputController.enabled = true;
			IsScenarioActive = false;
			return;
		}

		rb = Player.GetComponent<Rigidbody>();
		startingScenario = new Scenario(ScenarioType.TutorialOldMan);
        secondScenario = new Scenario(ScenarioType.TutorialOldMan2);
		PlayerMovement = Player.GetComponent<PlayerMovement>();
		// Throughout the intro, disable the general dance event sender
		// Or control with IsSCenarioActive?
		//DanceSender.enabled = false;

		Debug.Log("Awake called");
		InitializeScenario(startingScenario);
	}

	// General initialize function for future scenarios
	public void InitializeScenario(Scenario scenario)
	{
		// Configure variables for internal use during scenario
		IsScenarioActive = true;
		CurrentScenario = scenario;
		Debug.Log("Current scenario : " + CurrentScenario.Type);
		CurrentState = CurrentScenario.States.First();
		CurrentProgressionStage = 0;
		CurrentDialogueInteractionCount = 0;
		ScenarioTotalInteractionCount = CurrentScenario.DialogueInteractionCount;	
		
		// Restrict player movement and animation at beginning of scenario
		PlayerMovement.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        //AnimationInputController.enabled = false;
		
        Debug.Log("Scenario initialized, CurrentState = " + CurrentState + ", CurrentProgressionStage = " + CurrentProgressionStage);
	}

	void ProgressState() 
	{
		Debug.Log("Progressing state.");
		switch (CurrentScenario.Type)
		{	
			case ScenarioType.TutorialOldMan:
				switch (CurrentState)
				{
					case ScenarioStates.InteractedOnce:
						OldManTextPrompt.SetActive(false);
						Debug.Log("Made it Here");
						DialogueManagerAccessor.OldManTalks(ScenarioType.TutorialOldMan, ScenarioStates.InteractedOnce);
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
                        DialogueManagerAccessor.OldManTalks(ScenarioType.TutorialOldMan, ScenarioStates.InteractedTwice);
                        break;
					case ScenarioStates.Over:
						// PUFF OF SMOKE ETC.
						ParticleHandler.PUFF_O_SMOKE();
						//Debug.Log("OLD MAN GONE, PLAYER CAN MOVE! (sike)");
						//OldManObject.transform.position = new Vector3(-221.42f, 16.56f, -131.58f);
						//OldManObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 126.9f, 0f));
                        // Enable the general environmental dance event sender
                        //DanceSender.enabled = true;
						OldManTextPrompt.SetActive(false);
						PlayerMovement.enabled = true;
                        IsScenarioActive = false;
                        OldManObject.transform.position = new Vector3(0f, 16.56f, -131.58f);
                        OldManObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 126.9f, 0f));
						//StartCoroutine(DelayInitializeScenario(ScenarioType.TutorialOldMan2));
						break;
				}
				break;
            case ScenarioType.TutorialOldMan2:
                switch (CurrentState)
                {
                    case ScenarioStates.InteractedOnce:
                        OldManTextPrompt.SetActive(false);
                        //Debug.Log("Made it Here");
                        DialogueManagerAccessor.OldManTalks(ScenarioType.TutorialOldMan2, ScenarioStates.InteractedOnce);
                        break;
                    case ScenarioStates.DanceEventTutorial:
                        AnimationInputController.enabled = true;
						BattleHandler.ActivateBattleEvent(new BattleRequestContext()
						{
							Enemy = new Enemy(SpecialEnemies.OldManTut),
							Player = PlayerStats
						});
						/*
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
						*/
                        break;
                    case ScenarioStates.DanceEventTutorialOver:
                        // Enable interact text above old man head
                        OldManTextPrompt.SetActive(true);
                        break;
                    case ScenarioStates.InteractedTwice:
                        // Disable interact text above old man head
                        OldManTextPrompt.SetActive(false);
                        DialogueManagerAccessor.OldManTalks(ScenarioType.TutorialOldMan2, ScenarioStates.InteractedTwice);
                        break;
                    case ScenarioStates.Over:
                        // PUFF OF SMOKE ETC.
                        ParticleHandler.PUFF_O_SMOKE();
                        Debug.Log("OLD MAN GONE, PLAYER CAN MOVE!");
						PlayerMovement.enabled = true;
                        // Enable the general environmental dance event sender
                        //DanceSender.enabled = true;
                        IsScenarioActive = false;
                        OldManObject.transform.position = new Vector3(-221.42f, 16.56f, -131.58f);
                        OldManObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 126.9f, 0f));

                        break;
                }
				break;
            default:
				break;
		}

	}
	IEnumerator DelayInitializeScenario(ScenarioType scenarioType)
	{
		yield return new WaitForSeconds(0.5f);
		InitializeScenario(new Scenario(scenarioType));
	}

	public void ProgressScenario()
	{
		if (!IsScenarioActive)
		{
			return;
		}
		CurrentProgressionStage++;
		Debug.Log("Progressing scenario...");

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

    private void Update()
    {
		Debug.Log("Current Scenario " + CurrentScenario);
		Debug.Log("Current Scenerio State " + CurrentState);
    }
}
