using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pickup : MonoBehaviour
{

    public GameObject[] guns;
    public GameObject gunChoice;
    private GameObject gun;
    public GameObject itemPoint;
    private float startingY;
    // how often a new gun spawns in seconds
    public float gunTime = 7.5f;
    public float nextTime;

    void Awake()
    {
        newItem();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "playa hitbox" && gun != null && collider.gameObject.transform.parent.gameObject.GetComponent<playa>().schlong == null)
        {
            collider.gameObject.transform.parent.gameObject.GetComponent<playa>().getGun(gunChoice);
            Destroy(gun);
            gun = null;
            nextTime = Time.time + gunTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gun != null)
        {
            gun.transform.position = new Vector3(gun.transform.position.x, startingY + 0.5f * (float)Math.Sin(Time.fixedTime));
            gun.transform.Rotate(0, 45f * Time.deltaTime, 0);
        }
        else if (Time.time >= nextTime)
        {
            newItem();
        }

    }

    void newItem()
    {
        // choose random gun
        gunChoice = guns[UnityEngine.Random.Range(0, guns.Length)];

        gun = Instantiate(gunChoice, itemPoint.transform.position, itemPoint.transform.rotation);
        startingY = gun.transform.position.y;
    }
}