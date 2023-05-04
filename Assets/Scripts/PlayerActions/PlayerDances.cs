using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDances : MonoBehaviour
{
	public List<DanceEvent.Pose> Dances;

    // Start is called before the first frame update
    void Start()
    {
        Dances = new List<DanceEvent.Pose>();
		// Give player their first move
		Dances.Add(DanceEvent.Pose.Splits);
    }

    // Update is called once per frame
    void Update()
    { 
		//Debug.Log("Number of dances: " + Dances.Count);
    }
}
