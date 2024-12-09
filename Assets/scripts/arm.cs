using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class arm : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject currentTarget;
    private bool tracking = false;
    private playa playerScript;
    public float swingForce = 100f;
    private Quaternion startRot;
    private GameObject[] inRange = new GameObject[4];

    void Start()
    {
        playerScript = transform.parent.gameObject.GetComponent<playa>();
        startRot = transform.rotation;
    }

    void OnTriggerStay(Collider collider)
    {
        if (targets.Contains(collider.gameObject) && !collider.gameObject.GetComponent<playa>().ded)
        {
            inRange[collider.gameObject.GetComponent<playa>().playaNumber - 1] = collider.gameObject;
            UpdateNearestTarget();
            if (playerScript.schlong != null && !playerScript.schlong.GetComponent<gun>().meele)
            {
                tracking = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (targets.Contains(collider.gameObject))
        {
            inRange[collider.gameObject.GetComponent<playa>().playaNumber - 1] = null;
            UpdateNearestTarget();
        }
    }

    void Update()
    {
        // Ensure the player has a gun
        if (playerScript.schlong != null)
        {
            if (tracking && currentTarget != null && playerScript.schlong.GetComponent<gun>().equipped)
            {
                transform.LookAt(currentTarget.transform.position + new Vector3(0f, 1f));
            }

            if (currentTarget != null && currentTarget.GetComponent<playa>().ded)
            {
                UpdateNearestTarget();
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
    }

    // Method to update the nearest target
    private void UpdateNearestTarget()
    {
        float closestDistance = float.MaxValue;
        GameObject nearestTarget = null;

        foreach (GameObject target in inRange)
        {
            if (target != null && !target.GetComponent<playa>().ded)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestTarget = target;
                }
            }
        }

        currentTarget = nearestTarget;

        // Stop tracking if no targets are left
        if (currentTarget == null)
        {
            tracking = false;
        }
    }
}