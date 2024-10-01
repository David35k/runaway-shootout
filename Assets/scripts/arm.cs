using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class arm : MonoBehaviour
{

    public GameObject[] targets;
    public GameObject currentTarget;
    private bool tracking = false;
    private playa playerScript;
    public float swingForce = 100f;
    private Quaternion startRot;
    void Start()
    {
        playerScript = transform.parent.gameObject.GetComponent<playa>();
        startRot = transform.rotation;
    }

    void OnTriggerStay(Collider collider)
    {
        if (targets.Contains(collider.gameObject))
        {
            currentTarget = collider.gameObject;
            if (playerScript.schlong != null && !playerScript.schlong.GetComponent<gun>().meele)
            {
                tracking = true;
            }
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
        GetComponent<Rigidbody>().AddTorque(0f, 0f, -1 * swingForce, ForceMode.Impulse);
    }

    public void resetRot()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().MoveRotation(startRot);
        // transform.rotation = startRot;
    }
}
