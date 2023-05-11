using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : MonoBehaviour
{
	[SerializeField]
	GameObject Player;

    public PlayerOrbCollection importedTotalValue;
	List<DanceEvent.Pose> Dances;

    // Start is called before the first frame update
    void Start()
    {
		Dances = Player.GetComponent<PlayerDances>().Dances; 
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
		Dances = Player.GetComponent<PlayerDances>().Dances;
        if (other.tag == "Player" && importedTotalValue.total == 0)
        {
            importedTotalValue.total++;
			// Unlock dance move
			Dances.Add(DanceEvent.Pose.Cool);
			Debug.Log("Dance added");

            Destroy(gameObject);
        }
		else if (other.tag == "Player" && importedTotalValue.total >= 1)
		{
			importedTotalValue.total++;
			//  Unlock dance move
			Dances.Add(DanceEvent.Pose.Sick);
			Debug.Log("Dance added");

			Destroy(gameObject);
		}
    }
}
