using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalie : NPC
{

    //defining our own update or start here will override the update function from the NPC class
    //for now, leave these commented out to better practice/test how inheritance works
    //void Start() {}
    //void Update() {}
    
    //because the base NPC Move() is protected, we must also make this override protected
    //override states that this is a new behavior that will override the base NPC Move()
    protected override void Move()
    {
        //goalie tracks the ball
        Vector3 wishDir = myTarget.transform.position 
                        - transform.position;
        Debug.DrawRay(transform.position, wishDir, Color.green, 1f);
        //normalize the force so we can multiply by our own speed
        wishDir = wishDir.normalized * NPCspeed;

        //put some code in here to make sure the goalie
        //stays close to the goal to block shot attempts

        //add force finally
        myRB.AddForce(wishDir, ForceMode.Acceleration);
    }

}
