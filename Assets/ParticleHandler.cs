using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public GameObject self;
    public GameObject oldManCall;
    // Start is called before the first frame update
    void Start()
    {
        self.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (oldManCall.transform.position.y <= -10)
        {
            print("AHHHHHHHHHHHHHHHHHHH");
            self.SetActive(true);
        }
    }
}
