using UnityEngine;
using System.Collections;

public enum Category { Ground, Face, Sky, Auto }

public class ObstacleMovement: MonoBehaviour
{
    public const float originalSpeed = 300f;
    public static float speed = 300f;
    public static float speedFactor = 25f;
    private bool original = false; 
    public Category category;

    Rigidbody2D rigidbody2d;

    public void SetOriginal()
    {
        original = true;
    }

    public bool isOriginal()
    {
        return original;
    }

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if(original)
        {
            return;
        }
        if(category == Category.Auto)
        {
            category = (Category)Random.Range(0, 3);
            print($"Auto Spawn {category}");
        }

        switch (category)
        {
            case Category.Ground:
                //transform.position = new Vector3(transform.position.x, transform.localScale.y / 2);
                break;
            case Category.Face:
                transform.position = new Vector3(transform.position.x, transform.localScale.y / 2);
                break;
            case Category.Sky:
                transform.position = new Vector3(transform.position.x, transform.localScale.y + PlayerController.playerHeight * 2f);
                break;
        }

    }

    void Update()
    {
        if(original)
        {
            return;
        }
        if(PlayerController.frozen)
        {
            rigidbody2d.velocity = new Vector2(0, 0);
            return;
        }
        rigidbody2d.velocity = new Vector2((speed / speedFactor) * -1, rigidbody2d.velocity.y);
        //transform.position = new Vector3(transform.position.x + (ObstacleMovement.speed  / speedFactor * Time.deltaTime) * -1, transform.position.y, transform.position.z);
    }
}

