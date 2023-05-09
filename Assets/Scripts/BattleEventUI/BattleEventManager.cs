using UnityEngine;
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

		public BattleTurn CurrentTurn;


		[SerializeField]
		GameObject PlayerController;
		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		BattleEventUIManager BattleUIManager;

		BattleRequestContext Context;
		SpecialEnemies CurrentEnemy;

		bool WasSuccessful;
		int CoolnessLeadWinThreshhold;


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

			// Initialize the battle stats with the freshly fetched stats
			BattleUIManager.InitializeBattleUI(Context);
		}

		public void HandleSequenceStats(int sequenceCoolness, int sequenceStaminaCost)
		{
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
			if ((PlayerCurrentCoolness >= EnemyCurrentCoolness + CoolnessLeadWinThreshhold 
				 || EnemyCurrentCoolness >= PlayerCurrentCoolness + CoolnessLeadWinThreshhold) 
				 || (EnemyCurrentStamina <= 0 && PlayerCurrentStamina <= 0))
			{
				// If player is cooler, player wins
				if (PlayerCurrentCoolness > EnemyCurrentCoolness)
				{
					WasSuccessful = true;
				}
				else if (PlayerCurrentCoolness == EnemyCurrentCoolness)
				{
					Debug.Log("Tiebreaker! Dif minigame would be cool.");
					Debug.Log("For now, default to player victory.");
					WasSuccessful = true;
				}
				else
				{
					WasSuccessful = false;
				}

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
					Debug.Log("Enemy turn, displaying UI for enemy");
					BattleUIManager.ShowMainMenu();
					BattleUIManager.UpdateBattleStats();
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

		public void EndBattle()
		{
			BattleHandler.EndBattleEvent(WasSuccessful);
		}
	}
}
