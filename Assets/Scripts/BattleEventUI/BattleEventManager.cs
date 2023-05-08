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


		[SerializeField]
		GameObject PlayerController;
		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		BattleEventUIManager BattleUIManager;

		BattleRequestContext Context;
		SpecialEnemies CurrentEnemy;

		public int CurrentSequencePoseIndex;

		public void InitializeBattleEvent(BattleRequestContext context)
		{
			// Assign battle context to the initial battle request context
			Context = context;
			CurrentEnemy = context.Enemy.Name;
			EnemyCurrentStamina = context.Enemy.MaxStamina;

			// Fetch the current player stats from the player controller
			Context.Player = PlayerController.GetComponent<PlayerStats>();
			PlayerCurrentStamina = Context.Player.CurrentStamina;

			// Initialize the battle stats with the freshly fetched stats
			BattleUIManager.InitializeBattleUI(Context);
			BattleUIManager.InitializeBattleStats(Context);
		}

		public void HandleSequenceStats(int sequenceDamage)
		{
			// Need to handle coolness here as well
			EnemyCurrentStamina -= sequenceDamage;
			if (EnemyCurrentStamina <= 0)
			{
				EndBattle();
			}
			else
			{
				Debug.Log("Showing battle main menu");
				BattleUIManager.ShowMainMenu();
				UpdateBattleContext();
				BattleUIManager.UpdateStaminaStats(Context);
			}
		}

		// Track changes to player stats separately from the component
		// Track in the battle context, and update the player stats accordingly after battle
		public void UpdateBattleContext()
		{
			Context.Enemy.CurrentStamina = EnemyCurrentStamina;
			Context.Player.CurrentStamina = PlayerCurrentStamina;
		}

		public int GetPoseDamage(DanceEvent.Pose pose)
		{
			int poseDamage;

			switch (pose)
			{
				case DanceEvent.Pose.Splits:
					poseDamage = 1;
					break;
				case DanceEvent.Pose.Cool:
					poseDamage = 2;
					break;
				default:
					poseDamage = 0;
					break;
			}

			return poseDamage;
		}

		public void EndBattle()
		{
				BattleHandler.EndBattleEvent();
		}
	}
}
