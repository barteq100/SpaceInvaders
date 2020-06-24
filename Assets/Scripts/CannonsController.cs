using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CannonsController
{
    public GameObject BulletPrefab;

    public void Shoot(IEnumerable<Transform> Cannons, int ShootPower)
    {
        foreach (var cannon in Cannons)
        {
            SpawnBullet(cannon, ShootPower);
        }
    }

    void SpawnBullet(Transform cannon, int shootPower)
    {
        var bullet = GameObject.Instantiate(BulletPrefab, cannon.transform.position, BulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.up * shootPower;
    }
}