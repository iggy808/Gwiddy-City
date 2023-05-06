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

		public void ManagePoses()
		{
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

		public void EndBattle()
		{
			if (EnemyCurrentStamina <= 0)
			{
				BattleHandler.EndBattleEvent();
			}
		}
	}
}
