using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerVolume : MonoBehaviour
{
    public Collider player;
    public AudioSource audioSource;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other == player)
        {
            audioSource.enabled = false;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == player)
        {
                    audioSource.enabled=true;

        }
    }
}
