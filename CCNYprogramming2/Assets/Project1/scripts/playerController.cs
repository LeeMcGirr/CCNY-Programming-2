using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 1f;
    bool beenHit = false;
    //public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //the following is a simple WASD controller
        // Input.GetKey checks to see if a key is currently pressed down, instead of GetKeyDown or GetKeyUp, which only check for the press/release
        //this uses all if() statements so multiple movement inputs can be active in a single update loop
        if(Input.GetKey(KeyCode.W))
        {
            //Debug.Log("W pressed");
            transform.Translate(Vector3.up * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("S pressed");
            transform.Translate(Vector3.down * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A pressed");
            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("D pressed");
            transform.Translate(Vector3.right * speed);
        }
    }

        void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.name == "enemy") //collision2D other stores information on the object collided with so we can check for the player here
        {
           
        }


   }

        private IEnumerator itsBeenHit(float waitTime)
    {
        //flip our enemySpawned boolean to false to disable additional spawns until after this one completes
        beenHit = true;

        yield return new WaitForSeconds(waitTime);


        beenHit = false;
        //re-enable the enemy spawn function

    }
}
