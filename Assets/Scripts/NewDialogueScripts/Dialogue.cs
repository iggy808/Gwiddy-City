using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]


// This is a class that holds the basics for the Dialogue box
public class Dialogue 
{
    // Every NPC needs a....

    // NPC name
    public string name;
    // PUT NPC IMAGE HERE!!!!
    public Sprite characterSprite;
    // Text area that has a minimum and maximum line count
    [TextArea(3,10)]
    // Sentences from NPC (Type these out in Unity Inspector)

    // COLIN: This is now obselete for the FINAL but I haven't removed it yet
    public string[] sentencesHolder;
    
    
}
