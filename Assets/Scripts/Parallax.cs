using System;
using UnityEngine;

namespace DefaultNamespace
{
    
    
    public class Parallax : MonoBehaviour
    {
        
        public GameObject Camera;
        public float parallaxEffect;
        public float Speed = 10f;
        private float length, startpos;

        private void Awake()
        {
            startpos = transform.position.y;
            length = GetComponent<SpriteRenderer>().bounds.size.y;
        }

        private void FixedUpdate()
        {
            
           transform.position = new Vector3(transform.position.x , transform.position.y - (parallaxEffect * Time.deltaTime * Speed ), transform.position.z);
           var dist = startpos - transform.position.y;
           if(dist > length) transform.position = new Vector3(transform.position.x , startpos, transform.position.z);
        }
    }
}