using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playa : MonoBehaviour
{

    private GameObject player;
    private Rigidbody rig;

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

    // Update is called once per frame
    void Update()
    {
        float zrot = player.transform.eulerAngles.z;
        Debug.Log(zrot);

        if (Input.GetKey(KeyCode.D) && (zrot < 70 || zrot > 290))
        {
            player.transform.Rotate(new Vector3(0, 0, -0.5f));
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rig.AddForce(new Vector3(0, 500, 0));
        }
    }
}