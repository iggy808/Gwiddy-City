using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class OldManDialogue : MonoBehaviour
{

    /*
     Make this generalized so that the dialogue box can receive:
       - Character Lines 
       - Character Image
       - Character Name
     Then Change the name from OldMan Dialogue to the generalized
     We'll probably pull in dialogue lines, images, and names according to gameobject tags? 
     We'll probably store the NPC's gameobject tag as a string and pull it like that 
     We can add crazy rotation and flash effects to the dialogue box as needed 
     We can also call for specific dialogues if a certain condition is met

     
    */

    public TextMeshProUGUI textMeshProUGUI;
    public string[] linesToBeTyped;
    public float textSpeed;
    private int index;
    public Image mouseImage;
    public GameObject oldMan;

    // Start is called before the first frame update
    public void Start()
    {
        textMeshProUGUI.text = string.Empty;
        mouseImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // If line is done
            if(textMeshProUGUI.text == linesToBeTyped[index])
            {
                NextLine();
                mouseImage.enabled = false;
            }
            // End typing coroutine and finish line 
            else
            {
                StopAllCoroutines();
                textMeshProUGUI.text = linesToBeTyped[index];
                mouseImage.enabled = true;
                mouseImage.transform.localScale = new Vector3(.9f, .9f, .9f);

            }
        }
    }
    public void StartDialogue()
    {
        textMeshProUGUI.text = string.Empty;

        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in linesToBeTyped[index].ToCharArray())
        {
            textMeshProUGUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        // show mouse?
        mouseImage.enabled = true;
        mouseImage.transform.localScale = new Vector3(.9f, .9f, .9f);

    }
    void NextLine()
    {
        if (index < linesToBeTyped.Length - 1)
        {
            // go to next line[index]
            index++;
            // clear the text
            textMeshProUGUI.text = string.Empty;
            // Type the next line
            StartCoroutine (TypeLine());

        }
        else
        {
            gameObject.SetActive(false);
            textMeshProUGUI.text = string.Empty;
            
            oldMan.transform.position = new Vector3(0,-10, 0);
            
        }
    }
}
