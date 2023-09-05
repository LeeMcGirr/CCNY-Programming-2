using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject myPlayer;
    public float maxDistDelta = .01f;
    public Rigidbody2D myBody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get playerPos then move the enemy towards it, change speed with DistDetla
        Vector3 playerPos = myPlayer.transform.position; 
        transform.position = Vector3.MoveTowards(transform.position, playerPos, maxDistDelta);
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.name == "player1")
        {
            myBody.AddForceAtPosition(new Vector3 (1000,1000,1000), myPlayer.transform.position);
            Debug.Log("force");
        }

   }
}
