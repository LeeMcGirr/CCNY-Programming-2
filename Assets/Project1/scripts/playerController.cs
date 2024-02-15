using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 1f;
    public SpriteRenderer mySprite;
    public int hitCount = 0;
    public float myHealth = 1000;
    Rigidbody2D myBody;

    Vector3 previousPos;
    Vector3 targetDir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        myBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        targetDir = Vector3.zero;
        //the following is a simple WASD controller
        // Input.GetKey checks to see if a key is currently pressed down, instead of GetKeyDown or GetKeyUp, which only check for the press/release
        //this uses all if() statements so multiple movement inputs can be active in a single update loop
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("W pressed");
            targetDir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("S pressed");
            targetDir += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A pressed");
            targetDir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("D pressed");
            targetDir += Vector3.right;
        }

        if(this.gameObject.transform.position == previousPos)
        {
            myHealth += 1*Time.deltaTime;
        }



        previousPos = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
        myBody.AddForce(targetDir, ForceMode2D.Impulse);
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
        myHealth -= 25;
        yield return new WaitForSeconds(waitTime);
        mySprite.color = Color.white;

    }
}
