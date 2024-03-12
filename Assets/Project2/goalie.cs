using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalie : NPC
{
    // Start is called before the first frame update
    //void Start()   {     }

    // Update is called once per frame
    //void Update() { }

    internal override void Move()
    {
        float zDiff = curBall.transform.position.z - transform.position.z;
        Vector3 dir = new Vector3(0f, 0f, zDiff).normalized;
        dir *= mySpeed;
        myRB.AddForce(dir);
    }
}
