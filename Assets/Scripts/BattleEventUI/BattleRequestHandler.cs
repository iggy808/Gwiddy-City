using UnityEngine;
using DanceEvent;

namespace BattleEvent
{
	/* Battle event scene hierarchy:
	 * 
	 *   BattleEventUI object holds BattleRequestHander
	 *   \
	 *   \--BattleEventCanvas (BattleEventUI) object holds all event logic (BattleEventManager, InputController)
	 *      \
	 *      \--UIComponents (BattleEventUIComponents) object holds all UI components (panels, buttons, etc.)
	 */
	public class BattleRequestHandler : MonoBehaviour
	{
		BattleRequestContext Context;	
		[SerializeField]
		GameObject BattleEventUI;
		[SerializeField]
		GameObject BattleEventUIComponents;
		[SerializeField]
		DanceRequestHandler DanceHandler;

		bool BattleIsActive = false;		 

		void Start()
		{
			BattleEventUIComponents.SetActive(false);
			BattleEventUI.SetActive(false);	
		}

		public void ActivateBattleEvent(BattleRequestContext context)
		{
			Context  = context;
			if (!BattleIsActive)
			{
				BattleIsActive = true;
				InitializeBattle();
			}
		}

		void InitializeBattle()
		{
			/* potentially useful at some point
			 * if we want to do stuff with UI based on boss/spacial enemy
			switch (context.Enemy)
			{
				case SpecialEnemy.CoolDancer:
					break;
				default:
					break;
			}
			*/
			BattleEventUI.SetActive(true);
			BattleEventUIComponents.SetActive(true);
		}
	}
}
