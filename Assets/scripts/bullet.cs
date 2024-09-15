using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float bulletLifetime = 5.0f;
    public int playaFired;
    public float damage = 7.5f;
    private bool uselessAhh = false;

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
        if (collision.gameObject.tag == "ground")
        {
            Destroy(gameObject, 1f);
            uselessAhh = true;
        }
        if (collision.gameObject.tag == "Player" && !uselessAhh)
        {
            collision.gameObject.GetComponent<playa>().health -= damage;
            uselessAhh = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
