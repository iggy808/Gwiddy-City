using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using DanceEvent;

namespace BattleEvent
{
	public enum InputState
	{
		MainMenu,
		DanceMenu,
		SequenceMenu
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
		BattleEventUIManager BattleEventUIManager;
		[SerializeField]
		GameObject MainMenuButtons;
		[SerializeField]
		GameObject DanceMenuButtons;


		// temp alpha implementation for adding dances to menu
		[SerializeField]
		GameObject Player;
		List<DanceEvent.Pose> PlayerDances;


		DanceEvent.Pose CurrentPose;


		void Start()
		{
		}

		public void BackButtonClicked()
		{
			Debug.Log("BackButton clicked");
			if (BattleEventUIManager.CurrentState == InputState.DanceMenu)
			{
				Debug.Log("Show main menu called frim back button script");
				BattleEventUIManager.ShowMainMenu();
			}
			else if (BattleEventUIManager.CurrentState == InputState.SequenceMenu)
			{
				BattleManager.InitializeSequencerState();
				Debug.Log("Going back from sequence menu to dance menu.");
				BattleEventUIManager.ShowDanceMenu();
			}
		}

		public void FleeButtonClicked()
		{
			BattleManager.EndBattle();
		}

		// Will need to find a way to accept poses as input from the panel
		//
		// Also need to figure out how to change from using the cursor for 
		// looking around to using the cursor for clicking menu buttons,
		// because this does not work currently
		public void DanceMenuClicked()
		{
			Debug.Log("Dance button clicked");
			BattleEventUIManager.ShowDanceMenu();
		}

		public void SequenceMenuClicked()
		{
			BattleEventUIManager.ShowSequenceMenu();
		}

		public void DanceMenuDanceButtonClicked(DanceEvent.Pose pose)
		{
			BattleManager.TriggerOneOffDanceEvent(pose);
		}

		public void DemoSequenceEvent()
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
		}

		public void SplitsAttackClicked()
		{
			Debug.Log("Splits attack clicked");
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

		public void AddToSequencer(DanceEvent.Pose pose)
		{
			BattleManager.AddPoseToSequencer(pose);
		}

		public void MoveItButtonClicked()
		{
			BattleManager.TriggerSequenceEvent();
		}

	}
}
