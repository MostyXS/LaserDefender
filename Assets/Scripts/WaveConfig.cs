using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyWaveConfig")] 
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBeetwenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWaypoints()
    {
        var WaveWaypoints = new List<Transform>();
        foreach(Transform child in  pathPrefab.transform)
        {
            WaveWaypoints.Add(child);

        }

        return WaveWaypoints;
    }
    public float GettimeBeetwenSpawns()
    {
        return timeBeetwenSpawns;
    }
    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }
    public int GetnumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }



   
}
