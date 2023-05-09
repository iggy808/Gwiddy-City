using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Button Dance1;
    public Button Dance2;
    public Button Dance3;
    public Button Dance4;
    public Button Dance5;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Dance1.gameObject.SetActive(false);
        Dance2.gameObject.SetActive(false);
        Dance3.gameObject.SetActive(false);
        Dance4.gameObject.SetActive(false);
        Dance5.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerDances>().Dances.Contains(DanceEvent.Pose.Splits))
        {
            Dance1.gameObject.SetActive(true);

        }
        if (player.GetComponent<PlayerDances>().Dances.Contains(DanceEvent.Pose.Cool))
        {
            Dance2.gameObject.SetActive(true);

        }

    }
}
