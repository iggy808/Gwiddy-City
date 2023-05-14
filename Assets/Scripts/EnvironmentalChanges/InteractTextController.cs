using UnityEngine;

public class InteractTextController : MonoBehaviour
{
	[SerializeField]
	GameObject Player;
	[SerializeField]
	GameObject InteractTextObject;

	InteractTextActions TextActions;

	bool IsActive;

	void Start()
	{
		TextActions = InteractTextObject.GetComponent<InteractTextActions>();
		TextActions.enabled = false;
		IsActive = false;

		InteractTextObject.SetActive(false);
	}

	public void ActivateInteractText()
	{
		if (!IsActive)
		{
			Debug.Log("InteractText is being initialized.");
			IsActive = true;
			InteractTextObject.SetActive(true);		
			TextActions.enabled = true;
		}
		else
		{
			Debug.Log("InteractText is still active...");
		}
	}

	public void DisableInteractText()
	{
		if (IsActive)
		{
			IsActive = false;
			TextActions.enabled = false;
			InteractTextObject.SetActive(false);
		}
	}
}
