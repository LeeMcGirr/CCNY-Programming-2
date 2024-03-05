using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class playerController3D : MonoBehaviour
{
    [Header("Base Vars")]
    public float speed = 10f;
    public float lookSpeed = 100f;

    [Header("Jump Vars")]
    public float jumpForce = 50f;
    public bool canJump;
    public bool jumped;


    Rigidbody myRB;
    public Camera myCam;
    public float camLock; //maxlook up/down

    Vector3 myLook;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myLook = myCam.transform.forward;
        Cursor.lockState = CursorLockMode.Locked;
        canJump = true;
        jumped = false;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerLook = myCam.transform.forward;

        //camera forward direction
        Debug.DrawRay(transform.position, playerLook*3f, Color.green);

        myLook += DeltaLook() * Time.deltaTime;

        //clamp the magnitude to keep the player from looking fully upside down
        myLook.y = Mathf.Clamp(myLook.y, -camLock, camLock);

        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.y, myLook.x, 0f);

        //check for key and ability to jump (canJump boolean)
        if(Input.GetKey(KeyCode.Space) && canJump)
        {
            jumped = true;
        }
        else { jumped = false; }

        if (Input.GetKey(KeyCode.Return))
        {
            Kick();
        }
    }

    void FixedUpdate()
    {
        Vector3 pMove = transform.TransformDirection(Dir());
        myRB.AddForce(pMove * speed * Time.fixedDeltaTime);

        //player raw input - in magenta
        //Debug.DrawRay(transform.position, pMove * 5f, Color.magenta);
        //Debug.DrawRay(transform.position, Vector3.up, Color.magenta);

        //combined velocity of the rigidbody in black
        //Debug.DrawRay(transform.position + Vector3.up, myRB.velocity.normalized*5f, Color.black);

        if (jumped && canJump)
        {
            Jump();
        }
    }

    Vector3 Dir()
    {
        //reference Unity Input Manager virtual axes here
        //horizontal and vertical for WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 myDir = new Vector3(x, 0, z);

        //remove console clutter by only logging direction when input is pressed
        if (myDir != Vector3.zero)
        {
            Debug.Log("Player Move Dir: " + myDir);
        }

        return myDir;
    }

    Vector3 DeltaLook()
    {
        Vector3 dLook = Vector3.zero;
        float rotY = Input.GetAxisRaw("Mouse Y") * lookSpeed;
        float rotX = Input.GetAxisRaw("Mouse X") * lookSpeed;
        dLook = new Vector3(rotX, rotY, 0);

        if (dLook != Vector3.zero)
        {
            //Debug.Log("delta look: " + dLook);
        }
        
        
        return dLook;  
    }

    //add a jumpForce and flip boolean for jump request (jumped) to false
    void Jump()
    {
        myRB.AddForce(Vector3.up * jumpForce);
        jumped = false;
    }

    void Kick()
    {
        bool rayCast = Physics.Raycast(transform.position, Vector3.forward, 5f);
        Debug.Log("raycast: " + rayCast);
        Debug.DrawRay(transform.position, Vector3.forward * 5f, Color.blue);

        if(rayCast)
        {
             //code to kick the ball goes in here
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain") { canJump = true; }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain") { canJump = false; }
    }

}
