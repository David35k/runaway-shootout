using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class arm : MonoBehaviour
{

    public GameObject[] targets;
    private GameObject currentTarget;
    private bool tracking = false;
    private playa playerScript;
    public float swingForce = 50000f;

    void Start()
    {
        playerScript = transform.parent.gameObject.GetComponent<playa>();
    }

    void OnTriggerStay(Collider collider)
    {
        if (targets.Contains(collider.gameObject))
        {
            currentTarget = collider.gameObject;
            // tracking = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == currentTarget)
        {
            currentTarget = null;
            tracking = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // make sure bro actually has a gun
        if (playerScript.schlong != null)
        {
            if (tracking && currentTarget != null && playerScript.schlong.GetComponent<gun>().equipped)
            {
                transform.LookAt(currentTarget.transform.position + new Vector3(0f, 0.8f));
            }
        }

    }

    public void swing()
    {
        tracking = false;
        // transform.rotation = Quaternion.Lerp(Quaternion.Euler(), Quaternion.Euler(Vector3.forward), 1f);
        // transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        GetComponent<Rigidbody>().AddTorque(0f, 0f, -90f, ForceMode.Impulse);
    }
}
