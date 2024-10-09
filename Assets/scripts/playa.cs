using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

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
    bool stunned = false;
    public GameObject bloodEffect;
    public GameObject groundEffect;
    private float jumpCooldown = 0.2f;
    private float lastJumpTime = 0f;
    public GameObject healthBarFill;
    public GameObject shootBarFill;
    private float lastHealthUpdateTime = 0f; // Track the last time the health was updated
    public float healthBarDisappearTime = 3f; // Time in seconds before the health bar disappears
    private CanvasGroup healthBarCanvasGroup; // To control visibility
    private float lastShootUpdateTime = 0f; // Track the last time the health was updated
    public float shootBarDisappearTime = 0.5f; // Time in seconds before the health bar disappears
    private CanvasGroup shootBarCanvasGroup; // To control visibility
    public GameObject gameManager;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        defaultConstraints = rb.constraints;

        // Initialize the health bar canvas group and hide it initially
        healthBarCanvasGroup = healthBarFill.GetComponentInParent<CanvasGroup>();
        healthBarCanvasGroup.alpha = 0;

        shootBarCanvasGroup = shootBarFill.GetComponentInParent<CanvasGroup>();
        shootBarCanvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ded && !stunned)
        {
            handleGround();
            if (grounded)
            {
                handleJump();
            }
            if (schlong != null && !schlong.GetComponent<gun>().equipped)
            {
                schlong = null;
            }
        }
    }

    void FixedUpdate()
    {
        if (!ded)
        {
            // Check if the health bar should disappear after inactivity
            if (Time.time - lastHealthUpdateTime > healthBarDisappearTime && healthBarCanvasGroup.alpha == 1)
            {
                healthBarCanvasGroup.DOFade(0, 0.25f); // Fade out the health bar
            }
            // Check if the shoot bar should disappear after inactivity
            if (Time.time - lastShootUpdateTime > shootBarDisappearTime && shootBarCanvasGroup.alpha == 1)
            {
                shootBarCanvasGroup.DOFade(0, 0.1f); // Fade out the shoot bar
            }

            if (health <= 0)
            {
                died();
            }

            float zrot = transform.rotation.eulerAngles.z;
            if (!stunned)
            {
                handleInput(zrot);
            }
        }
    }
    public void updateHealthBar()
    {
        // Update health bar fill amount
        float fillAmount = health / 100;
        healthBarFill.GetComponent<Image>().DOFillAmount(fillAmount, 0.3f);

        // Show health bar if it needs updating
        if (healthBarCanvasGroup.alpha == 0)
        {
            healthBarCanvasGroup.DOFade(1, 0.1f); // Fade in the health bar
        }

        // Reset the last update time whenever health is updated
        lastHealthUpdateTime = Time.time;
    }

    public void updateShootBar(float fireRate, float thing)
    {
        if (thing > 0)
        {
            float fillAmount = thing / fireRate;
            shootBarFill.GetComponent<Image>().DOFillAmount(fillAmount, 0.01f);

            // Show shoot bar if it needs updating
            if (shootBarCanvasGroup.alpha == 0)
            {
                shootBarCanvasGroup.alpha = 1;
            }

            // Reset the last update time whenever shoot is updated
            lastShootUpdateTime = Time.time;
        }
        else
        {
            shootBarCanvasGroup.alpha = 0;
        }

    }
    public void stun(float length)
    {
        stunned = true;
        rb.isKinematic = true;
        StartCoroutine(unfreeze(length));
    }

    IEnumerator unfreeze(float length)
    {
        yield return new WaitForSeconds(length);
        rb.isKinematic = false;
        stunned = false;
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
        healthBarCanvasGroup.DOFade(0, 0.25f); // Fade out the health bar
        StartCoroutine(delayedDed());
    }

    IEnumerator delayedDed()
    {
        yield return new WaitForSeconds(2f);
        rb.velocity = Vector3.zero; // Reset velocity
        rb.angularVelocity = Vector3.zero; // Reset angular velocity
        rb.centerOfMass = new Vector3(0f, -0.27f);
        rb.constraints = defaultConstraints;
        rb.MovePosition(spawnPoint.transform.position);
        rb.MoveRotation(spawnPoint.transform.rotation);
        playaArm.GetComponent<arm>().resetRot();
        health = 100;
        updateHealthBar();
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
        // grounded = false;
        Collider[] cols = Physics.OverlapSphere(transform.position, groundDetectDist);
        foreach (Collider col in cols)
        {
            if (col.gameObject.tag == "ground" || col.gameObject.tag == "bullet" || (col.gameObject.name == "render for playa" && !col.gameObject.transform.IsChildOf(transform)))
            {
                if (!grounded)
                {
                    Vector3 effectPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                    Instantiate(groundEffect, effectPos, Quaternion.Euler(0, 0, 0));
                }
                grounded = true;
                rb.angularDrag = lowAngularDrag;
                return;
            }
        }

        grounded = false;
        rb.angularDrag = highAngularDrag;

    }

    void handleJump()
    {
        // Prevent double jump in the same frame
        if (Time.time - lastJumpTime < jumpCooldown) return;

        // PLAYER 1
        if (playaNumber == 1)
        {
            if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.W))
            {
                rb.AddForce(transform.up * jumpForce);
                lastJumpTime = Time.time;
                return;
            }
        }

        // PLAYER 2
        if (playaNumber == 2)
        {
            if (Input.GetKeyUp(KeyCode.O) || Input.GetKeyUp(KeyCode.I))
            {
                rb.AddForce(transform.up * jumpForce);
                lastJumpTime = Time.time;
                return;
            }
        }
    }

    void handleInput(float zrot)
    {
        // PLAYER 1
        if (Input.GetKey(KeyCode.E) && playaNumber == 1)
        {
            if (grounded && (zrot > 300 || zrot < 90))
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
            else if (zrot < 300 || zrot > 90)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }

        if (Input.GetKey(KeyCode.W) && playaNumber == 1)
        {
            if (grounded && (zrot > 270 || zrot < 60))
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
            else if (zrot < 270 || zrot > 60)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }

        // PLAYER 2
        if (Input.GetKey(KeyCode.O) && playaNumber == 2)
        {
            if (grounded && (zrot > 300 || zrot < 90))
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque * -1);
            }
            else if (zrot < 300 || zrot > 90)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }

        if (Input.GetKey(KeyCode.I) && playaNumber == 2)
        {
            if (grounded && (zrot > 270 || zrot < 60))
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
            else if (!grounded)
            {
                rb.AddTorque(Vector3.forward * rotationTorque);
            }
            else if (zrot < 270 || zrot > 60)
            {
                rb.angularVelocity = Vector3.zero; // Reset angular velocity
            }
        }
    }
}