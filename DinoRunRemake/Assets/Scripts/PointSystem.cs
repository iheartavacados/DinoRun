using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    float elapsedTime;
    int currentPoints;
    int high;
    public int speedUpFactor = 2;
    private static TextMesh PointText;
    private static TextMesh HighPointText;

    // Start is called before the first frame update
    void Start()
    {
        PointText = GameObject.Find("Points").GetComponent<TextMesh>();
        HighPointText = GameObject.Find("High Points").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

        elapsedTime += Time.deltaTime * 10;
        currentPoints = (int)elapsedTime;

        PointText.text = $"Score: {currentPoints: 00000}";

        if (PlayerController.frozen)
        {
            if (currentPoints > high)
            {
                high = currentPoints;

                HighPointText.text = $"High Score: {high: 000000}";
            }
        }

        if (currentPoints%100 == 0 && currentPoints != 0)
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

}
