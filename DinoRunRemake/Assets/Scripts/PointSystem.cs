﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    static float elapsedTime;
    static int currentPoints;
    static int high;
    public int speedUpFactor = 1;
    private static TextMesh PointText;
    private static TextMesh HighPointText;
    private static TextMesh GameOver;
    //GameObject button;
    //public Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        PointText = GameObject.Find("Points").GetComponent<TextMesh>();
        HighPointText = GameObject.Find("High Points").GetComponent<TextMesh>();
        GameOver = GameObject.Find("GameOver").GetComponent<TextMesh>();
        HighPointText.text = "";
        GameOver.text = "";
        //button = GameObject.Find("Button"); //position needs to be set to -0.75, 2.5
        //endPos = button.transform.position;
    }

   // Update is called once per frame
    void Update()
    {
        if (PlayerController.frozen)
        {
            GameOver.text = "G A M E  O V E R";
            

            if (currentPoints > high)
            {
                high = currentPoints;
 
                HighPointText.text = $"Hi: {high: 00000}";
            }

            if(Gravity.isUpsideDown)
            {
                Gravity.FlipGravity();
            }
            return;
        }

        elapsedTime += Time.deltaTime * 10;
        currentPoints = (int)elapsedTime;

        PointText.text = $"{currentPoints: 00000}";

        if (currentPoints%100 == 0 && currentPoints != 0)
        {
            FlashScore();
            SpeedUp();
        }
    }

     void FlashScore()
    {
        var currPlayer = GameObject.Find("Player");
        var controller = currPlayer.GetComponent<PlayerController>();

        controller.PlayRandomSound(controller.scoreClips);
    }

    void SpeedUp()
    {
        if(currentPoints <= 1000)
        {
            ObstacleMovement.speed += speedUpFactor;
        }       

    }

    internal static void resetScore()
    {
        elapsedTime = 0;
        GameOver.text = "";
        ObstacleMovement.speed = ObstacleMovement.originalSpeed;
    }
}
