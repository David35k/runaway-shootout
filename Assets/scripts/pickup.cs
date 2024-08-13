using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    public GameObject gun;
    private GameObject gunPickup;
    public GameObject itemPoint;
    public GameObject gunSlot;
    public bool noGun = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OHIOOIHOHIOHII " + collider.gameObject.tag);
        if (collider.gameObject.tag == "Player" && !noGun)
        {
            collider.gameObject.GetComponent<playa>().getGun(gun);
            noGun = true;
        }
    }

    void Awake()
    {
        gunPickup = Instantiate(gun, itemPoint.transform.position, itemPoint.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (noGun)
        {
            if (gunPickup)
            {
                Destroy(gunPickup);
            }
        }
    }
}
