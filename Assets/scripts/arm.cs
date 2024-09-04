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

    void Start()
    {
        playerScript = transform.parent.gameObject.GetComponent<playa>();
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

        if (tracking && currentTarget != null && playerScript.schlong != null)
        {
            gameObject.transform.LookAt(currentTarget.transform);
        }
    }
}
