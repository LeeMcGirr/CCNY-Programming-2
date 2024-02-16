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
    Vector3 myDir;
    // Start is called before the first frame update
    void Start()
    {
        myBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position == previousPos)
        {
            myHealth += 1*Time.deltaTime;
        }
        previousPos = this.gameObject.transform.position;
    }

    public Vector3 Dir()
    {
        //referencing Unity's virtual axis - these pick up KBM OR controller inputs
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        myDir = new Vector3(x, y, 0); //combining them into one vector
        Debug.Log(myDir);
        return myDir; //return the value
    }

    void FixedUpdate()
    {
        myBody.AddForce(Dir(), ForceMode2D.Impulse);
    }
        //OnCollisionEnter2D other stores information on the object collided with so we can check for the enemies here
    void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.tag == "enemy") 
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
