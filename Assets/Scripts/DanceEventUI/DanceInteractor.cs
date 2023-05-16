using UnityEngine;

public enum InteractorType
{
	OldManTutorial,
	EnvEnemy,
	TutorialBridge1,
	TutorialBridge2,
	None
}


public class DanceInteractor : MonoBehaviour
{
	public InteractorType Type;
}
