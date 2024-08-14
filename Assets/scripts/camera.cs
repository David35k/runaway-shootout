using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    public GameObject[] players;
    public float cameraSpeed = 2.8f;
    public float initialZ = -17.3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float centreX = 0f;
        float centreY = 0f;
        float offsetZ;

        for (int i = 0; i < players.Length; i++)
        {
            centreX += players[i].transform.position.x;
            centreY += players[i].transform.position.y;
        }

        centreX /= players.Length;
        centreY /= players.Length;

        offsetZ = initialZ + (centreX - transform.position.x + centreY - transform.position.y) * 10;

        transform.position = transform.position + new Vector3(centreX - transform.position.x, centreY - transform.position.y + 1.25f, offsetZ - transform.position.z) * Time.deltaTime * cameraSpeed;
    }
}
