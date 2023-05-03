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
		[SerializeField]
		GameObject BattleEventUI;
		[SerializeField]
		GameObject BattleEventUIComponents;
		[SerializeField]
		DanceRequestHandler DanceHandler;
		[SerializeField]
		BattleEventManager BattleManager;
		[SerializeField]
		PlayerCam PlayerCam;

		BattleRequestContext Context;	
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

		public void EndBattleEvent()
		{
			PlayerCam.enabled = true;
			PlayerCam.SwitchMouseControls();
			BattleEventUIComponents.SetActive(false);
			BattleEventUI.SetActive(false);
			BattleIsActive = false;
		}

		void InitializeBattle()
		{
			BattleManager.InitializeBattleEvent(Context);
			// Restrict player motion, enable mouse menu input
			PlayerCam.SwitchMouseControls();
			// Set battle event UI to main input panel
			BattleEventUI.SetActive(true);
			BattleEventUIComponents.SetActive(true);
		}
	}
}
