using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyCarFactory> enemyCarFactories;
    private float[] randomPosXArray = new float[3]{-2f,0,2f};
    public List<Transport> carOnRoad = new List<Transport>();
    public float timeForSpawn;
    public float repeatRateForSpawn;
    private float lastLaneEnemy;
    private void Start()
    {
        enemyCarFactories = new List<EnemyCarFactory>(Resources.LoadAll<EnemyCarFactory>("EnemyCarScriptObject"));
    }

    public void StartSpawnCar()
    { 
        InvokeRepeating(nameof(CreateCar),timeForSpawn,repeatRateForSpawn); 
        foreach (var item in carOnRoad)
        {
            if(item !=  null)
                item.currentSpeed = item.startSpeed;
        }

        for (int i = 0; i < carOnRoad.Count; i++)
        {
            if(carOnRoad[i] == null)
            {
                carOnRoad.Remove(carOnRoad[i]);
            }
        }   

    }

    public void StopSpawnCar()
    {
        CancelInvoke(nameof(CreateCar));
    }

    public void StopCarSpeed()
    {
        foreach (var item in carOnRoad)
        {
            if(item !=  null)
                item.currentSpeed = 0f;
        }  
    }
    private void CreateCar()
    {
        float randomPos = GetRandomLane();
        var factory = enemyCarFactories[Random.Range(0,enemyCarFactories.Count)];
        var transport = factory.Create();
        transport.transform.position = new Vector2(randomPos,transform.position.y);
        carOnRoad.Add(transport);
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
