using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSceneToTutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
    }
    public void ChangeSceneToTitle()
    {
        SceneManager.LoadScene("Start Screen");
    }
}
