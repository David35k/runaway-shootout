using System.Collections;
using UnityEngine;

public class playa : MonoBehaviour
{
    private Rigidbody rb;
    private bool grounded = false;
    public float jumpForce = 300f;
    public float rotationTorque = 0.01f;
    public float lowAngularDrag = 0.4f;
    public float highAngularDrag = 10f;
    public GameObject gunSpawn;
    public GameObject playaArm;
    public int playaNumber;
    public GameObject schlong = null;
    public float groundDetectDist = 0.5f;
    public float health = 100f;
    public bool ded = false;
    public GameObject spawnPoint;
    private RigidbodyConstraints defaultConstraints;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        defaultConstraints = rb.constraints;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundDetectDist);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ded)
        {
            handleGround();
            if (grounded)
            {
                handleJump();
            }

            if (schlong && !schlong.GetComponent<gun>().equipped)
            {
                schlong = null;
            }
        }
    }

    // frame independent type shi
    void FixedUpdate()
    {
        if (!ded)
        {
            // check if ded
            if (health <= 0)
            {
                died();
            }

            float zrot = transform.rotation.eulerAngles.z;
            handleInput(zrot);
        }
    }

    void died()
    {
        ded = true;
        // throw that bish away, ded people dont need guns
        if (schlong != null)
        {
            schlong.GetComponent<gun>().yeet();
            schlong = null;
        }
        rb.constraints = RigidbodyConstraints.None;
        rb.centerOfMass = new Vector3(0f, 0f);
        rb.AddTorque(Random.insideUnitSphere * jumpForce, ForceMode.Impulse);
        StartCoroutine(delayedDed());
    }

    IEnumerator delayedDed()
    {
        yield return new WaitForSeconds(2f);
        rb.velocity = Vector3.zero; // Reset velocity
        rb.angularVelocity = Vector3.zero; // Reset angular velocity
        playaArm.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.right));
        playaArm.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playaArm.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        rb.centerOfMass = new Vector3(0f, -0.27f);
        rb.constraints = defaultConstraints;
        rb.MovePosition(spawnPoint.transform.position);
        rb.MoveRotation(spawnPoint.transform.rotation);
        health = 100;
        ded = false;
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

    void handleJump()
    {
        // PLAYER 1
        if (Input.GetKeyUp(KeyCode.W) && playaNumber == 1)
        {
            rb.AddForce(transform.up * jumpForce);
            grounded = false;
        }
        if (Input.GetKeyUp(KeyCode.Q) && playaNumber == 1)
        {
            rb.AddForce(transform.up * jumpForce);
            grounded = false;
        }

        // PLAYER 2
        if (Input.GetKeyUp(KeyCode.O) && playaNumber == 2)
        {

            rb.AddForce(transform.up * jumpForce);
            grounded = false;
        }

        if (Input.GetKeyUp(KeyCode.I) && playaNumber == 2)
        {
            rb.AddForce(transform.up * jumpForce);
            grounded = false;
        }

    }

    void handleInput(float zrot)
    {
        // PLAYER 1
        if (Input.GetKey(KeyCode.W) && playaNumber == 1)
        {
            if (grounded && (zrot > 300 || zrot < 60))
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }else if (zrot < 300 || zrot > 60)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }

        if (Input.GetKey(KeyCode.Q) && playaNumber == 1)
        {
            if (grounded && (zrot > 300 || zrot < 60))
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }else if (zrot < 300 || zrot > 60)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }

        // PLAYER 2
        if (Input.GetKey(KeyCode.O) && playaNumber == 2)
        {
            if (grounded && (zrot > 300 || zrot < 60))
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }else if (zrot < 300 || zrot > 60)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }

        if (Input.GetKey(KeyCode.I) && playaNumber == 2)
        {
            if (grounded && (zrot > 300 || zrot < 60))
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }else if (zrot < 300 || zrot > 60)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }
    }
}