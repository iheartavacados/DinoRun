using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float width;
    static GameObject c1;
    static GameObject c2;
    static Vector3 c1start = new Vector3();
    static Vector3 c2start = new Vector3();
    private static bool delayTrigger;

    void Start()
    {
        if (name == "bg1")
        {
           c1 = this.gameObject;
           c1start = transform.position;
        }
        else
        {
            c2 = this.gameObject;
            c2start = transform.position;
        }
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
        //transform.position = new Vector3(transform.position.x + (ObstacleMovement.speed * Time.deltaTime / ObstacleMovement.speedFactor) * -1, transform.position.y, transform.position.z);
        rigidbody2d.velocity = new Vector2((ObstacleMovement.speed / ObstacleMovement.speedFactor) * -1, rigidbody2d.velocity.y);
        delayTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.name == "Kill Point" && !PlayerController.frozen && !delayTrigger)
        {
            transform.position = new Vector3(transform.position.x + width * 2 - 0.1f, transform.position.y, transform.position.z);
        }
    }

    public static void resetBack()
    {
        print("background reset");
        delayTrigger = true;  // If resetting background triggers on exit, ignore it.
        c1.transform.position = c1start;
        c2.transform.position = c2start;
    }
}
