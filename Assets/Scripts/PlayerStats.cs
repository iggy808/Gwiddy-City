using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public int MaxStamina;
	public int CurrentStamina;
	public int SequencerSlots;

    // Start is called before the first frame update
    void Start()
    {
		MaxStamina = 10;    
		CurrentStamina = 10;
		SequencerSlots = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
