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


    void Start()
    {
        // ConfigurableJoint joint = gameObject.GetComponent<ConfigurableJoint>();

        // // Adjust anchor and connected anchor to define the pivot point
        // joint.anchor = Vector3.zero; // Local pivot point
        // joint.connectedAnchor = Vector3.zero; // Relative to the connected body
    }
    void OnTriggerStay(Collider collider)
    {
        if (targets.Contains(collider.gameObject))
        {
            currentTarget = collider.gameObject;
            tracking = true;
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
        if (tracking && currentTarget != null)
        {
            gameObject.transform.LookAt(currentTarget.transform);
        }
        else
        {
            // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
