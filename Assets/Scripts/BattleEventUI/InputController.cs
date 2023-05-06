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
		BattleRequestHandler BattleHandler;
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


		DanceEvent.Pose CurrentPose;


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
				//Debug.Log("Player has cool");
				// activate cool button
				CoolButton.SetActive(true);
			}
			else
			{
				//Debug.Log("Player does not have cool");
				CoolButton.SetActive(false);
			}
		}

		public void SplitsAttackClicked()
		{
			CurrentPose = DanceEvent.Pose.Splits;
			DanceHandler.ActivateDanceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMoves = new List<DanceEvent.Pose>() 
				{
					DanceEvent.Pose.Splits
				},
				TargetObject = null
			});	
		}

		public void CoolAttackClicked()
		{
			CurrentPose = DanceEvent.Pose.Cool;
			DanceHandler.ActivateDanceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMoves = new List<DanceEvent.Pose>() 
				{
					DanceEvent.Pose.Cool
				},
				TargetObject = null
			});	
		}

		public void SequenceAttackClicked()
		{
			DanceHandler.ActivateDanceSequenceEvent(new DanceRequestContext()
			{
				Environment = Environment.BattleDance,
				DesiredMoves = new List<DanceEvent.Pose>()
				{
					DanceEvent.Pose.Splits,
					DanceEvent.Pose.Cool
				}
			});

			//BattleManager.CurrentSequencePoseIndex = ;
		}

		public void ResetMenuState(bool wasSuccessful)
		{
			if (wasSuccessful && DanceHandler.CurrentSequencePoseIndex >= DanceHandler.Context.DesiredMoves.Count)
			{
				Debug.Log("Dance sequence over! Resetting menu state");
				DanceMenuButtons.SetActive(false);
				MainMenuButtons.SetActive(true);
			}
			else
			{
				Debug.Log("You missed! No damage!");
			}
			CurrentState = InputState.MainMenu;
		}
	}
}
