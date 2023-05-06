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
		int EnemyCurrentStamina;

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

		public void InflictDamage(DanceEvent.Pose pose, bool isLastInSequence)
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
			Debug.Log("OOh ouchy - took some damage: currenthp: " + EnemyCurrentStamina);

			EnemyStaminaUI.text = EnemyCurrentStamina.ToString(); 
			Debug.Log("Current sequence index: " + CurrentSequencePoseIndex);
			Debug.Log("Current sequence length: " + BattleHandler.DanceHandler.Context.DesiredMoves.Count);
			if (EnemyCurrentStamina <= 0)
			{
				//Debug.Log(EnemyCurrentStamina);
				//Debug.Log("Gotem");
				BattleHandler.EndBattleEvent();
			}			
		}
	}
}
