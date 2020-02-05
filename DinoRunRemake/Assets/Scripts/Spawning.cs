using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    //Can alter the size, you can add as many "obstacle types" as needed
    string[] ObstacleType = {"Obstacle", "Obstacle V2", "Obstacle V3"};
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
        GameObject spawner = MasterObjects[Random.Range(0, MasterObjects.Length)];

        spawner = Instantiate(spawner, spawnPoint.transform.position, Quaternion.identity);
    }
}
