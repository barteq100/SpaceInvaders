using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DestroyOnHit : MonoBehaviour
{
    public LayerMask ColisionMask;
    public int HitPoints = 2;

    public ParticleSystem ParticlesToSpawnOnDeath;

    private UnityAction<GameObject> OnDeath;

    public void RegisterOnDeath(UnityAction<GameObject> action)
    {
        OnDeath += action;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ColisionMask == (ColisionMask | (1 << other.gameObject.layer)))
        {
            HitPoints--;
        }

        if (HitPoints <= 0)
        {
            OnDeath?.Invoke(gameObject);
            Destroy(gameObject);
            SpawnParticlesOnDeath();
        }
    }
    

    private void SpawnParticlesOnDeath()
    {
        var particle =  Instantiate(ParticlesToSpawnOnDeath, transform.position, Quaternion.identity);
        Destroy(particle.gameObject, particle.main.duration);
    }
}
