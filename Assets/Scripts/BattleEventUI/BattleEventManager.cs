using UnityEngine;
using DanceEvent;
using System.Collections.Generic;
using System.Collections;
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
		public BattleEventUIManager BattleUIManager;

		[SerializeField]
		GameObject PlayerController;
		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		SequencerUIManager SequencerUIManager;
		[SerializeField]
		DanceRequestHandler DanceHandler;

		SpecialEnemies CurrentEnemy;
		bool WasSuccessful;
		int CoolnessLeadWinThreshhold;
		List<DanceEvent.Pose> SequencerPoses;
		DanceModifier CurrentDanceModifier;
		
		int CurrentSequencerIndex;
		int CurrentSequencerStaminaCost;


		public void InitializeBattleEvent(BattleRequestContext context)
		{
			WasSuccessful = false;
			CurrentTurn = BattleTurn.Player;
			// Assign battle context to the initial battle request context
			Context = context;
			CurrentEnemy = context.Enemy.Name;
			EnemyCurrentStamina = context.Enemy.MaxStamina;

			// Tell the player they are in a battle
			PlayerEngaged engaged = PlayerController.GetComponent<PlayerEngaged>();
			engaged.battleEngaged = true;

			// Fetch the current player stats from the player controller
			Context.Player = PlayerController.GetComponent<PlayerStats>();
			PlayerCurrentStamina = Context.Player.CurrentStamina;

			EnemyCurrentCoolness = 0;
			PlayerCurrentCoolness = 0;
			CoolnessLeadWinThreshhold = context.Enemy.CoolnessThreshhold;

			SequencerPoses = new List<DanceEvent.Pose>();
			CurrentSequencerIndex = 0;
			CurrentSequencerStaminaCost = 0;
			SequencerUIManager.SequencerIcons = new List<GameObject>();

			// Initialize the battle stats with the freshly fetched stats
			BattleUIManager.InitializeBattleUI(Context);
		}

		public void InitializeSequencerState()
		{
			// Can let player earn more sequencer slots as an upgrade
			SequencerPoses = new List<DanceEvent.Pose>();
			CurrentSequencerIndex = 0;
			CurrentSequencerStaminaCost = 0;
		}

		public void PlayEnemyTurn()
		{
			Debug.Log("Playing enemy turn.");
			bool IsEnemyTurn = true;
			if (EnemyCurrentStamina >= GetPoseStaminaCost(DanceEvent.Pose.Splits))
			{
				Debug.Log("Enemy has enough stamina, doing the splits.");
				TriggerOneOffDanceEvent(DanceEvent.Pose.Splits, IsEnemyTurn);
			}
			else 
			{
				Debug.Log("Enemy does not have enough stamina, resting.");
				RestTurn();
				IsEnemyTurn = false;

				// Will need to augment this later
				BattleUIManager.UpdateBattleStats();
				BattleUIManager.ShowInputPanel();
				BattleUIManager.ShowMainMenu();	
			}

			// if not enough stamina to do any poses,
			// Rest
		}

		public void HandleSequenceStats(int sequenceCoolness, int sequenceStaminaCost)
		{
			// Apply stat buffs to one shot moves
			if (CurrentDanceModifier != null)
			{
				if (CurrentDanceModifier.CoolnessModifier != 0)
				{
					sequenceCoolness *= CurrentDanceModifier.CoolnessModifier;	
				}
				sequenceStaminaCost -= CurrentDanceModifier.StaminaReplenish;
				if (CurrentTurn == BattleTurn.Player)
				{
					if (EnemyCurrentCoolness - CurrentDanceModifier.EnemyEmbarassment < 0)
					{
						EnemyCurrentCoolness = 0;
					}
					else
					{	
						EnemyCurrentCoolness -= CurrentDanceModifier.EnemyEmbarassment;
					}
				}
				else if (CurrentTurn == BattleTurn.Enemy)
				{
					if (PlayerCurrentCoolness - CurrentDanceModifier.EnemyEmbarassment < 0)
					{
						PlayerCurrentCoolness = 0;
					}
					else
					{
						PlayerCurrentCoolness -= CurrentDanceModifier.EnemyEmbarassment;
					}
				}
				
			}
			// Track correct stats according to current turn, switch turns afterwards
			if (CurrentTurn == BattleTurn.Player)
			{
				PlayerCurrentCoolness += sequenceCoolness;
				PlayerCurrentStamina -= sequenceStaminaCost;
			}
			else if (CurrentTurn == BattleTurn.Enemy)
			{	
				EnemyCurrentCoolness += sequenceCoolness;
				EnemyCurrentStamina -= sequenceStaminaCost;
			}	

			// If both dancers run out of stamina, or if one dancer leads the other by 30 coolness,
			// end the battle
			if ((PlayerCurrentCoolness - CoolnessLeadWinThreshhold > EnemyCurrentCoolness 
				 || EnemyCurrentCoolness - CoolnessLeadWinThreshhold >= PlayerCurrentCoolness))
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
				CurrentDanceModifier = null;
				EndBattle();
			}
			else
			{
				if (CurrentTurn == BattleTurn.Player)
				{
					CurrentDanceModifier = null;
					Debug.Log("Player turn is over switching to enemy.");
					CurrentTurn = BattleTurn.Enemy;
					Debug.Log("Hiding player battle stats\n Hiding InputPanel\n Showing EnemyBattleStats, UpdatingBattleStats");
					// other indications of this being the enemy turn in the ui, etc
					BattleUIManager.HideInputPanel();
					BattleUIManager.ShowBattleStats();
					BattleUIManager.UpdateBattleStats();

					// After player turn is over, begin enemy turn
					StartCoroutine(DelayBeginEnemyTurn());
				}
				else if (CurrentTurn == BattleTurn.Enemy)
				{
					CurrentDanceModifier = null;
					Debug.Log("Enemy turn is over, switching to player.");
					CurrentTurn = BattleTurn.Player;
					Debug.Log("Showing enemy+player BattleStats\n Updating battile stats\nShowing Input panel\n Showing Main menu");
					BattleUIManager.ShowInputPanel();
					BattleUIManager.ShowBattleStats();
					BattleUIManager.UpdateBattleStats();
					BattleUIManager.ShowMainMenu();
				}
			}
		}
		
		IEnumerator DelayBeginEnemyTurn()
		{
			yield return new WaitForSeconds(1f);
			PlayEnemyTurn();
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
				case DanceEvent.Pose.Sick:
					poseCoolness = 15;
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
				case DanceEvent.Pose.Sick:
					poseStaminaCost = 3;
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

				if (CurrentSequencerStaminaCost + GetPoseStaminaCost(pose) > PlayerCurrentStamina)
				{
					Debug.Log("Player does not have enough stamina to add [" + pose + "] to sequencer.");	
				}
				else
				{
					SequencerPoses.Add(pose);
					SequencerUIManager.AddPoseIconToSequencer(CurrentSequencerIndex, pose);
					CurrentSequencerIndex++;
					CurrentSequencerStaminaCost += GetPoseStaminaCost(pose);
				}
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

				BattleUIManager.HideInputPanel();
				BattleUIManager.ShowBattleStats();

				DanceHandler.ActivateDanceSequenceEvent(new DanceRequestContext()
				{
					Environment = Environment.BattleDance,
					DesiredMoves = SequencerPoses,
				});	
			}
		}

		public void TriggerOneOffDanceEvent(DanceEvent.Pose pose, bool IsEnemyTurn = false)
		{
			BattleUIManager.HideInputPanel();
			Debug.Log("Triggering one off dance, hiding stats respectively.");

			BattleUIManager.ShowBattleStats();
			CurrentDanceModifier = new DanceModifier(pose);

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

		public void RestTurn()
		{
			if (CurrentTurn == BattleTurn.Player)
			{
				if (PlayerCurrentStamina + 10 > Context.Player.MaxStamina)
				{
					PlayerCurrentStamina = Context.Player.MaxStamina;
				}
				else 
				{
					PlayerCurrentStamina += 10;
				}
				BattleUIManager.UpdateBattleStats();
				CurrentTurn = BattleTurn.Enemy;
				PlayEnemyTurn();
			}
			else if (CurrentTurn == BattleTurn.Enemy)
			{
				if (EnemyCurrentStamina + 10 > Context.Enemy.MaxStamina)
				{
					EnemyCurrentStamina = Context.Enemy.MaxStamina;
				}
				else
				{
					EnemyCurrentStamina += 10;
				}

				CurrentTurn = BattleTurn.Player;
			}
		}

		public void EndBattle()
		{
			PlayerEngaged engaged = PlayerController.GetComponent<PlayerEngaged>();
			engaged.battleEngaged = false;
			BattleHandler.EndBattleEvent(WasSuccessful);
		}
	}
}
