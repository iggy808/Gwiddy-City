using UnityEngine;

namespace BattleEvent
{
	// Will control the turnbased battle's current state (stamina, dances, whateva)
	public class BattleEventManager : MonoBehaviour
	{
		[SerializeField]
		BattleRequestHandler BattleHandler;

		BattleRequestContext Context;
		SpecialEnemies CurrentEnemy;
		int CurrentStamina;

		public void InitializeBattleEvent(BattleRequestContext context)
		{
			Context = context;
			CurrentEnemy = context.Enemy.Name;
			CurrentStamina = context.Enemy.MaxStamina;
		}

		public void InflictDamage()
		{
			CurrentStamina -= 1;
			if (CurrentStamina<= 0)
			{
				Debug.Log("Gotem");
				Debug.Log("Enemy health: " + CurrentStamina);
				BattleHandler.EndBattleEvent();
			}			
		}
	}
}
