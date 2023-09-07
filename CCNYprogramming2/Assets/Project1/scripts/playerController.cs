using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 1f;
    public SpriteRenderer mySprite;
    public int hitCount = 0;
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
        //OnCollisionEnter2D other stores information on the object collided with so we can check for the enemies here
        void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.name == "enemy") 
        {
            // each time the player is hit, start a coroutine to track hit count
            StartCoroutine(itsBeenHit(.2f));            
        }

   }

        private IEnumerator itsBeenHit(float waitTime)
    {

        mySprite.color = Color.red; //make the player red so we know it's been hit
        hitCount += 1; //add to the player hitCount - gameManager pulls this number to track number of hits to the player
        yield return new WaitForSeconds(waitTime);
        mySprite.color = Color.white;

    }
}
