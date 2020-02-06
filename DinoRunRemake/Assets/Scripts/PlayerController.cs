using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 100f;
    public static float playerHeight;
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    private GameObject player;
    private bool canJump = true;
    internal static bool frozen;



    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2 = GetComponent<BoxCollider2D>();
        GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHeight = bc2.size.y;
    }

    private void Update()
    {
        
        if(frozen)
        {
            if (Input.anyKeyDown)
            {
                Unfreeze();
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                transform.localScale = new Vector3(transform.localScale.x, 2, transform.localScale.z);
                return;
            }
        }
        if (canJump && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.velocity = new Vector2(rb.velocity.y, jumpVelocity);
            GetComponent<AudioSource>().Play();
            canJump = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {//Rewrite this code when animation is in place * * * * * * * !!!!!!!!!!!!!
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
        }
    }

    private void Unfreeze()
    {
        
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        frozen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            canJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if player collides with an object, 
        if (collision.gameObject.tag == "Obstacle")
        {
            print("player hit");
            //freeze moving components
            frozen = true;
        }
 
        //press space bar  
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            //reset position based on previous position
        }*/
    }

}
