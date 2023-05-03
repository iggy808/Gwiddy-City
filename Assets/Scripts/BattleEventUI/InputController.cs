using UnityEngine;
using DanceEvent;

namespace BattleEvent
{
	public enum InputState
	{
		MainMenu,
		DanceMenu
	}

	public class InputController : MonoBehaviour
	{
		[SerializeField]
		DanceRequestHandler DanceHandler;
		[SerializeField]
		BattleEventManager BattleManager;
		[SerializeField]
		GameObject MainMenuButtons;
		[SerializeField]
		GameObject DanceMenuButtons;

		InputState CurrentState;

		void Start()
		{
			MainMenuButtons.SetActive(true);
			DanceMenuButtons.SetActive(false);
		}

		// Will need to find a way to accept poses as input from the panel
		//
		// Also need to figure out how to change from using the cursor for 
		// looking around to using the cursor for clicking menu buttons,
		// because this does not work currently
		public void DanceMenuClicked()
		{
			// turon on a different set of UI buttons
			DanceMenuButtons.SetActive(true);
		}

		public void DanceAttackClicked()
		{
			DanceHandler.ActivateDanceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMove = DanceEvent.Pose.Splits,
				TargetObject = null
			});	
		}

		public void ResetMenuState(bool wasSuccessful)
		{
			DanceMenuButtons.SetActive(false);
			MainMenuButtons.SetActive(true);
			if (wasSuccessful)
			{
				BattleManager.InflictDamage();
			}
			else
			{
				Debug.Log("You missed! No damage!");
			}
			CurrentState = InputState.MainMenu;
		}
	}
}
