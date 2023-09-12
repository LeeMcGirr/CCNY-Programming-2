using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject myPlayer;
    public float maxDistDelta = .01f;
    public Rigidbody2D myBody;
    public float bounceMult = 100f;
    Vector3 dirTowards = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    // FixedUpdate is called at a fixed time interval set by the project (default in Unity is 0.02 seconds)
    void FixedUpdate()
    {

    }

    void LateUpdate()
    {
        //get playerPos then move the enemy towards it, change speed with DistDetla
        Vector3 playerPos = myPlayer.transform.position;
        Vector3 targetPos = Vector3.Lerp(playerPos, this.transform.position, .5f);
        //the greater the maxDistDelta, the faster the enemy moves each frame
        transform.position = Vector3.MoveTowards(transform.position, playerPos, maxDistDelta);
    }

    //OnCollisionEnter2D is called each time the collider attached to the object collides with any other collider
    void OnCollisionStay2D(Collision2D other) {

        if(other.gameObject.name == "player1") //collision2D other stores information on the object collided with so we can check for the player here
        {
            //to get the vector between two objects, 
            dirTowards = this.transform.position - other.gameObject.transform.position;
            dirTowards = dirTowards.normalized;
            Debug.DrawRay(this.transform.position, dirTowards, Color.cyan, 2f);
            Debug.Log(dirTowards.magnitude);
            //AddForceAtPosition will add a force originating from a location, in this case the player, to throw the enemy out
            myBody.AddForceAtPosition( dirTowards*bounceMult , myPlayer.transform.position);
            Debug.Log("force");
        }

        else if(other.gameObject.name == "enemy") 
        { 
            Destroy(this.gameObject); //destroy on contact with another enemy
        }

   }

   public void SetPlayer(GameObject player) // using Set() and Get() functions can help keep our objects correctly referencing each other during runtime
   {
        myPlayer = player;
   }
   
   }
