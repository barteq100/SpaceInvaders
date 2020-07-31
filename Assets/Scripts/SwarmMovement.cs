using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class SwarmMovement : InvaderMovement
    {
        // Start is called before the first frame update
        private void Start()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyFire")) return;
            ChangeMovement();
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, MapLimits.min.x, MapLimits.max.x),
                Mathf.Clamp(transform.position.y, MapLimits.min.y, MapLimits.max.y)
            );
            if (transform.position.x >= MapLimits.max.x ||
                transform.position.x <= MapLimits.min.x)
            {
                ChangeMovement();
            }

            Movement();
        }


        private void Movement()
        {
            if (ShouldChangeMovement())
            {
                ChangeMovement();
            }

            Move();
        }

        public override void ChangeMovement()
        {
            spentTime = 0f;
            direction *= -1;
        }

        private void Move()
        {
            transform.position += new Vector3(
                direction * AngularMovmentSpeed * Time.deltaTime,
                -DownMovementSpeed * Time.deltaTime);
        }
    }
}