using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float width;
    
    Vector3 cstart = new Vector3();
    

    void Start()
    {
     
        cstart = transform.position;
       
        rigidbody2d = GetComponent<Rigidbody2D>();
        width = GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        if (PlayerController.frozen)
        {
            rigidbody2d.velocity = new Vector2(0, 0);
            return;
        }
        var cloudSpeed = (ObstacleMovement.speed / ObstacleMovement.speedFactor) / 4;
        //transform.position = new Vector3(transform.position.x + (ObstacleMovement.speed * Time.deltaTime / ObstacleMovement.speedFactor) * -1, transform.position.y, transform.position.z);
        rigidbody2d.velocity = new Vector2(cloudSpeed * -1, rigidbody2d.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.name == "Kill Point" && !PlayerController.frozen)
        {
            transform.position = cstart;
        }
    }

//    public static void resetBack()
//    {
//        delayTrigger = true;  // If resetting background triggers on exit, ignore it.
//        c1.transform.position = c1start;
//        c2.transform.position = c2start;
//    }
}
