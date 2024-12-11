using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float bulletLifetime = 5.0f;
    public GameObject playaFired;
    public float damage = 7.5f;
    private bool uselessAhh = false;
    public bool missile = false;
    private bool tracking = false;
    private GameObject currentTarget;
    // only applies if missile is set to true
    public float flyForce = 100f;
    private float explosionRadius = 20f;
    private float sussyRadius = 3.75f;
    private float explosionForce = 2000f;
    public bool stun = false;
    public float stunLength = 3f;
    public GameObject particleThing;
    // if you get hit it turns you into paul lmao
    public bool paul;
    public GameObject hitMarker;
    public bool swappy = false;

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
            Instantiate(player.bloodEffect, transform.position, transform.rotation, collision.gameObject.transform);
            GameObject hitshit = Instantiate(hitMarker, transform.position, transform.rotation, collision.gameObject.transform);
            Destroy(hitshit, 0.1f);

            if (swappy)
            {
                if (GetComponent<AudioSource>())
                {
                    // Create a temporary GameObject for the sound
                    GameObject soundGameObject = new GameObject("TempAudio");
                    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

                    // Set the audio clip and play it
                    audioSource.clip = GetComponent<AudioSource>().clip;
                    audioSource.Play();

                    // Destroy the sound GameObject after the clip finishes
                    Destroy(soundGameObject, GetComponent<AudioSource>().clip.length);
                }

                Vector3 enemyPos = player.transform.position;
                player.transform.position = playaFired.transform.position;
                playaFired.transform.position = enemyPos;
                return;
            }
            if (missile)
            {
                kaboom();
            }
            if (stun)
            {
                player.stun(stunLength);
                Instantiate(player.freezeEffect, transform.position, transform.rotation, collision.gameObject.transform);
            }
            if (paul)
            {
                player.paul(5f); // become paul for 5 seconds, try not to kys :)
                // Instantiate(player.paulEffect, transform.position, transform.rotation, collision.gameObject.transform);
            }
            player.ouch(damage);
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
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, sussyRadius);
    }

    public void kaboom()
    {
        if (particleThing)
        {
            Instantiate(particleThing, transform.position, transform.rotation);
        }

        if (GetComponent<AudioSource>())
        {
            // Create a temporary GameObject for the sound
            GameObject soundGameObject = new GameObject("TempAudio");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            // Set the audio clip and play it
            audioSource.clip = GetComponent<AudioSource>().clip;
            audioSource.Play();

            // Destroy the sound GameObject after the clip finishes
            Destroy(soundGameObject, GetComponent<AudioSource>().clip.length);
        }

        // Find all objects within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // Calculate the distance from the explosion center
            float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

            // Check if the object is within the explosion radius
            if (distance <= sussyRadius)
            {
                // Apply force to any objects with a Rigidbody
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Apply explosion force
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }

                if (nearbyObject.gameObject.tag == "Player")
                {
                    nearbyObject.gameObject.GetComponent<playa>().ouch(damage);
                    nearbyObject.gameObject.GetComponent<playa>().gameManager.GetComponent<gameManager>().shakeEm(0.5f, 0.5f, nearbyObject.gameObject.GetComponent<playa>().playaNumber);
                }
            }
            else
            {
                if (nearbyObject.gameObject.tag == "Player")
                {
                    nearbyObject.gameObject.GetComponent<playa>().gameManager.GetComponent<gameManager>().shakeEm(0.5f, 1f / distance, nearbyObject.gameObject.GetComponent<playa>().playaNumber);
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
