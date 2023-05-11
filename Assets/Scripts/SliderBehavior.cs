using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class SliderBehavior : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public PlayerCam firstPerson;
    public CinemachineFreeLook thirdPerson;

    void Start()
    {
        if (!PlayerPrefs.HasKey("sensPref"))
        {
            PlayerPrefs.SetFloat("sensPref", 50f);
            Load("Sens");
        }
        else
        {
            Load("Sens");
        }

        if (!PlayerPrefs.HasKey("volPref"))
        {
            PlayerPrefs.SetFloat("volPref", 50f);
            Load("Vol");
        }
        else
        {
            Load("Vol");
        }

        slider.onValueChanged.AddListener((value) =>
        {
            if (gameObject.name == "Sensitivity")
            {
                if (firstPerson != null)
                {
                    firstPerson.sensX = (20f / 50f * value);
                    firstPerson.sensY = (20f / 50f * value);
                }
                if (thirdPerson != null)
                {
                    thirdPerson.m_XAxis.m_MaxSpeed = (3000f / 50f * value);
                    thirdPerson.m_YAxis.m_MaxSpeed = (80f / 50f * value);
                }
                Save("Sens");
            }
            else if (gameObject.name == "Volume")
            {
                AudioListener.volume = slider.value;
                Save("Vol");
            }
            sliderText.text = value.ToString();
        });
    }

    private void Save(string arg)
    {
        if (arg == "Sens")
            PlayerPrefs.SetFloat("sensPref", slider.value);
        else if (arg == "Vol")
            PlayerPrefs.SetFloat("volPref", slider.value);
    }

    private void Load(string arg)
    {
        if (arg == "Sens")
            slider.value = PlayerPrefs.GetFloat("sensPref");
        else if (arg == "Vol")
            slider.value = PlayerPrefs.GetFloat("volPref");
    }
}
