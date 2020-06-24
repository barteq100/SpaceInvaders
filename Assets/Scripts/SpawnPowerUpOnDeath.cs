using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPowerUpOnDeath : MonoBehaviour
{
    public float PowerUpProbability = 0.2f;

    public GameObject PowerUpPrefab;
    public GameObject PowerDownPrefab;
    private void OnDestroy()
    {
        if (ShouldSpawnPowerUp())
        {
            var power = RandomPower();
            Instantiate(power, transform.position, power.transform.rotation);
        }
    }

    private bool ShouldSpawnPowerUp()
    {
        return Random.value <= PowerUpProbability;
    }

    private GameObject RandomPower()
    {
        return Random.value < 0.5 ? PowerUpPrefab : PowerDownPrefab;
    }
}
