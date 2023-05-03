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
				case "LameDanceEventTrigger":
					BattleHandler.ActivateBattleEvent(new BattleRequestContext
					{
						Enemy = new Enemy(SpecialEnemies.LameDancer)
					});
					break;
				case "SickDanceEventTrigger":
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
