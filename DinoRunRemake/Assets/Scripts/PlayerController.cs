using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//public enum LevelState { MasterContent, PlusContent }

[Flags]
public enum AnimationState
    {   Master=0, Plus=4,
        Walk = 0, Stand = 1, Crouch = 2, Dead = 3,
        WalkPlus = 4, StandPlus = 5, CrouchPlus = 6, DeadPlus = 7
    }

public class PlayerController : MonoBehaviour
{
    public bool state; // False == Master, True == Plus
    public float jumpVelocity = 100f;
    public static float playerHeight;
    public static float playerOffset;
    private Rigidbody2D rb;
    private BoxCollider2D bc2;
    private GameObject player;
    private bool canJump = true;
    internal static bool frozen;
    public Animator myAnimator;
    public AnimationState currentAnimState;
    //public Vector3 ogPos;


    private RandomContainer randomC;
    public AudioClip[] jumpClips;
    public AudioClip[] deathClips;
    public AudioClip[] scoreClips;
    public AudioClip[] transitionClips;

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
        currentAnimState = AnimationState.Walk;

        //ogPos = new Vector3(GameObject.Find("Button").transform.position.x, GameObject.Find("Button").transform.position.y, GameObject.Find("Button").transform.position.z);
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
                ChangeState(AnimationState.Dead);
                
                return;
            }
        }
        if (canJump && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.velocity = new Vector2(rb.velocity.y, jumpVelocity);
            GetComponent<AudioSource>().Play();
            canJump = false;
            ChangeState(AnimationState.Stand);

            PlayRandomSound(jumpClips);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            UnCrouch();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(state == true)
            {
                SceneManager.LoadScene("Mars");
            }
            else
            {
                SceneManager.LoadScene("MasterGame");
            }
            
        }
    }

    public void PlayRandomSound(AudioClip[] sounds)
    {
        if (randomC != null)
        {
            randomC.clips = sounds;
            randomC.PlaySound(state == false);
        }
        else
        {
            print("Random Container Not Attached to Player");
        }
    }
    

    private void Crouch()
    {
        bc2.size = new Vector3(bc2.size.x, playerHeight / 2);
        bc2.offset = new Vector3(bc2.offset.x, playerOffset - playerHeight / 4);
        ChangeState(AnimationState.Crouch);
    }

    private void UnCrouch()
    {
        bc2.size = new Vector3(bc2.size.x, playerHeight);
        bc2.offset = new Vector3(bc2.offset.x, playerOffset);
        ChangeState(AnimationState.Walk);
    }
    
    public void Unfreeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        Spawning.clearObstacles();
        PointSystem.resetScore();

        frozen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            canJump = true;
            ChangeState(0);
        }
        if(collision.gameObject.name == "UFO")
        {
            PlayRandomSound(transitionClips);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if player collides with an object, 
        if (collision.gameObject.tag == "Obstacle")
        {
            //freeze moving components
            frozen = true;
            rb.velocity = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            PlayRandomSound(deathClips);

            UnCrouch();
            //ChangeState(AnimationState.Walk);


            //ChangeState(AnimationState.Dead);
        }

    }
   
    void ChangeState(AnimationState animationState)
    {
        if (state == true)
        {
            animationState |= AnimationState.Plus;            
        }
        myAnimator.SetInteger("animState", (int)animationState);

        currentAnimState = animationState;
    }
}
