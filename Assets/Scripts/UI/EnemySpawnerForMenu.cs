using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerForMenu : MonoBehaviour
{
    
    [SerializeField] private List<EnemyCarFactory> enemyCarFactories;
    private float[] randomPosXArray = new float[3]{-2f,0,2f};
    [SerializeField] private float timeForSpawn;
    [SerializeField] private float repeatRateForSpawn;
    private float lastLaneEnemy;
    private void Start()
    {
        StartSpawnCar();
    }

    public void StartSpawnCar()
    { 
        InvokeRepeating(nameof(CreateCar),timeForSpawn,repeatRateForSpawn);

    }

    private void CreateCar()
    {
        float randomPos = GetRandomLane();
        var factory = enemyCarFactories[Random.Range(0,enemyCarFactories.Count)];
        var transport = factory.Create();
        transport.transform.position = new Vector2(randomPos,transform.position.y);
        lastLaneEnemy = randomPos;
    }


    private float GetRandomLane()
    {
        float randomPos;
        do
        {
            randomPos = randomPosXArray[Random.Range(0,randomPosXArray.Length)];
        }
        while(lastLaneEnemy == randomPos);
        return randomPos;
    }
}

