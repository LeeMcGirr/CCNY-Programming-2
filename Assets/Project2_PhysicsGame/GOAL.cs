using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAL : MonoBehaviour
{
    public AudioSource scored;
    // Start is called before the first frame update
    void Start()
    {
        scored = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit: " + other.gameObject.tag);
        if(other.gameObject.tag == "ball")
        {
            scored.Play();
        }
    }
}
