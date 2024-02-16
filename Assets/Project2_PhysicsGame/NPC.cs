using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //NPC class is a generic rigidbody AI controller
    public Rigidbody myRB;
    public float NPCspeed;
    //target is used for aim and tracking uses
    public GameObject myTarget;
    public NPC myScript;
    // Start is called before the first frame update
    void Start()
    {
        //find the rigidbody and this script
        myRB = GetComponent<Rigidbody>();
        //GetComponent will find any of a type NPC, including subclasses
        myScript = GetComponent<NPC>();
    }
    // Update is called once per frame
    void Update()
    {
        //default update behavior is to just call move
        Move();
    }

    //our custom functions must be at least protected to be accessed and editable by subclasses
    //virtual declares that this method is allowed to be overridden, otherwise the subclass cannot edit it
    internal virtual void Move()
    {
        //parent class returns a debug placeholder for move
        Debug.Log("move not defined for this class");
    }

    protected virtual void Jump()
    {
        Debug.Log("jump not defined");
    }

    protected virtual void Kick()
    {
        Debug.Log("kick not defined");
    }
}
