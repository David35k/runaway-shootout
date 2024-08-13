using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float bulletLifetime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        Destroy(this.gameObject, bulletLifetime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
