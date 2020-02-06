using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    float elapsedTime;
    int points;
    int high;
    public int speedUpFactor = 2;
    private static TextMesh PointText;

    // Start is called before the first frame update
    void Start()
    {
        PointText = GameObject.Find("Points").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.frozen)
        {
            //if game pauses, reset regular score
            //update text to keep highest score
            return;
        }
        elapsedTime += Time.deltaTime * 10;
        points = (int)elapsedTime;
        
        //if (points > high)
        //{
        //    high = points;
        //}

        PointText.text = $"High: {high:00000}   Score: {points:00000}";

        if(points%100 == 0 && points != 0)
        {
            FlashScore();
            SpeedUp();
        }
    }

    void FlashScore()
    {
        //Input 100~!! animation score
    }

    void SpeedUp()
    {
        ObstacleMovement.speed += speedUpFactor;
    }

    void ResetScore()
    {
        
        
    }
}
