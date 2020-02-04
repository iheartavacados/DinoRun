using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 300f;
    public static float playerHeight;
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    private GameObject player;
    private bool canJump = true;
    
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2 = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHeight = bc2.size.y;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.y, jumpVelocity);
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

    /*
    private bool isGrounded()
    {
        if (player.transform.position.y == 0.5)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            canJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            canJump = false;
        }
    }

}
