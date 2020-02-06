using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public int speed = 5;
    Rigidbody2D rb;
    GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * -1, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            camera.transform.Rotate(0, 0, 180);
            camera.transform.localScale = new Vector3(-1, 1, 1);
            Destroy(GameObject.Find("PlusContent"));
        }
    }
}
