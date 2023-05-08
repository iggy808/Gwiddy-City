using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DanceEvent;

namespace BattleEvent
{
	public class BattleEventUIManager : MonoBehaviour
	{
		[SerializeField]
		GameObject Player;

		[SerializeField]
		GameObject UIComponents;
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
		
		List<DanceEvent.Pose> PlayerAvailableDances;

		public void ShowMainMenu()
		{
			UIComponents.SetActive(true);
			DanceMenuButtons.SetActive(false);
			MainMenuButtons.SetActive(true);
		}
				
		public void ShowDanceMenu()
		{
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
