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

		List<GameObject> SequenceDanceButtons;

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
			if (CurrentState == InputState.DanceMenu)
			{
				foreach (GameObject button in DanceMenuDanceButtons)
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
	
			// Turn on the dance menu buttons according to the player's currently available dances
			MainMenuButtons.SetActive(false);
			SequenceMenu.SetActive(false);
			DanceMenuButtons.SetActive(true);
			BackButton.SetActive(true);

			// Need to dynamically generate a button for every dance in the player's dance list
			GenerateDanceButtons();

			/*
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
			*/

			SequenceMenuButton.SetActive(true);
		}


		public void GenerateDanceButtons()
		{
			DanceMenuDanceButtons = new List<GameObject>();

			// Note:
			// Left side (i%2=0):
			// Min/Max x anchors: (0, 0.5)
			// Min/Max y anchors: (0.8, 1) <-- Y anchors vary by intervals of 0.2

			// Right side (i%2!=0):
			// Min/Max x anchors: (0.5,1)
			// Min/Max y anchors: (0.8, 1) <-- Y anchors vary by intervals 0.2
			Vector2 startingAnchorMin = new Vector2(0f, 0.8f);
			Vector2 startingAnchorMax = new Vector2(0.5f, 1f);
			Vector2 rowAnchors_Y = new Vector2(startingAnchorMin.y, startingAnchorMax.y);	
			float anchorOffset_Y = -0.2f;

			int i = 0;
			foreach (DanceEvent.Pose pose in PlayerAvailableDances)
			{
				// instantiate and position a prefab dance button	
				Debug.Log("Generating a dance button! i: " + i);

				// instantiate and position a prefab sequence dance event button
				// Instantiate dance button
				GameObject danceButton = Instantiate(DanceMenuButtonPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
				danceButton.SetActive(true);
				DanceMenuDanceButtons.Add(danceButton);

				// Set to be a child of the sequence dances panel
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
					Debug.Log("i : " + i + " , rowAnchors_Y (min) : " + rowAnchors_Y.x + " , rowAnchors_Y (max) : " + rowAnchors_Y.y);
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
				Debug.Log("First child of sequence dances panel (Sequence button) : " + DanceMenuButtons.transform.GetChild(0).name);
				Debug.Log("Second child of dance menu buttons object (Dance button container) : " + DanceMenuButtons.transform.GetChild(1).name);
				Debug.Log("Ideally 'text': " + DanceMenuButtons.transform.GetChild(i+1).transform.GetChild(0).transform.GetChild(0).name);
				DanceMenuButtons.transform.GetChild(i+1).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pose.ToString().ToUpper() + "!";

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
				// instantiate and position a prefab sequence dance event button
				Debug.Log("Generating a sequence dance button! i: " + i);
				// Instantiate dance button
				GameObject danceButton = Instantiate(SequenceDanceButtonPrefab, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
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
				Debug.Log("First child of sequence dances panel : " + SequenceDancesPanel.transform.GetChild(0).name);
				Debug.Log("Ideally 'DanceButton': " + SequenceDancesPanel.transform.GetChild(i).transform.GetChild(0).name);
				SequenceDancesPanel.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pose.ToString().ToUpper() + "!";

				// Assign onclick event for add button
				// Assign onhover/onclick stat display for label

				i++;
				// Note from internet: 
				// How to set left (left is float val for left):
				//rt.offsetMin = new Vector2(left, rt.offsetMin.y);
				// How to set right (right is float val for right):
				//rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
				// How to set top (top is float val for top)
				//rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
				// How to set bottom (bottom is float val for bottom)
				//rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
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

			// Need to dynamically generate buttons according to player available dances
			SequenceMenu.SetActive(true);
			// Same problem as show dance menu:
			GenerateSequenceDanceButtons();
		}
	}
}
