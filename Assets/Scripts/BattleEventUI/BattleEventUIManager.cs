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

		// Temporary hardcoded button references
		// Need to find a way to dynamically generate these with prefab Instantiate()
		[SerializeField]
		GameObject SplitsButton;
		[SerializeField]
		GameObject CoolButton;
		[SerializeField]
		GameObject SequenceButton;

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

		public void InitializeBattleUI(BattleRequestContext context)
		{
			EnemyUI_Name.text = context.Enemy.Name.ToString();
			InitializeBattleStats(context);
		}

		public void InitializeBattleStats(BattleRequestContext context)
		{
			// Fetch updated player stats, update context accordingly
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
		}
				
		public void ShowDanceMenu()
		{
			CurrentState = InputState.DanceMenu;
			BackButton.SetActive(true);
			Debug.Log("In show dance menu fn");

			// Fetch updated list of dances
			PlayerAvailableDances = Player.GetComponent<PlayerDances>().Dances;
			
			// Need to dynamically generate a button for every dance in the player's dance list
			//GenerateDanceButtons();

			// Turn on the dance menu buttons according to the player's currently available dances
			MainMenuButtons.SetActive(false);
			DanceMenuButtons.SetActive(true);

			if (PlayerAvailableDances.Contains(DanceEvent.Pose.Splits))
			{
				SplitsButton.SetActive(true);
			}
			else
			{
				SplitsButton.SetActive(false);
			}

			if (PlayerAvailableDances.Contains(DanceEvent.Pose.Cool))
			{
				CoolButton.SetActive(true);
			}
			else
			{
				CoolButton.SetActive(false);
			}

			SequenceButton.SetActive(true);
		}


		public void GenerateDanceButtons()
		{
			foreach (DanceEvent.Pose pose in PlayerAvailableDances)
			{
				// instantiate and position a prefab dance button	
				Debug.Log("Generating a dance button!");
			}
		}
	}
}
