using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playa : MonoBehaviour
{

    private GameObject player;
    private Rigidbody rb;
    private bool grounded = true;
    public float jumpForce = 300f;
    public float rotationTorque = 0.01f;
    public float lowAngularDrag = 0.4f;
    public float highAngularDrag = 10f;

    void Awake()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnCollisionStay(Collision collision)
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

        float zrot = transform.rotation.eulerAngles.z;

        if (grounded)
        {
            rb.angularDrag = lowAngularDrag;
        }
        else
        {
            rb.angularDrag = highAngularDrag;
        }


        if (Input.GetKey(KeyCode.D) && (zrot > 290 || zrot < 70))
        {
            rb.AddTorque(Vector3.forward * rotationTorque * -1);
        }

        if (Input.GetKey(KeyCode.A) && (zrot > 290 || zrot < 70))
        {
            rb.AddTorque(Vector3.forward * rotationTorque);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce); // figured this out myself!!
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce); // figured this out myself!!
            }
        }

    }
}