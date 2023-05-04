using UnityEngine;
using TMPro;

namespace BattleEvent
{
	// Will control the turnbased battle's current state (stamina, dances, whateva)
	public class BattleEventManager : MonoBehaviour
	{
		[SerializeField]
		BattleRequestHandler BattleHandler;
		[SerializeField]
		TextMeshProUGUI EnemyStaminaUI;

		BattleRequestContext Context;
		SpecialEnemies CurrentEnemy;
		int EnemyCurrentStamina;

		public void InitializeBattleEvent(BattleRequestContext context)
		{
			Context = context;
			CurrentEnemy = context.Enemy.Name;
			EnemyCurrentStamina = context.Enemy.MaxStamina;
			EnemyStaminaUI.text = EnemyCurrentStamina.ToString(); 			
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
			if (EnemyCurrentStamina <= 0)
			{
				Debug.Log(EnemyCurrentStamina);
				Debug.Log("Gotem");
				BattleHandler.EndBattleEvent();
			}			
		}
	}
}
