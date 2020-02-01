using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 300f;
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
    }

    // Update is called once per frame
    private void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.y, jumpVelocity);
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
