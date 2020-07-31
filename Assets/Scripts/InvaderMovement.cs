using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InvaderMovement : MonoBehaviour
{

    public MapLimits MapLimits;
    
    public float DownMovementSpeed = 5;
    public float AngularMovmentSpeed = 15;

    public float MinChangeDirectionTime = 1f;

    public float MaxChangeDirectionTime = 1.5f;
    public float ChangeProbability = 0.2f;
    private Rigidbody _rigidbody;

    public int direction = 1;

    public float spentTime = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SetMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyFire")) return;
        ChangeMovement();
    }

    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(Movement());
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MapLimits.min.x, MapLimits.max.x),
            Mathf.Clamp(transform.position.y, MapLimits.min.y, MapLimits.max.y)
        );
        if (transform.position.x >= MapLimits.max.x || 
            transform.position.x <= MapLimits.min.x)
        {
            ChangeMovement();
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(Movement());
    }

    public bool ShouldChangeMovement()
    {
        spentTime += Time.deltaTime;
        if (spentTime > MinChangeDirectionTime)
        {
            if (spentTime >= MaxChangeDirectionTime)
            {
                return true;
            }
            return Random.value < ChangeProbability;
        }

        return false;
    }
    
    private IEnumerator Movement()
    {
        if (ShouldChangeMovement())
        {
            ChangeMovement();
        }
        yield return new WaitForSeconds(0.1f);
    }

    public virtual void ChangeMovement()
    {
        spentTime = 0f;
        direction *= -1;
        SetMovement();
    }

    public virtual void SetMovement()
    {
        if(!_rigidbody) return;
        
        _rigidbody.velocity = new Vector3(
            direction * AngularMovmentSpeed * Time.deltaTime,
            -DownMovementSpeed * Time.deltaTime);
    }
}
