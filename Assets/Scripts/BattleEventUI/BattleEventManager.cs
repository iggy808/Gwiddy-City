using UnityEngine;
namespace BattleEventUI
{
	// Will control the turnbased battle game loop
	public class BattleEventManager : MonoBehaviour
	{
		[SerializeField]
		GameObject PlayerInputPanel;

		public void InitializeBattle()
		{
			// maybe restrict player movement here
		}
	}
}
