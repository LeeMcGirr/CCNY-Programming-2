using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Rigidbody myRB;
    public float mySpeed;
    public GameObject curPlayer;
    public GameObject curBall;
    public GameObject myTarget;
    public NPC myScript;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        curPlayer = GameObject.FindWithTag("Player");
        curBall = GameObject.FindWithTag("Ball");
        myScript = GetComponent<NPC>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    internal virtual void Move()
    {
        Debug.Log("move not defined for this class");
    }

    internal virtual void Jump()
    {
        Debug.Log("jump not defined");
    }

    internal virtual void Kick()
    {
        Debug.Log("kick not defined");
    }
}
