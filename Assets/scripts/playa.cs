using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class playa : MonoBehaviour
{
    private Rigidbody rb;
    private bool grounded = false;
    public float jumpForce = 300f;
    public float rotationTorque = 0.01f;
    public float lowAngularDrag = 0.4f;
    public float highAngularDrag = 10f;
    public GameObject gunSpawn;
    public int playaNumber;
    public GameObject schlong = null;
    public float groundDetectDist = 0.5f;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public float health = 100f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundDetectDist);
    }

    // Update is called once per frame
    void Update()
    {
        float zrot = transform.rotation.eulerAngles.z;
        handleGround();
        handleInput(zrot);

        // check if ded
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void getGun(GameObject gun)
    {
        schlong = Instantiate(gun, gunSpawn.transform.position + new Vector3(gun.GetComponent<gun>().xOffset, gun.GetComponent<gun>().yOffset), gunSpawn.transform.rotation, gunSpawn.transform);
        schlong.GetComponent<gun>().equipped = true;
        schlong.GetComponent<gun>().playaNumber = playaNumber;
    }

    void handleGround()
    {
        grounded = false;
        Collider[] cols = Physics.OverlapSphere(transform.position, groundDetectDist);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == "ground" || col.gameObject.tag == "bullet" || (col.gameObject.name == "render for playa" && !col.gameObject.transform.IsChildOf(transform)))
            {
                grounded = true;
                rb.angularDrag = lowAngularDrag;
            }
        }

        if (!grounded)
        {
            rb.angularDrag = highAngularDrag;
        }
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