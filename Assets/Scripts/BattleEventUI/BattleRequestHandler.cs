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
		public bool BattleIsActive = false;		 
		public BattleRequestContext Context;	
		public DanceRequestHandler DanceHandler;

		[SerializeField]
		GameObject BattleEventUI;
		[SerializeField]
		GameObject BattleEventUIComponents;
		[SerializeField]
		BattleEventManager BattleManager;
		[SerializeField]
		PlayerCam PlayerCam;

		

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
			Debug.Log("EndBattleEvent called");
			PlayerCam.enabled = true;
			PlayerCam.SwitchMouseControls();
			BattleEventUIComponents.SetActive(false);
			BattleEventUI.SetActive(false);
			BattleIsActive = false;
		}

		void InitializeBattle()
		{
			// Let DanceHandler know the state of the battle
			DanceHandler.CurrentBattleMoveCount = 0;
			BattleManager.InitializeBattleEvent(Context);
			// Restrict player motion, enable mouse menu input
			PlayerCam.SwitchMouseControls();
			// Set battle event UI to main input panel
			BattleEventUI.SetActive(true);
			BattleEventUIComponents.SetActive(true);
		}

		void SynchronizeDanceUI()
		{
		}
	}
}
