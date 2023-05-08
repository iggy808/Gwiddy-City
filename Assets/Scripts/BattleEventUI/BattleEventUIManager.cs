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
		GameObject Player;
		[SerializeField]
		GameObject UIComponents;
		[SerializeField]
		GameObject BackButton;
		[SerializeField]
		GameObject MainMenuButtons;
		[SerializeField]
		GameObject DanceMenuButtons;
		[SerializeField]
		GameObject SplitsButton;
		[SerializeField]
		GameObject CoolButton;
		[SerializeField]
		GameObject SequenceButton;
		[SerializeField]
		TextMeshProUGUI EnemyUI_CurrentStamina;
		[SerializeField]
		TextMeshProUGUI EnemyUI_MaxStamina;
		[SerializeField]
		TextMeshProUGUI PlayerUI_CurrentStamina;
		[SerializeField]
		TextMeshProUGUI	PlayerUI_MaxStamina;

		List<DanceEvent.Pose> PlayerAvailableDances;

		public void InitializeStaminaStats(PlayerStats player, Enemy enemy)
		{
			PlayerUI_CurrentStamina.text = player.CurrentStamina.ToString();
			PlayerUI_MaxStamina.text = player.MaxStamina.ToString();

			EnemyUI_CurrentStamina.text = enemy.MaxStamina.ToString();
			EnemyUI_MaxStamina.text = enemy.MaxStamina.ToString();
		}

		public void UpdateStaminaStats(int playerStamina, int enemyStamina)
		{
			PlayerUI_CurrentStamina.text = playerStamina.ToString();
			EnemyUI_CurrentStamina.text = enemyStamina.ToString();
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
