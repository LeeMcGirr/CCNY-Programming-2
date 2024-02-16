using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallMove : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //make the scale anywhere from 1 to 1.3 times start size
        this.transform.localScale = this.transform.localScale + Random.insideUnitSphere * .3f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(1 * speed, 0, 0);        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);   
        }
    }
}
