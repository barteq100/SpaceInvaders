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
            CannonsController.Shoot(Cannons, ShootPower);
            yield return new WaitForSeconds(Random.Range(MinTimeToShoot, MaxTimeToShoot));
        }
    }
}
