using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    //public int speed = 5;
    public static bool isUpsideDown;
    static float flipDurationTime = 15f;
    static float flipBackTime = 0f;
    Rigidbody2D rb;
    static GameObject levelCamera;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelCamera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
       if(isUpsideDown && flipBackTime < Time.time)
        {
            FlipGravity();
            flipBackTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            FlipGravity();
            flipBackTime = Time.time + flipDurationTime;
  
        }
    }

    public static void FlipGravity()
    {
        levelCamera.transform.Rotate(0, 0, 180);
        levelCamera.transform.localScale = new Vector3(-1, 1, 1);
        isUpsideDown = !isUpsideDown;
    }
}
