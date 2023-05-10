using UnityEngine;
using DanceEvent;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace BattleEvent
{
	// Will control the turnbased battle's current state (stamina, dances, whateva)
	public class BattleEventManager : MonoBehaviour
	{
		public int CurrentPoseIndex;
		public int EnemyCurrentStamina;
		public int EnemyCurrentCoolness;
		public int PlayerCurrentStamina;
		public int PlayerCurrentCoolness;
		public int CurrentSequencePoseIndex;
		public BattleRequestContext Context;
		public BattleTurn CurrentTurn;

		[SerializeField]
		GameObject PlayerController;
		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		BattleEventUIManager BattleUIManager;
		[SerializeField]
		SequencerUIManager SequencerUIManager;
		[SerializeField]
		DanceRequestHandler DanceHandler;

		SpecialEnemies CurrentEnemy;
		bool WasSuccessful;
		int CoolnessLeadWinThreshhold;
		List<DanceEvent.Pose> SequencerPoses;
		int CurrentSequencerIndex;


		public void InitializeBattleEvent(BattleRequestContext context)
		{
			WasSuccessful = false;
			CurrentTurn = BattleTurn.Player;
			// Assign battle context to the initial battle request context
			Context = context;
			CurrentEnemy = context.Enemy.Name;
			EnemyCurrentStamina = context.Enemy.MaxStamina;

			// Fetch the current player stats from the player controller
			Context.Player = PlayerController.GetComponent<PlayerStats>();
			PlayerCurrentStamina = Context.Player.CurrentStamina;

			EnemyCurrentCoolness = 0;
			PlayerCurrentCoolness = 0;
			CoolnessLeadWinThreshhold = context.Enemy.CoolnessThreshhold;

			SequencerPoses = new List<DanceEvent.Pose>();
			CurrentSequencerIndex = 0;
			SequencerUIManager.SequencerIcons = new List<GameObject>();

			// Initialize the battle stats with the freshly fetched stats
			BattleUIManager.InitializeBattleUI(Context);
		}

		public void InitializeSequencerState()
		{
			// Can let player earn more sequencer slots as an upgrade
			SequencerPoses = new List<DanceEvent.Pose>();
			CurrentSequencerIndex = 0;
		}

		public void PlayEnemyTurn()
		{
			Debug.Log("Playing enemy turn.");
			bool IsEnemyTurn = true;
			TriggerOneOffDanceEvent(DanceEvent.Pose.Splits, IsEnemyTurn);
		}

		public void HandleSequenceStats(int sequenceCoolness, int sequenceStaminaCost)
		{
			// Track correct stats according to current turn, switch turns afterwards
			if (CurrentTurn == BattleTurn.Player)
			{
				PlayerCurrentCoolness += sequenceCoolness;
				PlayerCurrentStamina -= sequenceStaminaCost;
				CurrentTurn = BattleTurn.Enemy;
			}
			else if (CurrentTurn == BattleTurn.Enemy)
			{	
				EnemyCurrentCoolness += sequenceCoolness;
				EnemyCurrentStamina -= sequenceStaminaCost;
				CurrentTurn = BattleTurn.Player;
			}	

			// If both dancers run out of stamina, or if one dancer leads the other by 30 coolness,
			// end the battle
			if ((PlayerCurrentCoolness - CoolnessLeadWinThreshhold > EnemyCurrentCoolness 
				 || EnemyCurrentCoolness - CoolnessLeadWinThreshhold >= PlayerCurrentCoolness) 
				 || (EnemyCurrentStamina <= 0 && PlayerCurrentStamina <= 0))
			{
				if (EnemyCurrentStamina <= 0 && PlayerCurrentStamina <= 0)
				{
					 Debug.Log("Both dancers out of stamina.");
				}

				// If player is cooler, player wins
				if (PlayerCurrentCoolness > EnemyCurrentCoolness)
				{
					WasSuccessful = true;
				}
				else if (PlayerCurrentCoolness == EnemyCurrentCoolness)
				{
					Debug.Log("Tiebreaker! For now, default to player victory.");
					WasSuccessful = true;
				}
				else
				{
					WasSuccessful = false;
				}
				
				Debug.Log("Ending battle. Player cooler than '" + Context.Enemy.Name +"' ? : " + WasSuccessful);
				EndBattle();
			}
			else
			{
				if (CurrentTurn == BattleTurn.Player)
				{
					Debug.Log("Player turn, displaying UI for player");
					BattleUIManager.ShowMainMenu();
					BattleUIManager.UpdateBattleStats();
				}
				else if (CurrentTurn == BattleTurn.Enemy)
				{
					Debug.Log("Enemy turn, temporarily displaying manual UI.");
					PlayEnemyTurn();
					//BattleUIManager.ShowMainMenu();
					//BattleUIManager.UpdateBattleStats();
				}
			}
		}

		public int GetPoseCoolness(DanceEvent.Pose pose)
		{
			int poseCoolness;

			switch (pose)
			{
				case DanceEvent.Pose.Splits:
					poseCoolness = 5;
					break;
				case DanceEvent.Pose.Cool:
					poseCoolness = 10;
					break;
				default:
					poseCoolness = 0;
					break;
			}

			return poseCoolness;
		}

		public int GetPoseStaminaCost(DanceEvent.Pose pose)
		{
			int poseStaminaCost;
			
			switch (pose)
			{
				case DanceEvent.Pose.Splits:
					poseStaminaCost = 1;
					break;
				case DanceEvent.Pose.Cool:
					poseStaminaCost = 2;
					break;
				default:
					poseStaminaCost = 0;
					break;
			}

			return poseStaminaCost;
		}

		public void AddPoseToSequencer(DanceEvent.Pose pose)
		{
			if (CurrentSequencerIndex < Context.Player.SequencerSlots)
			{
				Debug.Log("Adding [" + pose + "] to sequencer.");	
				SequencerPoses.Add(pose);
				SequencerUIManager.AddPoseIconToSequencer(CurrentSequencerIndex, pose);
				CurrentSequencerIndex++;
			}
			else
			{
				Debug.Log("Sequencer is full! Cannot add [" + pose + "] to sequencer.");
			}
		}

		public void TriggerSequenceEvent()
		{
			if (SequencerPoses.Count > 0)
			{
				// Disable icons here
				SequencerUIManager.InitializeSequencerIcons();

				// Disable the sequencer menu buttons
				foreach (GameObject button in BattleUIManager.SequenceDanceButtons)
				{
					Destroy(button);
				}

				DanceHandler.ActivateDanceSequenceEvent(new DanceRequestContext()
				{
					Environment = Environment.BattleDance,
					DesiredMoves = SequencerPoses,
				});	
			}
		}

		public void TriggerOneOffDanceEvent(DanceEvent.Pose pose, bool IsEnemyTurn = false)
		{
			DanceHandler.ActivateDanceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMoves = new List<DanceEvent.Pose>()
				{
					pose
				}
			},
			IsEnemyTurn);
		}

		public void EndBattle()
		{
			BattleHandler.EndBattleEvent(WasSuccessful);
		}
	}
}
