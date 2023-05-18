using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerVolume : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        audioSource.enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        audioSource.enabled=true;
    }
}
