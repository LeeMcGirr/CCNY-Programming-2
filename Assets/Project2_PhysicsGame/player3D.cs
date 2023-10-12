using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player3D : MonoBehaviour
{
    [Header("base vars")]
    public float speed = 1f;
    public float jumpUpSpeed = 5f;
    public GameObject myCam;

    Rigidbody myRB;
    Vector3 myDir;
    public bool jumpCalled;
    public bool jumped;

    [Header("cam vars")]
    public Vector2 camLock = new Vector2(-90f, 90f);
    float rotX;
    float rotY;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        jumpCalled = false;
        jumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        //visual debug in scene editor to verify dir forward
        Vector3 playerLook = myCam.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, playerLook.normalized*2f, Color.green, .1f);

        //visual debug for left/right as well
        Vector3 playerLeft = transform.TransformDirection(Vector3.left);
        Debug.DrawRay(transform.position, playerLeft.normalized, Color.yellow, .1f);
        //the same for right - use TransformDirection to convert Vector3.right from local transform to world space
        Vector3 playerRight = transform.TransformDirection(Vector3.right);
        Debug.DrawRay(transform.position, playerRight.normalized, Color.yellow, .1f);

        //for now we'll leave the look rotations in here - next class we're going to split out the camera update into late update
        Vector3 myLook = MouseLook();
        transform.rotation = Quaternion.Euler(0f, myLook.y, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.x, myLook.y, 0f);
        if(Input.GetKey(KeyCode.Space) && !jumped) { jumpCalled = true; }
        
    }

    void FixedUpdate()
    {

        Vector3 newDir = transform.TransformDirection(Direction());
        Debug.DrawRay(transform.position, newDir*4f, Color.black, 5f);
        myRB.AddForce(newDir*speed, ForceMode.VelocityChange);


        if (jumpCalled) { Debug.Log("Jump key pressed"); Jump(); }

    }

    void LateUpdate()
    {
        //WIP DO NOT ENABLE YET ***********************************
        //Vector3 myLook = MouseLook();
        //myLook.z = myCam.transform.rotation.z;
        //Vector3 currentLook = new Vector3(myCam.transform.rotation.x, myCam.transform.rotation.y, myCam.transform.rotation.z);
    }

    void OnCollisionStay(Collision collision)
    {
        jumped = false;
    }

    Vector3 Direction()
    {
        Vector3 myDir;
        //pull the WASD (or controller) by referencing Unity's default input axes options
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //construct with 0f for Y so it doesn't make the player jump
        myDir = new Vector3(x, 0f, z).normalized;
        return myDir;
    }

    Vector3 MouseLook()
    {
        Vector3 myLook;
        //call the mouse axis binds from Unity default settings
        rotX += Input.GetAxisRaw("Mouse Y");
        rotY += Input.GetAxisRaw("Mouse X");

        //we want to lock the up/down camera rot for better 3rd person tracking
        if (rotX < camLock.x) { rotX = camLock.x; }
        if (rotX > camLock.y) { rotX = camLock.y; }

        myLook = new Vector3(rotX, rotY, 0f);

        return myLook;
    }

    void Jump()
    {
        myRB.AddForce(Vector3.up * jumpUpSpeed, ForceMode.VelocityChange);
        jumpCalled = false;
        jumped = true;
    }

}
