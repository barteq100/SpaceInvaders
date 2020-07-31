using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    public float MovementSpeed;

    public int Power = 1;
    public int ShootPower;

    public AudioClip audioClip;
    public MapLimits MapLimits;
    public CannonsController CannonsController;
    public Transform MiddleCannon;
    public Transform LeftCannon;
    public Transform RightCannon;

    public ParticleSystem OnPowerUpParticle;
    public ParticleSystem OnPowerDownParticle;
    
    private AudioSource audioSource;
    private int maxPower = 3;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, MapLimits.min.x, MapLimits.max.x),
            Mathf.Clamp(transform.position.y, MapLimits.min.y, MapLimits.max.y)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            var power = other.GetComponent<PowerBase>().GetPower();
            Power += power;
            Power = Mathf.Clamp(Power, 1, maxPower);
            var powerParticle = Instantiate(GetParticlesByPower(power), transform);
            Destroy(powerParticle.gameObject, powerParticle.main.duration);
            Destroy(other.gameObject);
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(GetPlayerTranslate(Vector3.left));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(GetPlayerTranslate(Vector3.right));
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(GetPlayerTranslate(Vector3.up));
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(GetPlayerTranslate(Vector3.down));
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(audioClip);
            CannonsController.Shoot(GetCannons(), ShootPower);
        }
    }

    IEnumerable<Transform> GetCannons()
    {
        switch (Power)
        {
            case 1:
                return new[] {MiddleCannon};
            case 2:
                return new[] {LeftCannon, RightCannon};
            case 3:
                return new[] {MiddleCannon, LeftCannon, RightCannon};
            default:
                return new List<Transform>();
        }
    }

    Vector3 GetPlayerTranslate(Vector3 direction)
    {
        return direction * MovementSpeed * Time.deltaTime;
    }

    ParticleSystem GetParticlesByPower(int power)
    {
        return power > 0 ? OnPowerUpParticle : OnPowerDownParticle;
    }
}