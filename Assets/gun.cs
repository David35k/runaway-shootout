using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public float bulletSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shoot();
        }
    }

    void shoot()
    {
        if (bulletPrefab && bulletSpawn)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity = bulletSpawn.transform.up * bulletSpeed; // Adjust direction based on your setup
            }
        }
    }
}
