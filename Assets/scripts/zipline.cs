using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class zipline : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    private bool isMoving = false;
    GameObject player;
    RigidbodyConstraints bruh;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "zipline collider" && collider.transform.parent.GetComponent<Rigidbody>().velocity.y < 0f)
        {
            player = collider.transform.parent.gameObject;
            Debug.Log("this feller is ziplining: " + player.GetComponent<playa>().playaNumber);
            player.GetComponent<Rigidbody>().useGravity = false;
            // bruh = player.GetComponent<Rigidbody>().constraints;
            player.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezeRotationZ;

            if (transform.position != right.transform.position - new Vector3(0f, 1f))
            {
                isMoving = true;
            }


            // player.transform.DOMove(right.transform.position - new Vector3(0f, 1f), 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // Move the object toward the target position at a constant speed
            player.transform.position = Vector3.MoveTowards(player.transform.position, right.transform.position - new Vector3(0f, 1f), 10f * Time.deltaTime);

            if (Vector3.Distance(player.transform.position, right.transform.position - new Vector3(0f, 1f)) < 0.01f)
            {
                isMoving = false;
                player.GetComponent<Rigidbody>().useGravity = true;
                player.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}
