using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player3D : MonoBehaviour
{
    [Header("base vars")]
    public float speed = 1f;
    public GameObject myCam;
    Vector3 myDir;

    [Header("cam vars")]
    public Vector2 camLock = new Vector2(-90f, 90f);
    float rotX;
    float rotY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerLook = myCam.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, playerLook.normalized, Color.green, .1f);

        //pull the WASD (or controller) by referencing Unity's default input axes options
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //construct a vector3, leaving out Y because that would be a jump
        myDir = new Vector3(x, 0f, z).normalized;
        transform.Translate(myDir * speed);
        Debug.DrawRay(transform.position, myDir * speed, Color.yellow, 5f);

        rotX += Input.GetAxisRaw("Mouse Y");
        rotY += Input.GetAxisRaw("Mouse X");

        if(rotX < camLock.x) { rotX = camLock.x; }
        if(rotX > camLock.y) { rotX = camLock.y; }

        transform.rotation = Quaternion.Euler(0f, rotY, 0f);
        myCam.transform.rotation = Quaternion.Euler(-rotX, rotY, 0f);
        
    }
}
