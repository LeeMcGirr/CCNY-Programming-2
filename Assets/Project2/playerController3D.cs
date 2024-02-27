using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class playerController3D : MonoBehaviour
{
    public float speed = 10f;
    public float lookSpeed = 100f;
    Rigidbody myRB;
    public Camera myCam;
    public float camLock;

    Vector3 myLook;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myLook = myCam.transform.forward;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerLook = myCam.transform.forward;

        //camera forward direction
        Debug.DrawRay(transform.position, playerLook*3f, Color.green);

        myLook += DeltaLook() * Time.deltaTime;

        //clamp the magnitude to keep the player from looking fully upside down
        myLook = Vector3.ClampMagnitude(myLook, camLock);

        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.y, myLook.x, 0f);
    }

    void FixedUpdate()
    {
        Vector3 pMove = transform.TransformDirection(Dir());
        myRB.AddForce(pMove * speed * Time.fixedDeltaTime);

        //player raw input - in magenta
        Debug.DrawRay(transform.position, pMove * 5f, Color.magenta);
        Debug.DrawRay(transform.position, Vector3.up, Color.magenta);

        //combined velocity of the rigidbody in black
        Debug.DrawRay(transform.position + Vector3.up, myRB.velocity.normalized*5f, Color.black);
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
            Debug.Log("delta look: " + dLook);
        }
        
        
        return dLook;
        
    }
}
