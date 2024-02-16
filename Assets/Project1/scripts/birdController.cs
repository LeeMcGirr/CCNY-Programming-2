using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{
    public float speed = 1f;
    public SpriteRenderer mySprite;
    public bool dead = false;
    public float myHealth = 1000;
    Rigidbody2D rb;


    Vector3 previousPos;
    Vector3 myDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {

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
        rb.velocity = Vector3.zero;
        transform.Translate(new Vector3(Dir().x,0,0) * speed);
        if(Input.GetKey(KeyCode.Space))
        {
            transform.Translate(new Vector3(0, 1*speed, 0));
        }
    }
        //OnCollisionEnter2D other stores information on the object collided with so we can check for the enemies here
    void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.tag == "enemy") 
        {
            // each time the player is hit, execute code here
            dead = true;
        }

    }
}
