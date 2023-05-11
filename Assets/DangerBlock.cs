using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerBlock : MonoBehaviour
{

    public GameObject player;
    public GameObject playerRenderer;
    public Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource source = GetComponent<AudioSource>();
            AudioClip clip = source.clip;
            source.PlayOneShot(clip);
            StartCoroutine(Flasher());
            StartCoroutine(Delay());
        }
    }

    IEnumerator Flasher()
    {
        Debug.Log("Made it");
        for (int i = 0; i < 25; i++)
        {
            playerRenderer.GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            playerRenderer.GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
    IEnumerator Delay()
    {
        
        Debug.Log("HIT PLAYER");
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return new WaitForSeconds(3f);
        player.transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, respawnPoint.position.z);
        player.GetComponent<PlayerMovement>().enabled = true;

    }
}
