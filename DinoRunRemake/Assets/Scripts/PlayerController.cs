using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 100f;
    public static float playerHeight;
    public static float playerOffset;
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
        playerOffset = bc2.offset.y;
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
                UnCrouch();
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
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            UnCrouch();
        }
    }
    private void Crouch()
    {
        bc2.size = new Vector3(bc2.size.x, playerHeight / 2);
        bc2.offset = new Vector3(bc2.offset.x, playerOffset - playerHeight / 4);
    }

    private void UnCrouch()
    {
        bc2.size = new Vector3(bc2.size.x, playerHeight);
        bc2.offset = new Vector3(bc2.offset.x, playerOffset);
    }

    private void Unfreeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Spawning.clearObstacles();
        PointSystem.resetScore();
        BackgroundMovement.resetBack();
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

    }

}
