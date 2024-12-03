using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !collider.gameObject.GetComponent<playa>().ded)
        {
            collider.gameObject.GetComponent<playa>().spawnPoint = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
