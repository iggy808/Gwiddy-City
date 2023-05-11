using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DanceEvent;
using TMPro;

namespace BattleEvent
{
	public class BattleEventUIManager : MonoBehaviour
	{
		public InputState CurrentState;

		[SerializeField]
		GameObject PlayerHud;

		[SerializeField]
		BattleEventManager BattleManager;
		[SerializeField]
		InputController InputController;
		
		[SerializeField]
		SequencerUIManager SequencerUIManager;
		
		// References to player controller needed to get updated versions of player stats
		[SerializeField]
		GameObject Player;

		// UI Components may be shut off, allowing the battle logic to persist, but the UI elements to not be displayed
		[SerializeField]
		GameObject UIComponents;
		[SerializeField]
		GameObject InputPanel;
		[SerializeField]
		GameObject EnemyBattleStats;
		[SerializeField]
		Slider EnemyStaminaBar;
		[SerializeField]
		Slider EnemyCoolnessBar;
		[SerializeField]
		TextMeshProUGUI EnemyCoolnessThreshold;
		[SerializeField]
		GameObject PlayerBattleStats;
		[SerializeField]
		Slider PlayerStaminaBar;
		[SerializeField]
		Slider PlayerCoolnessBar;
		[SerializeField]
		TextMeshProUGUI PlayerCoolnessThreshold;

		// Handy button for going back a layer in the battle menu system
		[SerializeField]
		GameObject BackButton;

		// Main button categories - hold all buttons for a particular menu
		// Note: DanceMenuButtons should start empty, and generate buttons dynamically according to player's currently available moves
		[SerializeField]
		GameObject MainMenuButtons;
		[SerializeField]
		GameObject DanceMenuButtons;
		[SerializeField]
		GameObject DanceMenuButtonPrefab;

		List<GameObject> DanceMenuDanceButtons;

		// UI elements for the sequence menu
		[SerializeField]
		GameObject SequenceMenuButton;
		[SerializeField]
		GameObject SequenceMenu;
		[SerializeField]
		GameObject SequenceDanceButtonPrefab;
		[SerializeField]
		GameObject SequenceDancesPanel;

		public List<GameObject> SequenceDanceButtons;

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

		int MaxCooless = 150;

		public void HidePlayerHud()
		{
			PlayerHud.SetActive(false);
		}

		public void ShowPlayerHud()
		{
			PlayerHud.SetActive(true);
		}

		public void InitializeBattleUI(BattleRequestContext context)
		{
			EnemyUI_Name.text = context.Enemy.Name.ToString();
			InitializeBattleStats(context);
			ShowInputPanel();
			ShowMainMenu();
			HidePlayerHud();
		}

		public void InitializeBattleStats(BattleRequestContext context)
		{
			InitializeStaminaStats(context);	
			InitializeCoolnessStats(context);
		}

		public void InitializeCoolnessStats(BattleRequestContext context)
		{
			PlayerCoolnessThreshold.text = context.Enemy.CoolnessThreshhold.ToString();
			PlayerCoolnessBar.maxValue = MaxCooless;
			PlayerCoolnessBar.value = BattleManager.PlayerCurrentCoolness;
			PlayerUI_CurrentCoolness.text = BattleManager.PlayerCurrentCoolness.ToString();

			EnemyCoolnessThreshold.text = context.Enemy.CoolnessThreshhold.ToString();
			EnemyCoolnessBar.maxValue = MaxCooless;
			EnemyCoolnessBar.value = BattleManager.EnemyCurrentCoolness;
			EnemyUI_CurrentCoolness.text = BattleManager.EnemyCurrentCoolness.ToString();
		}

		public void InitializeStaminaStats(BattleRequestContext context)
		{
			PlayerStaminaBar.maxValue = context.Player.MaxStamina;
			PlayerStaminaBar.value = BattleManager.PlayerCurrentStamina;

			PlayerUI_CurrentStamina.text = BattleManager.PlayerCurrentStamina.ToString();
			PlayerUI_MaxStamina.text = context.Player.MaxStamina.ToString();

			EnemyStaminaBar.maxValue = context.Enemy.MaxStamina;
			EnemyStaminaBar.value = BattleManager.EnemyCurrentStamina;

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
			PlayerStaminaBar.value = BattleManager.PlayerCurrentStamina;
			PlayerUI_CurrentStamina.text = BattleManager.PlayerCurrentStamina.ToString();

			EnemyStaminaBar.value = BattleManager.EnemyCurrentStamina;
			EnemyUI_CurrentStamina.text = BattleManager.EnemyCurrentStamina.ToString();
		}

		public void UpdateCoolnessStats()
		{
			PlayerCoolnessThreshold.text = (BattleManager.EnemyCurrentCoolness + BattleManager.Context.Enemy.CoolnessThreshhold).ToString();
			PlayerCoolnessBar.value = BattleManager.PlayerCurrentCoolness;
			PlayerUI_CurrentCoolness.text = BattleManager.PlayerCurrentCoolness.ToString();

			EnemyCoolnessThreshold.text = (BattleManager.PlayerCurrentCoolness + BattleManager.Context.Enemy.CoolnessThreshhold).ToString();
			EnemyCoolnessBar.value = BattleManager.EnemyCurrentCoolness;
			EnemyUI_CurrentCoolness.text = BattleManager.EnemyCurrentCoolness.ToString();
		}

		public void ShowBattleStats()
		{
			EnemyBattleStats.SetActive(true);
			PlayerBattleStats.SetActive(true);
		}

		public void ShowEnemyBattleStats()
		{
			EnemyBattleStats.SetActive(true);
		}

		public void HideEnemyBattleStats()
		{
			EnemyBattleStats.SetActive(false);
		}

		public void ShowPlayerBattleStats()
		{
			PlayerBattleStats.SetActive(true);
		}

		public void HidePlayerBattleStats()
		{
			PlayerBattleStats.SetActive(false);
		}

		public void HideInputPanel()
		{
			InputPanel.SetActive(false);
		}

		public void ShowInputPanel()
		{
			InputPanel.SetActive(true);
		}

		public void ShowMainMenu(bool IsEnemyTurn = false)
		{
			// Delete generated buttons from dance menu if previous menu was dance menu
			if (CurrentState == InputState.DanceMenu)
			{
				foreach (GameObject button in DanceMenuDanceButtons)
				{
					Destroy(button);
				}
			}
			else if (CurrentState == InputState.SequenceMenu)
			{
				// Diable instantiated prefab buttons
				foreach (GameObject button in SequenceDanceButtons)
				{
					Destroy(button);
				}
			}

			CurrentState = InputState.MainMenu;

			BackButton.SetActive(false);
			UIComponents.SetActive(true);
			DanceMenuButtons.SetActive(false);
			MainMenuButtons.SetActive(true);
			SequenceMenu.SetActive(false);
		}

		IEnumerator DelayBeginEnemyTurn()
		{
			// Disable menu buttons
			Debug.Log("About to begin enemy turn!");
			yield return new WaitForSeconds(1f);
			BattleManager.PlayEnemyTurn();
		}
				
		public void ShowDanceMenu()
		{
			// If player is coming to the dance menu from the sequence menu, kill the buttons created for that menu
			if (CurrentState == InputState.SequenceMenu)
			{
				// Diable instantiated prefab buttons
				foreach (GameObject button in SequenceDanceButtons)
				{
					Destroy(button);
				}
			}

			ShowMainMenu();
			CurrentState = InputState.DanceMenu;

			if (BattleManager.CurrentTurn == BattleTurn.Player)
			{
				// Fetch updated list of dances
				PlayerAvailableDances = Player.GetComponent<PlayerDances>().Dances;

			}
			else if (BattleManager.CurrentTurn == BattleTurn.Enemy)
			{	
				// will figure out enemy stuff later
			}
	
			// Turn on the dance menu buttons according to the player's currently available dances
			MainMenuButtons.SetActive(false);
			if (BattleManager.PlayerCurrentStamina > 0)
			{
				SequenceMenuButton.transform.GetChild(0).GetComponent<Button>().interactable = true;
			}
			else
			{
				SequenceMenuButton.transform.GetChild(0).GetComponent<Button>().interactable = false;
			}
			SequenceMenu.SetActive(false);
			DanceMenuButtons.SetActive(true);
			BackButton.SetActive(true);

			// Dynamically generate a button for every dance in the player's dance list
			GenerateDanceMenuButtons();
			
		}



		public void GenerateDanceMenuButtons()
		{
			DanceMenuDanceButtons = new List<GameObject>();

			Vector2 startingAnchorMin = new Vector2(0f, 0.8f);
			Vector2 startingAnchorMax = new Vector2(0.5f, 1f);
			Vector2 rowAnchors_Y = new Vector2(startingAnchorMin.y, startingAnchorMax.y);	
			float anchorOffset_Y = -0.2f;

			int i = 0;
			foreach (DanceEvent.Pose pose in PlayerAvailableDances)
			{
				// Instantiate and position a prefab dance event button
				GameObject danceButton = Instantiate(DanceMenuButtonPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
				// For some reason have to enable this button? Maybe prefab error
				danceButton.SetActive(true);
				// Track current set of generated DanceMenuButtons for later destroying
				DanceMenuDanceButtons.Add(danceButton);

				// Set to be a child of the DanceMenuButtons container
				danceButton.transform.parent = DanceMenuButtons.transform;

				// Position with RectTransform accordingly
				RectTransform rectTransform = danceButton.GetComponent<RectTransform>();

				// Assign row anchors per button
				if (i == 0)
				{
					rowAnchors_Y = new Vector2(startingAnchorMin.y, startingAnchorMax.y);
				}
				else if (i % 2 == 0)
				{
					rowAnchors_Y = new Vector2(startingAnchorMin.y + ((i/2) * anchorOffset_Y), startingAnchorMax.y + ((i/2) * anchorOffset_Y));
				}
				else
				{
					rowAnchors_Y = new Vector2(rowAnchors_Y.x, rowAnchors_Y.y);
				}

				// Even/odd positioning logic:
				// Dances displayed like this
				// 0 | 1  y-anchors: (0.8, 1)
				// 2 | 3  y-anchors: (0.6, 0.8)
				// 4 | 5  y-anchors: (0.4, 0.6) 
				// 6 | 7  y-anchors: (0.2, 0.4)
				if (i % 2 == 0)
				{	
					// Set RectTransform anchors
					rectTransform.anchorMin = new Vector2(startingAnchorMin.x, rowAnchors_Y.x);
					rectTransform.anchorMax = new Vector2(startingAnchorMax.x, rowAnchors_Y.y); 
				}
				else
				{
					rectTransform.anchorMin = new Vector2(startingAnchorMax.x, rowAnchors_Y.x); 
					rectTransform.anchorMax = new Vector2(startingAnchorMax.x * 2, rowAnchors_Y.y); 
				}

				// Set rect transform, left, right, top, bottom
				rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
				rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y);
				rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0f);
				rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0f);

				// Label button with appropriate dance name
				DanceMenuButtons.transform.GetChild(i+1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pose.ToString().ToUpper() + "!";

				// Assign OnClick functions to generated buttons
				Button danceButtonComponent = DanceMenuButtons.transform.GetChild(i+1).transform.GetChild(0).GetComponent<Button>();
				danceButtonComponent.onClick.AddListener(delegate{InputController.DanceMenuDanceButtonClicked(pose);});

				if (BattleManager.GetPoseStaminaCost(pose) > BattleManager.PlayerCurrentStamina)
				{
					// Disable button if stamina too costly (hoping this dims the button a lil)
					danceButtonComponent.interactable = false;
				}

				i++;
			}
		}

		public void GenerateSequenceDanceButtons()
		{
			SequenceDanceButtons = new List<GameObject>();
			Vector2 startingAnchorMin = new Vector2(0f, 0.85f);
			Vector2 startingAnchorMax = new Vector2(1f, 0.95f);
			float anchorOffset_Y = -0.1f;

			int i = 0;
			foreach (DanceEvent.Pose pose in PlayerAvailableDances)
			{
				// Instantiate and position a prefab sequence dance button
				GameObject danceButton = Instantiate(SequenceDanceButtonPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
				// Track current set of SequenceDanceButtons for later destorying
				SequenceDanceButtons.Add(danceButton);
				// Set to be a child of the sequence dances panel
				danceButton.transform.parent = SequenceDancesPanel.transform;

				// Position with RectTransform accordingly
				RectTransform rectTransform = danceButton.GetComponent<RectTransform>();

				// Set RectTransform anchors
				rectTransform.anchorMin = new Vector2(startingAnchorMin.x, startingAnchorMin.y + (i * anchorOffset_Y));
				rectTransform.anchorMax = new Vector2(startingAnchorMax.x, startingAnchorMax.y + (i * anchorOffset_Y));

				// Set rect transform, left, right, top, bottom
				rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
				rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y);
				rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0f);
				rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0f);

				// Label button with appropriate dance name
				danceButton.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>()
					.text = pose.ToString().ToUpper() + "!";

				// Assign onclick event for add button
				// Assign onhover/onclick stat display for label* (maybe)
				Button addButtonComponent = danceButton.transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();

				// Assign onclick event to button
				addButtonComponent.onClick.AddListener(delegate{InputController.AddToSequencer(pose);});

				i++;
			}
		}

		public void ShowSequenceMenu()
		{
			if (CurrentState == InputState.DanceMenu)
			{
				foreach (GameObject button in DanceMenuDanceButtons)
				{
					Destroy(button);
				}
			}

			CurrentState = InputState.SequenceMenu;
			DanceMenuButtons.SetActive(false);	
			BackButton.SetActive(true);

			SequenceMenu.SetActive(true);
			GenerateSequenceDanceButtons();
			BattleManager.InitializeSequencerState();
			SequencerUIManager.InitializeSequencerIcons();
			SequencerUIManager.EnableSequencerSlotBorders(BattleManager.Context.Player.SequencerSlots);
		}
	}
}
