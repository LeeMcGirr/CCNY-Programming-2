using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myGoal : MonoBehaviour
{
    public GameObject myBall;
    public GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject == myBall)
        {
            myBall.GetComponent<Rigidbody>().AddExplosionForce(500f, transform.position, 300f, 20f);
            myPlayer.GetComponent<Rigidbody>().AddExplosionForce(500f, transform.position, 300f, 20f);
        }
    }
}
