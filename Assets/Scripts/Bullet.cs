
using System;
using System.Collections;
using UnityEngine;

    public class Bullet: MonoBehaviour
    {
        public ParticleSystem Particle;
        public float MaxDist = 50;
        public LayerMask ColisionMask;
        
        public float CurrentDist = 0;
        
        private Vector3 startPos;
        private Renderer _renderer;
        private bool ShouldDestroy = false;
        private bool toDestroy = false;
        
        public void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ColisionMask == (ColisionMask | (1 << other.gameObject.layer)))
            {
                DestroyWithDelay();
            }
        }
        
        public void Update()
        {
            if (ShouldDestroy)
            {
                DestroyWithDelay();
            }
            StartCoroutine(CheckFlight());
        }
        

        private float GetFlownDistance()
        {
            CurrentDist = Mathf.Abs(transform.position.y - startPos.y);
            return CurrentDist;
        }

        private bool ShouldBeDestroyed()
        {
            return GetFlownDistance() > MaxDist;
        }
        

        IEnumerator CheckFlight()
        {
            ShouldDestroy = ShouldBeDestroyed();
            while (!ShouldDestroy)
            {
                yield return new WaitForSeconds(1);
            }
        }

        private ParticleSystem SpawnParticle()
        {
            var spawnedParticle = Instantiate(Particle, transform);
            spawnedParticle.transform.localScale = Vector3.one;
            return spawnedParticle;
        }

        private void DestroyWithDelay()
        {
            if (toDestroy)
            {
                return;
            }

            toDestroy = true;
            StopCoroutine(CheckFlight());
            _renderer.enabled = false;
           var particle = SpawnParticle();
           Destroy(this.gameObject, particle.main.duration + 0.5f);
        }
    }