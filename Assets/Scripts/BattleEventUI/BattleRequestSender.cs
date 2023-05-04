using UnityEngine;

namespace BattleEvent
{
	public class BattleRequestSender : MonoBehaviour
	{
		[SerializeField]
		BattleRequestHandler BattleHandler;

		void OnTriggerEnter(Collider collider)
		{
			Debug.Log("Trigger entered");
			// test triggers to send new dance requests
			switch (collider.gameObject.name)
			{
				case "LameDanceEventTrigger (1)":
					BattleHandler.ActivateBattleEvent(new BattleRequestContext
					{
						Enemy = new Enemy(SpecialEnemies.LameDancer)
					});
					break;
				case "SickDanceEventTrigger (1)":
					BattleHandler.ActivateBattleEvent(new BattleRequestContext
					{
						Enemy = new Enemy(SpecialEnemies.SickDancer)
					});
					break;
				default:
					break;
			}
		}
	}
}
