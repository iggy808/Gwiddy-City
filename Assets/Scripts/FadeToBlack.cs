using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public GameObject blackOut;

    void Start()
    {
        StartCoroutine(FadeBlack(false, 1));
    }

    public void StartFade()
    {
        StartCoroutine(FadeBlack(true, 5));
    }

    public void CutFade()
    {
        StartCoroutine(FadeBlack(true, 20));
    }

    public void StopFade()
    {
        StartCoroutine(FadeBlack(false, 5));
    }

    public IEnumerator FadeBlack(bool fadeToBlack, int fadeSpeed)
    {
        Color objectColor = blackOut.GetComponent<Image>().color;
        float fadeAmount;

        if(fadeToBlack)
        {
            while (blackOut.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOut.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackOut.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOut.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}