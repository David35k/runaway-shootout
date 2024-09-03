using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public float bulletSpeed = 10f;
    public bool equipped = false;
    public int playaNumber;
    public float xOffset = 0f;
    public float yOffset = 0f;
    // time betweem shots in seconds
    public float fireRate = 0.1f;
    private float nextFire = 0.0f;
    public bool automatic = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (automatic)
        {
            if (Input.GetKey(KeyCode.E) && equipped && playaNumber == 1 && Time.time > nextFire)
            {
                shoot();
            }
            if (Input.GetKey(KeyCode.P) && equipped && playaNumber == 2 && Time.time > nextFire)
            {
                shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) && equipped && playaNumber == 1 && Time.time > nextFire)
            {
                shoot();
            }
            if (Input.GetKeyDown(KeyCode.P) && equipped && playaNumber == 2 && Time.time > nextFire)
            {
                shoot();
            }
        }

    }

    void shoot()
    {
        if (bulletPrefab && bulletSpawn)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            bullet.GetComponent<bullet>().playaFired = playaNumber;
            if (rb)
            {
                rb.velocity = bulletSpawn.transform.up * bulletSpeed;
            }
            nextFire = Time.time + fireRate;
        }
    }
}
