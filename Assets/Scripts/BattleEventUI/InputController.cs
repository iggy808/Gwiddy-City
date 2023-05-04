using UnityEngine;
using System.Collections.Generic;
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


		// temp alpha implementation for adding dances to menu
		[SerializeField]
		GameObject Player;
		[SerializeField]
		GameObject SplitsButton;
		[SerializeField]
		GameObject CoolButton;
		List<DanceEvent.Pose> PlayerDances;
		InputState CurrentState;


		void Start()
		{
			PlayerDances = Player.GetComponent<PlayerDances>().Dances;
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
			PlayerDances = Player.GetComponent<PlayerDances>().Dances;
			if (PlayerDances.Contains(DanceEvent.Pose.Splits))
			{
				// activate splits button	
				SplitsButton.SetActive(true);

			}
			else
			{
				SplitsButton.SetActive(false);
			}

			if (PlayerDances.Contains(DanceEvent.Pose.Cool))
			{
				Debug.Log("Player has cool");
				// activate cool button
				CoolButton.SetActive(true);
			}
			else
			{
				Debug.Log("Player does not have cool");
				CoolButton.SetActive(false);
			}
		}

		public void SplitsAttackClicked()
		{
			DanceHandler.ActivateDanceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMove = DanceEvent.Pose.Splits,
				TargetObject = null
			});	
		}

		public void CoolAttackClicked()
		{
			DanceHandler.ActivateDanceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMove = DanceEvent.Pose.Cool,
				TargetObject = null
			});	
		}

		public void ResetMenuState(bool wasSuccessful)
		{
			DanceMenuButtons.SetActive(false);
			MainMenuButtons.SetActive(true);
			if (wasSuccessful)
			{
				Debug.Log("Dance successful! Inflicting damage...");
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
