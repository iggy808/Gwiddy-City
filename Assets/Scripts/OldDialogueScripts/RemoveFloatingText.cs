using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFloatingText : MonoBehaviour
{

    public float destroyLineDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyLineDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
