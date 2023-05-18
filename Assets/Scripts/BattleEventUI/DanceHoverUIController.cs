using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// One of these scripts for every mouse button
public class DanceHoverUIController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{	
	public GameObject HoverInfoPanel;
	TextMeshProUGUI PoseNameText;
	TextMeshProUGUI StaminaCostText;
	TextMeshProUGUI CoolnessGainText;
	TextMeshProUGUI EffectsText;


	PoseBattleInfo PoseInfo;
	bool IsMouseHovering;
	bool IsHoverUIDisplayed = false;

	
	void Update()
	{
		if (IsMouseHovering)
		{
			Debug.Log("Mouse hovering over button");
			if (!IsHoverUIDisplayed)
			{
				Debug.Log("Displaying stats panel");

				// Display hover UI
				IsHoverUIDisplayed = true;	
				HoverInfoPanel.SetActive(true);
				if (gameObject.name != "(Seq.)DanceButtonContainer")
				{
					PoseNameText.text = PoseInfo.Pose.ToString();
					StaminaCostText.text = PoseInfo.StaminaCost.ToString();
					CoolnessGainText.text = PoseInfo.CoolnessGain.ToString();
					EffectsText.text = PoseInfo.Effects;
				}
				else 
				{
					PoseNameText.text = "Sequencer\nMenu";
					StaminaCostText.text = "-";
					CoolnessGainText.text = "-";
					EffectsText.text = "Sequence moves!";
				}
			}
		}
	}

	public void SetOnHoverInfo(DanceEvent.Pose pose)
	{
		// Cache stats relevant to poses
		PoseInfo = new PoseBattleInfo(pose);

		// Cache references to text objects on hover stats panel
		PoseNameText = HoverInfoPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
		StaminaCostText = HoverInfoPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
		CoolnessGainText = HoverInfoPanel.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
		EffectsText = HoverInfoPanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
			
		Debug.Log("Setting on hover info for button's new dancehoverui component");
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		IsMouseHovering = true;
		Debug.Log("Mouse began hovering over button.");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		IsMouseHovering = false;
		IsHoverUIDisplayed = false;
		HoverInfoPanel.SetActive(false);
		// Disable hover UI
		Debug.Log("Mouse no longer hovering over button");
	}
}
