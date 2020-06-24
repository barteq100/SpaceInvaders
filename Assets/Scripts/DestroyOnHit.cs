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

    private UnityAction OnDeath;

    public void RegisterOnDeath(UnityAction action)
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
            Destroy(gameObject);
            OnDeath?.Invoke();
            SpawnParticlesOnDeath();
        }
    }
    

    private void SpawnParticlesOnDeath()
    {
        var particle =  Instantiate(ParticlesToSpawnOnDeath, transform.position, Quaternion.identity);
        Destroy(particle, particle.main.duration);
    }
}
