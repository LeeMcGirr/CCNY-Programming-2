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

    [Header("Kick Vars")]
    public Transform myFoot;
    public float kickForce = 50f;
    public float upForce = 10f;
    public float legLength = 5f;


    Rigidbody myRB;
    public Camera myCam;
    public float camLock; //maxlook up/down

    Vector3 myLook;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        myLook = transform.localEulerAngles;
    }


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        canJump = true;
        jumped = false;
        //get the current mouse position
        //zero out our rotations based off that value

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
        RaycastHit hit;
        bool rayCast = false; ;
        //bool rayCast = Physics.Raycast(myFoot.position, myCam.transform.forward, out hit, 5f);
        if (Physics.SphereCast(myFoot.position, 1f, myCam.transform.position, out hit, legLength)) { rayCast = true; }
        Debug.DrawRay(myFoot.position, myCam.transform.forward * legLength, Color.blue);
        Debug.Log("raycast: " + hit);

        if(rayCast)
        {
            hit.rigidbody.AddExplosionForce(kickForce,hit.point,legLength,upForce);
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
