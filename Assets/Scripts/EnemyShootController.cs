using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyShootController : MonoBehaviour
{
    public float MinTimeToShoot = 1f;

    public float MaxTimeToShoot = 3f;
    public List<Transform> Cannons;
    public int ShootPower = -10;
    public CannonsController CannonsController;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Shoot());
    }

    private void OnDestroy()
    {
        StopCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            var waitTime = Random.Range(MinTimeToShoot, MaxTimeToShoot);
            Debug.Log($"Waiting To shoot: {waitTime}");
            yield return new WaitForSeconds(waitTime);
            CannonsController.Shoot(Cannons, ShootPower);
        }
    }
}
