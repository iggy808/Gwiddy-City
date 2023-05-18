using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject controlUI;
    public PlayerCam firstPerson;
    public CinemachineFreeLook thirdPerson;
    public PlayerEngaged engaged;
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
        if (firstPerson != null)
            firstPerson.enabled = false;
        if (thirdPerson != null)
            thirdPerson.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void UnPause()
    {
        if (firstPerson != null)
            firstPerson.enabled = true;
        if (thirdPerson != null)
            thirdPerson.enabled = true;
        if (!engaged.battleEngaged)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public async void Quit()
    {
        UnPause();
        GameObject fade = GameObject.FindWithTag("FadeScreen");
        fade.GetComponent<FadeToBlack>().StartFade();
        await Task.Delay(1000);
        SceneManager.LoadScene("Start Screen");
    }

    public void Return()
    {
        menuUI.SetActive(true);
        controlUI.SetActive(false);
    }

    public void Controls()
    {
        menuUI.SetActive(false);
        controlUI.SetActive(true);
    }
}
