using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    public GameObject gunChoice;
    private GameObject gun;
    public GameObject itemPoint;
    public GameObject gunSlot;
    public bool gunPicked = false;
    private float startingY;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OHIOOIHOHIOHII " + collider.gameObject.tag);
        if (collider.gameObject.tag == "Player" && !gunPicked)
        {
            collider.gameObject.GetComponent<playa>().getGun(gunChoice);
            gunPicked = true;
        }
    }

    void Awake()
    {
        gun = Instantiate(gunChoice, itemPoint.transform.position, itemPoint.transform.rotation);
        startingY = gun.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunPicked)
        {
            if (gun)
            {
                Destroy(gun);
            }
        }
        else
        {
            gun.transform.position = new Vector3(gun.transform.position.x, startingY + 0.5f * (float)Math.Sin(Time.fixedTime));
            gun.transform.Rotate(0, 45f * Time.deltaTime, 0);
            Debug.Log(gun.transform.rotation.y);
        }
    }
}
