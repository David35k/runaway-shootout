using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float bulletLifetime = 5.0f;
    public int playaFired;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "playa hitbox")
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
