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
    public GameObject gunSpawn;

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
            rb.angularDrag = lowAngularDrag;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
            rb.angularDrag = highAngularDrag;
        }
    }

    public void getGun(GameObject gun)
    {
        Debug.Log("OHIO");
        GameObject ohio = Instantiate(gun, gunSpawn.transform.position, gunSpawn.transform.rotation, gunSpawn.transform);
        ohio.GetComponent<gun>().equipped = true;
    }

    // Update is called once per frame
    void Update()
    {
        float zrot = transform.rotation.eulerAngles.z;
        handleInput(zrot);



    }

    void handleInput(float zrot)
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (grounded && (zrot > 290 || zrot < 70))
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (grounded && (zrot > 290 || zrot < 70))
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce); // figured this out myself!!
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce); // figured this out myself!!
            }
        }

    }
}