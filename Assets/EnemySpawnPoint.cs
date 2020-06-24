using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public List<SpawnableEnemy> EnemiesToSpawn;
  
    public List<SpawnConfig> SpawnConfigs;
    public MapLimits SpawnerLimits;
    
    private int SpawnedEnemiesCount = 0;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        foreach (var spawnConfig in SpawnConfigs)
        {
            var tier = spawnConfig.tier;
            var spawnableEnemies = EnemiesToSpawn.Where(x => x.tier == tier).ToArray();
            for (var i = 0; i < spawnConfig.amountToSpawn; i++)
            {
                var enemy = PickRandomFromArray(spawnableEnemies);
                var spawned = Instantiate(enemy, getSpawnPosition(), enemy.transform.rotation);
                SpawnedEnemiesCount++;
                spawned.GetComponent<DestroyOnHit>().RegisterOnDeath(DecreaseSpawnedCount);
            }
        }
    }

    Vector3 getSpawnPosition()
    {
        return new Vector3(
            transform.position.x + Random.Range(SpawnerLimits.min.x, SpawnerLimits.max.x),
            transform.position.y + Random.Range(SpawnerLimits.min.y, SpawnerLimits.max.y)
        );
    }
    void DecreaseSpawnedCount()
    {
        SpawnedEnemiesCount--;
        if (SpawnedEnemiesCount == 0)
        {
            Spawn();
        }
    }

    GameObject PickRandomFromArray(SpawnableEnemy[] enemies)
    {
        return enemies[Random.Range(0, enemies.Length - 1)].prefab;
    }
}
 
[Serializable]
public class SpawnConfig
{
    public int tier;
    public int amountToSpawn;
}
[Serializable]
public class SpawnableEnemy
{
    public GameObject prefab;
    public int tier;
}