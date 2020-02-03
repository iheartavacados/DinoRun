using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObstacle : MonoBehaviour
{
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObstacleMovement obstacle;
        //Passing a parameter out (If something has ObstacleMovement, then set it as obstacle, and destroy it)
        if (collision.gameObject != null && collision.gameObject.TryGetComponent<ObstacleMovement>(out obstacle))
        {
            Destroy(collision.gameObject);
        }
    }
}
