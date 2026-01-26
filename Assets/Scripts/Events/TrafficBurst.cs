using UnityEngine;

public class TrafficBurst : Events
{
    [SerializeField] private float repeatRateForSpawnForEvent;
    [SerializeField] private float timeForSpawnForEvent;
    public override void Activity()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        enemySpawner.repeatRateForSpawn = repeatRateForSpawnForEvent;
        enemySpawner.timeForSpawn = timeForSpawnForEvent;
        base.Activity();
    }

    public override void OffEvent()
    {
        base.OffEvent();
        enemySpawner.repeatRateForSpawn = 1f;
        enemySpawner.timeForSpawn = 1f;
    }
}
