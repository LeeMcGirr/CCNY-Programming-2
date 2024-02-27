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

    Vector3 myLook;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myLook = myCam.transform.forward;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerLook = myCam.transform.forward;
        Debug.DrawRay(transform.position, playerLook, Color.white);
        myLook += DeltaLook() * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.y, myLook.x, 0f);
    }

    void FixedUpdate()
    {
        Vector3 pMove = Dir();
        myRB.AddForce(pMove * speed * Time.fixedDeltaTime);
        Debug.DrawRay(transform.position, pMove * 5f, Color.magenta);
        Debug.DrawRay(transform.position, myRB.velocity.normalized*5f, Color.black);
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
