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

		bool IsMouseOver;


		public void OnPointerEnter(PointerEventData eventData)
		{
			Debug.Log("Mouse is hovering over button");
			IsMouseOver = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Debug.Log("Mouse has stopped hovering over button");
			IsMouseOver = false;
		}

		void Start()
		{
		}

		public void BackButtonClicked()
		{
			Debug.Log("BackButton clicked");
			if (BattleEventUIManager.CurrentState == InputState.DanceMenu)
			{
				BattleEventUIManager.ShowMainMenu();
			}
			else if (BattleEventUIManager.CurrentState == InputState.SequenceMenu)
			{
				BattleManager.InitializeSequencerState();
				BattleEventUIManager.ShowDanceMenu();
			}
		}

		public void FleeButtonClicked()
		{
			BattleManager.EndBattle();
		}

		public void RestButtonClicked()
		{
			BattleManager.RestTurn();
		}

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
