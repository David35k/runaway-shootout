using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camera : MonoBehaviour
{

    public GameObject player;
    public float cameraSpeed = 2.8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = transform.position + new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y + 1.25f) * Time.deltaTime * cameraSpeed;
        }
    }
}
