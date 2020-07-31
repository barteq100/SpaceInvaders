using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public List<SwarmController> EnemiesToSpawn;
    public MapLimits SpawnerLimits;
    public SwarmController SpawnedSwarm;
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        var swarmPrefab = Random.Range(0, EnemiesToSpawn.Count);
        var spawned =GameObject.Instantiate(EnemiesToSpawn[swarmPrefab], getSpawnPosition(), Quaternion.identity);
        SpawnedSwarm = spawned;
        spawned.OnAllInvadersDeath += OnAllInvadersDeath;
    }

    Vector3 getSpawnPosition()
    {
        return new Vector3(
            transform.position.x + Random.Range(SpawnerLimits.min.x, SpawnerLimits.max.x),
            transform.position.y + Random.Range(SpawnerLimits.min.y, SpawnerLimits.max.y)
        );
    }

    void OnAllInvadersDeath()
    {
        Destroy(SpawnedSwarm.gameObject);
        Spawn();
    }
}