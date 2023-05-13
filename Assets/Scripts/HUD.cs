using HighScore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public GameObject Dance1;
    public GameObject Dance2;
    public GameObject Dance3;
    public GameObject Dance4;
    public GameObject Dance5;
    public GameObject player;

    private bool dance1Acquired;
    private bool dance2Acquired;
    private bool dance3Acquired;
    private bool dance4Acquired;
    private bool dance5Acquired;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the score with the game name as a string
        // PUT THIS IN A START FUNCTION (title scree, HUD, etc; whatever runs at the start of the game
        HS.Init(this, "Gwiddy City");
        GameObject end = GameObject.FindWithTag("EndGame");
        end.SetActive(false);

        Dance1.gameObject.SetActive(false);
        Dance2.gameObject.SetActive(false);
        Dance3.gameObject.SetActive(false);
        Dance4.gameObject.SetActive(false);
        Dance5.gameObject.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        // Move this from the update function so that we can have animations.
        if (player.GetComponent<PlayerDances>().Dances.Contains(DanceEvent.Pose.Splits))
        {
            Dance1.gameObject.SetActive(true);

        }
        if (player.GetComponent<PlayerDances>().Dances.Contains(DanceEvent.Pose.Cool))
        {
            Dance2.gameObject.SetActive(true);

        }
        if (player.GetComponent<PlayerDances>().Dances.Contains(DanceEvent.Pose.Sick))
        {
            Dance3.gameObject.SetActive(true);
        }

    }
}
