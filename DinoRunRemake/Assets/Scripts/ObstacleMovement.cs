using UnityEngine;
using System.Collections;

public class ObstacleMovement: MonoBehaviour
{
    public float speed = 3f;

    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(speed * -1, rigidbody2d.velocity.y);
    }
}

