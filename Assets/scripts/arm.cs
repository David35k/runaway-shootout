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


    // Start is called before the first frame update
    void Start()
    {

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
        if (tracking)
        {
            this.gameObject.transform.LookAt(currentTarget.transform);
        }
    }
}
