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
    public Animator myAnimator;
    public enum LevelChange { MasterContent, PlusContent };

    private RandomContainer randomC;
    public AudioClip[] jumpClips;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2 = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHeight = bc2.size.y;
        playerOffset = bc2.offset.y;
        randomC = GetComponent<RandomContainer>();
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
            ChangeState(1);

            randomC.clips = jumpClips;
            randomC.PlaySound(false);
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
        //bc2.size = new Vector3(bc2.size.x, playerHeight / 2);
        //bc2.offset = new Vector3(bc2.offset.x, playerOffset - playerHeight / 4);
        ChangeState(2);
    }

    private void UnCrouch()
    {
        //bc2.size = new Vector3(bc2.size.x, playerHeight);
        //bc2.offset = new Vector3(bc2.offset.x, playerOffset);
        ChangeState(0);
    }

    private void Unfreeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Spawning.clearObstacles();
        PointSystem.resetScore();
        //BackgroundMovement.resetBack();
        frozen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            canJump = true;
            ChangeState(0);
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
            ChangeState(3);
        }

    }

    void ChangeState(int state)
    {
        switch(state)
        {
            case 0:
                myAnimator.SetInteger("animState", 0);
                break;

            case 1:
                myAnimator.SetInteger("animState", 1);
                break;

            case 2:
                myAnimator.SetInteger("animState", 2);
                break;

            case 3:
                myAnimator.SetInteger("animState", 3);
                break;

            case 4:
                myAnimator.SetInteger("animState", 4);
                break;

            case 5:
                myAnimator.SetInteger("animState", 5);
                break;

            case 6:
                myAnimator.SetInteger("animState", 6);
                break;

            case 7:
                myAnimator.SetInteger("animState", 7);
                break;

        }
    }
}
