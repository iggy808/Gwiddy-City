using HighScore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameEnd : MonoBehaviour
{

    public GameObject player;
    public GameObject hud;
    public GameObject end;
    public TMP_InputField inputField;
    private string userName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // freeze movement 
            player.gameObject.GetComponent<PlayerMovement>().enabled = false;
            // disable hud 
            hud.SetActive(false);


            // Show keyboard
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            // Get user name 
            end.SetActive(true);
            // Send score 

            // Ty is the best 
            // Okay later in the game, get the player to input their name 
            // Ty used a keyboard on his screen and used the output from that
            // THen we can just grab the score (likely from a script on the PlayerCharacter)
            // DO NOT PUT IT IN AN UPDATE FUNCTION
        }
    }
	*/
    public void GetName()
    {

    }
    public void EndTheGame()
    {
            player.gameObject.GetComponent<PlayerMovement>().enabled = false;
            // disable hud 
            hud.SetActive(false);


            // Show keyboard
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            // Get user name 
            end.SetActive(true);
            // Send score 
    }

	public void ForRealEndTheGame()
	{
        userName = inputField.GetComponent<TMP_InputField>().text;
        HS.SubmitHighScore(this, userName, player.GetComponent<PlayerOrbCollection>().total);
        SceneManager.LoadScene("End Screen");
	}
}
