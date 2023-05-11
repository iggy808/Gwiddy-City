using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace BattleEvent
{
	public class SequencerUIManager : MonoBehaviour
	{
		public List<GameObject> SequencerIcons;

		[SerializeField]	
		GameObject SequencerSlotIconPrefab;
		[SerializeField]
		List<GameObject> SequencerSlotBorders;
				
		public void EnableSequencerSlotBorders(int sequencerSlotCount)
		{
			Debug.Log("Enabling sequencer borders, player sequencer slot count : " + sequencerSlotCount);
			for (int i = 0; i < sequencerSlotCount; i++)
			{
				SequencerSlotBorders.ElementAt(i).SetActive(true);
			}
			
			if (sequencerSlotCount < SequencerSlotBorders.Count)
			{
				for (int i = sequencerSlotCount; i < SequencerSlotBorders.Count; i++)
				{
					SequencerSlotBorders.ElementAt(i).SetActive(false);
				}
			}
		}

		public void AddPoseIconToSequencer(int currentSequencerIndex, DanceEvent.Pose pose)
		{
			// need to generate icons as prefabs and adjust them
			Debug.Log("Adding pose icon to sequencer.");
			// Instantiate a sequencer icon prefab
			GameObject sequencerIcon = Instantiate(SequencerSlotIconPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
			SequencerIcons.Add(sequencerIcon);

			// Make prefab's parent be the sequencer slot at the correct index
			Debug.Log("Current sequencer index : " + currentSequencerIndex);
			sequencerIcon.transform.parent = SequencerSlotBorders.ElementAt(currentSequencerIndex).transform.parent.transform;

			// Position icon as needed
			RectTransform rectTransform = sequencerIcon.GetComponent<RectTransform>();	

			rectTransform.anchorMin = new Vector2(0.15f, 0.15f);
			rectTransform.anchorMax = new Vector2(0.85f, 0.85f);

			// Set rect transform, left, right, top, bottom (0, 0, 0, 0)
			rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
			rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y);
			rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0f);
			rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0f);

			// Set icon to have appropriate label for pose
			sequencerIcon.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pose.ToString().ToUpper() + "!";
		}
		
		public void InitializeSequencerIcons()
		{
			// Delete old icons
			if (SequencerIcons != null)
			{
				foreach (GameObject sequenceIcon in SequencerIcons)
				{
					Destroy(sequenceIcon);
				}
			}

			// Empty sequencer icons list
			SequencerIcons = new List<GameObject>();
		}
		
	}
}
