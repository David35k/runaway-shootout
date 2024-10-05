using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float bulletLifetime = 5.0f;
    public int playaFired;
    public float damage = 7.5f;
    private bool uselessAhh = false;
    public bool missile = false;
    private bool tracking = false;
    private GameObject currentTarget;
    // only applies if missile is set to true
    public float flyForce = 100f;
    private float explosionRadius = 3.75f;
    private float explosionForce = 2000f;
    public bool stun = false;
    public float stunLength = 3f;
    public GameObject particleThing;
    // private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" && !uselessAhh)
        {
            if (missile)
            {
                kaboom();
            }
            uselessAhh = true;
            Destroy(gameObject, 1f);
        }
        if (collision.gameObject.tag == "Player" && !uselessAhh)
        {
            playa player = collision.gameObject.GetComponent<playa>();
            player.health -= damage;
            player.updateHealthBar();
            Instantiate(player.bloodEffect, transform.position, transform.rotation, collision.gameObject.transform);
            if (missile)
            {
                kaboom();
            }
            if (stun)
            {
                player.stun(stunLength);
            }
            uselessAhh = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (tracking)
        {
            transform.LookAt(currentTarget.transform);
            GetComponent<Rigidbody>().AddForce(transform.forward * flyForce, ForceMode.Force);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void kaboom()
    {
        if (particleThing)
        {
            Instantiate(particleThing, transform.position, transform.rotation);
        }

        // Find all objects within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Calculate the distance from the explosion center
            float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

            // Check if the object is within the explosion radius
            if (distance <= explosionRadius)
            {
                // Apply force to any objects with a Rigidbody
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Apply explosion force
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }

                // Check if the object has a health system and apply damage
                if (nearbyObject.gameObject.tag == "Player")
                {
                    nearbyObject.gameObject.GetComponent<playa>().health -= damage;
                    nearbyObject.gameObject.GetComponent<playa>().updateHealthBar();
                }
            }
        }

        Destroy(gameObject);
    }

    public void shoot(GameObject bulletSpawn, float bulletSpeed, GameObject target, GameObject thaplaya)
    {
        // shoot da bullet!!!
        GetComponent<Rigidbody>().velocity = bulletSpawn.transform.up * bulletSpeed;

        if (missile)
        {
            // bombs away!!!!
            tracking = true;

            // the big funny, if no one around it tracks you
            if (target)
            {
                currentTarget = target;
            }
            else
            {
                currentTarget = thaplaya;
            }
        }
    }
}
