using UnityEngine;

public enum InteractorType
{
	OldManTutorial,
	EnvEnemy,
	TutorialBridge,
	None
}


public class DanceInteractor : MonoBehaviour
{
	public InteractorType Type;
}
