using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public async void ChangeSceneToTutorial()
    {
        GameObject fade = GameObject.FindWithTag("FadeScreen");
        fade.GetComponent<FadeToBlack>().StartFade();
        await Task.Delay(1000);
        SceneManager.LoadScene("TutorialLevel");
    }
    public async void ChangeSceneToTitle()
    {
        GameObject fade = GameObject.FindWithTag("FadeScreen");
        fade.GetComponent<FadeToBlack>().StartFade();
        await Task.Delay(1000);
        SceneManager.LoadScene("Start Screen");
    }
}
