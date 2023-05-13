using UnityEngine;
using System.Collections.Generic;


public enum ScenarioType
{
	TutorialOldMan,
	None
}

public enum ScenarioStates
{
	NotYetEngaged,
	InteractedOnce,
	DanceEventTutorial,
	DanceEventTutorialOver,
	InteractedTwice,
	Over
}

public class Scenario
{
	public int DialogueInteractionCount;
	public ScenarioType Type;
	public List<ScenarioStates> States;

	public Scenario(ScenarioType scenario)
	{
		switch (scenario)
		{
			case ScenarioType.TutorialOldMan:
				DialogueInteractionCount = 2;
				States = new List<ScenarioStates>()
				{
					ScenarioStates.NotYetEngaged,
					ScenarioStates.InteractedOnce,
					ScenarioStates.DanceEventTutorial,
					ScenarioStates.DanceEventTutorialOver,
					ScenarioStates.InteractedTwice,
					ScenarioStates.Over
				};
				break;
			case ScenarioType.None:
				DialogueInteractionCount = 0;
				break;
			default:
				break;
		}
	}
}
