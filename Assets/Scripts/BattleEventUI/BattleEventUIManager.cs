using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DanceEvent;
using TMPro;

namespace BattleEvent
{
	public class BattleEventUIManager : MonoBehaviour
	{
		public InputState CurrentState;
		[SerializeField]
		BattleEventManager BattleManager;
		
		// References to player controller needed to get updated versions of player stats
		[SerializeField]
		GameObject Player;
		// UI Components may be shut off, allowing the battle logic to persist, but the UI elements to not be displayed
		[SerializeField]
		GameObject UIComponents;

		// Handy button for going back a layer in the battle menu system
		[SerializeField]
		GameObject BackButton;


		// Main button categories - hold all buttons for a particular menu
		// Note: DanceMenuButtons should start empty, and generate buttons dynamically according to player's currently available moves
		[SerializeField]
		GameObject MainMenuButtons;
		[SerializeField]
		GameObject DanceMenuButtons;

		// UI elements for the sequence menu
		[SerializeField]
		GameObject SequenceMenuButton;
		[SerializeField]
		GameObject SequenceMenu;

		// Temporary hardcoded button references
		// Need to find a way to dynamically generate these with prefab Instantiate()
		[SerializeField]
		GameObject SplitsButton;
		[SerializeField]
		GameObject CoolButton;

		// Enemy UI name text
		[SerializeField]
		TextMeshProUGUI EnemyUI_Name;
	
		// Stamina UI text
		[SerializeField]
		TextMeshProUGUI EnemyUI_CurrentStamina;
		[SerializeField]
		TextMeshProUGUI EnemyUI_MaxStamina;
		[SerializeField]
		TextMeshProUGUI PlayerUI_CurrentStamina;
		[SerializeField]
		TextMeshProUGUI	PlayerUI_MaxStamina;

		// Coolness UI text
		[SerializeField]
		TextMeshProUGUI EnemyUI_CurrentCoolness;
		[SerializeField]
		TextMeshProUGUI	PlayerUI_CurrentCoolness;


		List<DanceEvent.Pose> PlayerAvailableDances;
		List<DanceEvent.Pose> EnemyAvailableDances;

		public void InitializeBattleUI(BattleRequestContext context)
		{
			EnemyUI_Name.text = context.Enemy.Name.ToString();
			InitializeBattleStats(context);
		}

		public void InitializeBattleStats(BattleRequestContext context)
		{
			InitializeStaminaStats(context);	
			InitializeCoolnessStats(context);
		}

		public void InitializeCoolnessStats(BattleRequestContext context)
		{
			Debug.Log("Initializing coolness stats to 0");
			EnemyUI_CurrentCoolness.text = BattleManager.EnemyCurrentCoolness.ToString();
			PlayerUI_CurrentCoolness.text = BattleManager.PlayerCurrentCoolness.ToString();
		}

		public void InitializeStaminaStats(BattleRequestContext context)
		{
			Debug.Log("Initializing stamina stats");
			PlayerUI_CurrentStamina.text = BattleManager.PlayerCurrentStamina.ToString();
			PlayerUI_MaxStamina.text = context.Player.MaxStamina.ToString();
			Debug.Log("Current Player stamina: " + context.Player.CurrentStamina.ToString());
			Debug.Log("Player max stamina: " + context.Player.MaxStamina.ToString());


			EnemyUI_CurrentStamina.text = context.Enemy.MaxStamina.ToString();
			EnemyUI_MaxStamina.text = context.Enemy.MaxStamina.ToString();
		}

		public void UpdateBattleStats()
		{
			UpdateStaminaStats();
			UpdateCoolnessStats();
		}

		public void UpdateStaminaStats()
		{
			PlayerUI_CurrentStamina.text = BattleManager.PlayerCurrentStamina.ToString();
			EnemyUI_CurrentStamina.text = BattleManager.EnemyCurrentStamina.ToString();
		}

		public void UpdateCoolnessStats()
		{
			PlayerUI_CurrentCoolness.text = BattleManager.PlayerCurrentCoolness.ToString();
			EnemyUI_CurrentCoolness.text = BattleManager.EnemyCurrentCoolness.ToString();
		}

		public void ShowMainMenu()
		{
			CurrentState = InputState.MainMenu;
			BackButton.SetActive(false);
			UIComponents.SetActive(true);
			DanceMenuButtons.SetActive(false);
			MainMenuButtons.SetActive(true);
			SequenceMenu.SetActive(false);
		}
				
		public void ShowDanceMenu()
		{
			CurrentState = InputState.DanceMenu;
			Debug.Log("In show dance menu fn");

			if (BattleManager.CurrentTurn == BattleTurn.Player)
			{
				// Fetch updated list of dances
				PlayerAvailableDances = Player.GetComponent<PlayerDances>().Dances;
				// Remove dances where the stamina cost is higher than the available stamina
				PlayerAvailableDances = PlayerAvailableDances.Where(x => BattleManager.GetPoseStaminaCost(x) <= BattleManager.PlayerCurrentStamina).ToList();
			}
			else if (BattleManager.CurrentTurn == BattleTurn.Enemy)
			{	
				EnemyAvailableDances = BattleManager.Context.Enemy.DanceMoves.Where(x => BattleManager.GetPoseStaminaCost(x) <= BattleManager.EnemyCurrentStamina).ToList();
			}
	
			// Need to dynamically generate a button for every dance in the player's dance list
			//GenerateDanceButtons();

			// Turn on the dance menu buttons according to the player's currently available dances
			MainMenuButtons.SetActive(false);
			SequenceMenu.SetActive(false);
			DanceMenuButtons.SetActive(true);
			BackButton.SetActive(true);
			if (BattleManager.CurrentTurn == BattleTurn.Player)
			{
				if (PlayerAvailableDances.Contains(DanceEvent.Pose.Splits))
				{
					Debug.Log("Player has enough stamina to use the splits.");
					SplitsButton.SetActive(true);
				}
				else
				{
					SplitsButton.SetActive(false);
				}

				if (PlayerAvailableDances.Contains(DanceEvent.Pose.Cool))
				{
					Debug.Log("Player has enough stamina to use the splits.");
					CoolButton.SetActive(true);
				}
				else
				{
					CoolButton.SetActive(false);
				}
			}
			else if (BattleManager.CurrentTurn == BattleTurn.Enemy)
			{
				if (EnemyAvailableDances.Contains(DanceEvent.Pose.Splits))	
				{
					SplitsButton.SetActive(true); }
				else
				{
					SplitsButton.SetActive(false);
				}
				if (EnemyAvailableDances.Contains(DanceEvent.Pose.Cool))
				{
					Debug.Log("Player has enough stamina to use the splits.");
					CoolButton.SetActive(true);
				}
				else
				{
					CoolButton.SetActive(false);
				}
			}

			SequenceMenuButton.SetActive(true);
		}


		public void GenerateDanceButtons()
		{
			foreach (DanceEvent.Pose pose in PlayerAvailableDances)
			{
				// instantiate and position a prefab dance button	
				Debug.Log("Generating a dance button!");
			}
		}

		public void ShowSequenceMenu()
		{
			CurrentState = InputState.SequenceMenu;
			DanceMenuButtons.SetActive(false);	
			BackButton.SetActive(true);

			// Same problem as show dance menu:
			// Need to dynamically generate buttons according to player available dances
			SequenceMenu.SetActive(true);
		}
	}
}
