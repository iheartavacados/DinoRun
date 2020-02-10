using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Transform BG2;
    float width;
    static GameObject bg1;
    static GameObject bg2;
    static Vector3 bg1start = new Vector3();
    static Vector3 bg2start = new Vector3();
    private static int delayTrigger;

    void Start()
    {
        if(name == "bg1")
        {
            bg1 = this.gameObject;
            bg1start = transform.position;
        }
        else
        {
            bg2 = this.gameObject;
            bg2start = transform.position;
        }
        rigidbody2d = GetComponent<Rigidbody2D>();
        width = GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        if (delayTrigger > 0)
            delayTrigger--;
        if (PlayerController.frozen)
        {
            rigidbody2d.velocity = new Vector2(0, 0);
            return;
        }
        //transform.position = new Vector3(transform.position.x + (ObstacleMovement.speed * Time.deltaTime / ObstacleMovement.speedFactor) * -1, transform.position.y, transform.position.z);
        rigidbody2d.velocity = new Vector2((ObstacleMovement.speed / ObstacleMovement.speedFactor) * -1, rigidbody2d.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.name == "Kill Point" && !PlayerController.frozen && delayTrigger == 0)
        {
            transform.position = new Vector3(transform.position.x + width * 2 - 0.1f, transform.position.y, transform.position.z);
        }
    }

    public static void resetBack()
    {
        print("background reset");
        delayTrigger = 3;  // If resetting background triggers on exit, ignore it for first 3 update loops...
        bg1.transform.position = bg1start;
        bg2.transform.position = bg2start;
    }
}
