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
    public int playaNumber;
    private GameObject gun;

    void Awake()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ground" || collider.gameObject.tag == "bullet" || collider.gameObject.tag == "player")
        {
            grounded = true;
            rb.angularDrag = lowAngularDrag;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "ground" || collider.gameObject.tag == "bullet" || collider.gameObject.tag == "player")
        {
            grounded = false;
            rb.angularDrag = highAngularDrag;
        }
    }

    public void getGun(GameObject gun)
    {
        gun = Instantiate(gun, gunSpawn.transform.position, gunSpawn.transform.rotation, gunSpawn.transform);
        gun.GetComponent<gun>().equipped = true;
        gun.GetComponent<gun>().playaNumber = playaNumber;
    }

    // Update is called once per frame
    void Update()
    {
        float zrot = transform.rotation.eulerAngles.z;
        handleInput(zrot);
    }

    void handleInput(float zrot)
    {
        // PLAYER 1
        if (Input.GetKey(KeyCode.W) && playaNumber == 1)
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

        if (Input.GetKey(KeyCode.Q) && playaNumber == 1)
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

        if (Input.GetKeyUp(KeyCode.W) && playaNumber == 1)
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
        if (Input.GetKeyUp(KeyCode.Q) && playaNumber == 1)
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }

        // PLAYER 2
        if (Input.GetKey(KeyCode.O) && playaNumber == 2)
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

        if (Input.GetKey(KeyCode.I) && playaNumber == 2)
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

        if (Input.GetKeyUp(KeyCode.O) && playaNumber == 2)
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }
        if (Input.GetKeyUp(KeyCode.I) && playaNumber == 2)
        {
            if (grounded)
            {
                rb.AddForce(transform.up * jumpForce);
            }
        }

    }
}