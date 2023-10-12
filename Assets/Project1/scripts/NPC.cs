using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Rigidbody myRB;
    public float NPCspeed;
    public GameObject myTarget;
    public NPC myScript;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myScript = GetComponent<NPC>();
    }
    // Update is called once per frame
    void Update()
    {
        //default update behavior is to just call move
        Move();
    }

    protected virtual void Move()
    {
        //parent class returns a debug placeholder for move
        Debug.Log("move not defined for this class");
    }

    void Jump()
    {
        Debug.Log("jump not defined");
    }

    void Kick()
    {
        Debug.Log("kick not defined");
    }
}
