using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawning : MonoBehaviour
{
    //Can alter the size, you can add as many "obstacle types" as needed
    string[] ObstacleType = {"Cacti1", "Cacti2", "Cacti3", "Cacti4", "Bird"};
    private int numOfCacti = 4;
    public float spawnGroup = 0.2f; //Likelihood of spawning a group
    public float groupOffset = 0.3f;//How far apart to spawn each object within the group
    GameObject[] MasterObjects;
    public float minDistance = 300;
    public float maxDistance = 900;
    private float nextDistance;
    private GameObject spawnPoint;
    
    private void Awake()
    {
        //Find the Game Object for each obstacle type

        MasterObjects = new GameObject[ObstacleType.Length];

        for(int i = 0; i < ObstacleType.Length; i++)
        {
            var temp = GameObject.Find(ObstacleType[i]);
            MasterObjects[i] = temp;
            temp.GetComponent<ObstacleMovement>().SetOriginal();
        }
        spawnPoint = GameObject.Find("SpawnPoint");

    }

    void Start()
    {
        nextDistance = Random.Range(minDistance, maxDistance);
    }

    void Update()
    {
        if(!PlayerController.frozen)
        {
            nextDistance -= ObstacleMovement.speed * Time.deltaTime;
            if (nextDistance <= 0)
            {
                Spawn();
                nextDistance = Random.Range(minDistance, maxDistance);
            }
        }
        
    }
    
    void Spawn()
    {
        //Spawn a group of cacti
        if (Random.Range(0f, 1f) < spawnGroup) 
        {
            int spawnCount = Random.Range(2, 4);
            for(int spawnIndex = 0; spawnIndex < spawnCount; spawnIndex++)
            {
                int spawnCacti = Random.Range(0, numOfCacti);

                GameObject spawner = MasterObjects[spawnCacti];
                Vector3 spawnAt = new Vector3(spawnPoint.transform.position.x + spawnIndex * groupOffset, spawner.transform.position.y, 0);
                spawner = Instantiate(spawner, spawnAt, Quaternion.identity);
            }
        }
        else
        {
            int spawnIndex = Random.Range(0, MasterObjects.Length);

            GameObject spawner = MasterObjects[spawnIndex];

            Vector3 spawnAt = new Vector3(spawnPoint.transform.position.x, spawner.transform.position.y, 0);
            spawner = Instantiate(spawner, spawnAt, Quaternion.identity);
        }
        
    }

    internal static void clearObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for(int i = 0; i < obstacles.Length; i++)
        {
            var temp = obstacles[i];
            if(!temp.GetComponent<ObstacleMovement>().isOriginal())
            {
                Destroy(temp);
            }
        }
    }
}
