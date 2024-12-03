using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncePad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
        if (rb && collider.gameObject.name != "arm")
        {
            Debug.Log("BRAZ " + collider.gameObject.name);
            rb.velocity = new Vector3(rb.velocity.x * 2, -rb.velocity.y * 2, rb.velocity.z);
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
