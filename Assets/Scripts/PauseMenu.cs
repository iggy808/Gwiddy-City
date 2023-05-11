using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;
    public PlayerCam playerCam;
    public bool paused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                UnPause();
            else
                Pause();
        }    
    }

    public void Pause()
    {
        playerCam.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void UnPause()
    {
        playerCam.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Return()
    {
        UnPause();
        //SceneManager.LoadScene("Main Menu");
    }
}
