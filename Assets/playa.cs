using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playa : MonoBehaviour
{

    private GameObject player;
    public Rigidbody rig;
    private bool grounded = true;
    private bool inputting = false;

    // for rotation
    private Quaternion targetRot;
    private Quaternion startRot;
    public float rotSpeed = 4f;
    private float timeCount = 0.0f;
    private bool rotating = false;

    void Awake()
    {
        player = this.gameObject;
        rig = player.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKey(KeyCode.D) && !inputting)
        {
            rotating = true;
            inputting = true;
            timeCount = 0.0f;
            targetRot = Quaternion.Euler(0, 0, 290);
            startRot = transform.rotation;
        }

        if (Input.GetKey(KeyCode.A) && !inputting)
        {
            rotating = true;
            inputting = true;
            timeCount = 0.0f;
            targetRot = Quaternion.Euler(0, 0, 70);
            startRot = transform.rotation;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rotating = false;
            inputting = false;
            if (grounded)
            {
                rig.AddForce(transform.up * 350); // figured this out myself!!
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rotating = false;
            inputting = false;
            if (grounded)
            {
                rig.AddForce(transform.up * 350); // figured this out myself!!
            }
        }

        if (!inputting && !rotating)
        {
            rotating = true;
            timeCount = 0.0f;
            targetRot = Quaternion.Euler(0, 0, 0);
            startRot = transform.rotation;
        }

        if (rotating)
        {
            // handle rotation
            transform.rotation = Quaternion.Lerp(startRot, targetRot, timeCount * rotSpeed);
            timeCount = timeCount + Time.deltaTime;
        }
    }
}