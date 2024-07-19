using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playa : MonoBehaviour
{

    private GameObject player;
    private Rigidbody rig;
    private bool grounded = true;

    // for rotation
    private bool rotating = false;
    private Quaternion targetRot;
    private Quaternion startRot;
    private float rotSpeed = 2f;
    private float timeCount = 0.0f;

    void Awake()
    {
        player = this.gameObject;
        rig = this.gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("script is attached to: " + player.name);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float zrot = transform.eulerAngles.z;

        if (Input.GetKey(KeyCode.D) && !rotating)
        {
            rotating = true;
            targetRot = Quaternion.Euler(0, 0, 290);
            startRot = transform.rotation;
        }

        if (Input.GetKeyUp(KeyCode.D) && rotating)
        {
            rotating = false;
            rig.AddForce(250 * transform.up); // figured this out myself!!
        }

        // handle rotation
        if (rotating)
        {
            transform.rotation = Quaternion.Lerp(startRot, targetRot, timeCount * rotSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
    }
}