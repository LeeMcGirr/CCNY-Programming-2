using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player3D : MonoBehaviour
{
    [Header("base vars")]
    public float speed = 1f;
    public float jumpUpSpeed = 5f;
    public GameObject myCam;

    [Header("kick vars")]
    public float kickVel = 5f;
    public float kickUpMod = 5f;
    public float kickDist = 3f;
    public Transform attackPoint;

    Rigidbody myRB;
    Vector3 myDir;
    Vector3 myLook;
    public bool jumpCalled;
    public bool jumped;
    public bool kickCalled;
    public bool kicked;

    [Header("cam vars")]
    public Vector2 camLock = new Vector2(-90f, 90f);
    float rotX;
    float rotY;

    [Header("cosmetics")]
    public MeshRenderer bodyRender;
    public MeshFilter myBodyMesh;
    public MeshRenderer hatRender;
    public MeshFilter myHatMesh;
    Material bodyMat;

    public Slider R;
    public Slider G;
    public Slider B;

    public Mesh[] myHats;
    int hatIndex;
    public Mesh[] myBodies;
    int bodyIndex;

    private Vector3 velocity = Vector3.zero;
    Vector3 groundNormal;


    [Header("Audio Vars")]
    public AudioClip[] myKicks;
    AudioSource kickSource;
    // Start is called before the first frame update
    private void Awake()
    {
        kickSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        hatIndex= 0;
        bodyIndex= 0;
        myHatMesh.mesh = myHats[0];
        myBodyMesh.mesh = myBodies[0];
        bodyMat = bodyRender.material;
        myDir = Vector3.zero;
        myLook = Vector3.zero;
        myRB = GetComponent<Rigidbody>();

        jumpCalled = false;
        jumped = false;

        kickCalled = false;
        kicked = false;

        groundNormal = Vector3.up;
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
        myLook = MouseLook();
        transform.rotation = Quaternion.Euler(0f, myLook.y, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.x, myLook.y, 0f);
        if(Input.GetKey(KeyCode.Space) && !jumped) { jumpCalled = true; }
        if(Input.GetKey(KeyCode.Return) && !kicked) { kickCalled = true; }
        
    }

    void FixedUpdate()
    {

        Vector3 newDir = transform.TransformDirection(Direction());
        Vector3 wishDir = Vector3.Cross(Vector3.Cross(groundNormal, newDir), groundNormal);
        Debug.DrawRay(transform.position, wishDir*4f, Color.black, 5f);



        myRB.AddForce(newDir*speed, ForceMode.VelocityChange);


        if (jumpCalled) { Debug.Log("Jump key pressed"); Jump(); }

        Vector3 kickDir = transform.TransformDirection(Vector3.forward);
        if (kickCalled) 
        { 
            Kick(kickDir); 
            Debug.Log("myLook: " + kickDir); 
        }

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

        if(collision.gameObject.tag == "ground")
        { groundNormal = collision.contacts[0].normal; } //don't pull the index raw like this, best to declare the array first
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

    void Kick(Vector3 dir)
    {
        bool kickHit = false;
        //declare a placeholder for the hit data
        RaycastHit hit;
        //visual debug to confirm the kick direction and magnitude
        Debug.DrawRay(attackPoint.position, dir * kickDist, Color.green, 5f);
        //the physics call itself
        if(Physics.SphereCast(attackPoint.position, 1f, dir, out hit, kickDist)) { kickHit = true; }
        //checking to see if it worked before we write more code

        Debug.Log("sent a raycast");
        kickCalled = false;
        kicked = true;
        StartCoroutine(kickCooldown());

        Vector3 hitPoint = Vector3.Lerp(hit.point, transform.position, .4f);
        Debug.DrawRay(hitPoint, Vector3.up, Color.red, 5f);
        //code if we hit something

        if (kickHit)
        {
            PlayKick();
            hit.rigidbody.AddExplosionForce(kickVel, hitPoint, 10f, kickUpMod);
        }

    }

    #region cosmetics
    public void OnSliderChange()
    {
        bodyMat.color = new Color(R.value, G.value, B.value, 1f);
    }

    public void OnHatButton()
    {
        if(hatIndex < myHats.Length-1)
        {
            hatIndex++;
            myHatMesh.mesh = myHats[hatIndex];
        }
        else { hatIndex = 0; myHatMesh.mesh = myHats[hatIndex]; }
    }
    public void onBodyButton()
    {
        if(bodyIndex< myBodies.Length-1) { bodyIndex++; myBodyMesh.mesh = myBodies[bodyIndex]; }
        else { bodyIndex = 0; myBodyMesh.mesh = myBodies[bodyIndex]; }

    }
    #endregion

    public void PlayKick()
    {
        int i = Random.Range(0, myKicks.Length - 1);
        kickSource.clip = myKicks[i]; 
        kickSource.Play();
    }

    IEnumerator kickCooldown()
    {
        yield return new WaitForSeconds(.5f);
        kicked = false;
    }

}
