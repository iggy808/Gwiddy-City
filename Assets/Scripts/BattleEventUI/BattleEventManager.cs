using UnityEngine;
using TMPro;

namespace BattleEvent
{
	// Will control the turnbased battle's current state (stamina, dances, whateva)
	public class BattleEventManager : MonoBehaviour
	{
		public int CurrentPoseIndex;

		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		BattleEventUIManager BattleUIManager;
		[SerializeField]
		TextMeshProUGUI EnemyStaminaUI;

		BattleRequestContext Context;
		SpecialEnemies CurrentEnemy;
		public int EnemyCurrentStamina;

		public int CurrentSequencePoseIndex;

		public void InitializeBattleEvent(BattleRequestContext context)
		{
			Context = context;
			CurrentEnemy = context.Enemy.Name;
			EnemyCurrentStamina = context.Enemy.MaxStamina;
			EnemyStaminaUI.text = EnemyCurrentStamina.ToString();
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
				EnemyStaminaUI.text = EnemyCurrentStamina.ToString(); 
			}
		}

		public void InflictDamage(DanceEvent.Pose pose)
		{
			switch (pose)
			{
				case DanceEvent.Pose.Splits:
					EnemyCurrentStamina -= 1;
					break;
				case DanceEvent.Pose.Cool:
					EnemyCurrentStamina -= 2;
					break;
				default:
					break;
			}

			EnemyStaminaUI.text = EnemyCurrentStamina.ToString(); 
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
