using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject myPlayer;
    public float maxDistDelta = .01f;
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

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "MyGameObjectName"){
            
        }

   }
}
