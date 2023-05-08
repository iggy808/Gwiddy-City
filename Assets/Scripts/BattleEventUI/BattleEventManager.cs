using UnityEngine;
using TMPro;

namespace BattleEvent
{
	// Will control the turnbased battle's current state (stamina, dances, whateva)
	public class BattleEventManager : MonoBehaviour
	{
		public int CurrentPoseIndex;
		public int EnemyCurrentStamina;
		public int PlayerCurrentStamina;

		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		BattleEventUIManager BattleUIManager;

		BattleRequestContext Context;
		SpecialEnemies CurrentEnemy;

		public int CurrentSequencePoseIndex;

		public void InitializeBattleEvent(BattleRequestContext context)
		{
			Context = context;
			CurrentEnemy = context.Enemy.Name;
			EnemyCurrentStamina = context.Enemy.MaxStamina;

			// Might need to take in a player object
			BattleUIManager.InitializeStaminaStats(context.Player, context.Enemy);
		}

		public void HandleSequenceStats(int sequenceDamage)
		{
			EnemyCurrentStamina -= sequenceDamage;
			if (EnemyCurrentStamina <= 0)
			{
				EndBattle();
			}
			else
			{
				Debug.Log("Showing battle main menu");
				BattleUIManager.ShowMainMenu();
				BattleUIManager.UpdateStaminaStats(PlayerCurrentStamina, EnemyCurrentStamina);
			}
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
