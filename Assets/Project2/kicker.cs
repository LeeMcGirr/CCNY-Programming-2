using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class kicker : NPC
{

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(curBall.transform);
        Move();
    }

    internal override void Move()
    {
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        dir *= mySpeed;
        myRB.AddForce(dir);
    }
}
