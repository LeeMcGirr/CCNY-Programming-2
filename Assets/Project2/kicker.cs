using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class kicker : NPC
{


    [Header("Kicker Vars")]
    public float attackDist = 10f;
    public float attackSpeed = 50f;
    public float attackCooldown = 30f;
    public bool canKick = true;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(curBall.transform);
    }

    private void FixedUpdate()
    {
        Move();

        //define a condition for the Kick() method to run, in this case distance to the soccer ball
        //so once we're close, the kicker will try to kick
        float distToTarget = Vector3.Distance(transform.position, myTarget.transform.position);

        //if we are able to (haven't kicked recently, not on cooldown)
        if (distToTarget < attackDist && canKick)
        {
            //startCoroutine - starting a separate loop that runs on its own timer, not like fixedUpdate/Update
            StartCoroutine(kickAction(attackCooldown));
            canKick = false;
        }
    }

    internal override void Move()
    {
        if (!canKick) { return; }
        //if we cannot kick, then we also can't move, so exit the Move() function

        Vector3 dir = transform.TransformDirection(Vector3.forward);
        dir *= mySpeed;
        myRB.AddForce(dir);
    }

    //because we're using a coroutine, kick does not need any if statements or checks
    //we should handle all our conditional logic in fixedUpdate or Update, before calling the coroutine
    internal override void Kick()
    {
        Vector3 dirTowards = myTarget.transform.position - transform.position;
        dirTowards = dirTowards.normalized;
        myRB.AddForce(dirTowards * attackSpeed);
    }

    //here's the kick with a cooldown built in!
    IEnumerator kickAction(float time)
    {
        Debug.Log("kick initiated");
        Kick();
        yield return new WaitForSeconds(time); //we wait an arbitrary [TIME] seconds
        Debug.Log("kick cooldown ENDED");
        canKick = true;
    }

}

