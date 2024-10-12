using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winTrigger : MonoBehaviour
{

    public bool won = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !collider.gameObject.GetComponent<playa>().ded)
        {
            won = true;
            Debug.Log("YO THIS DUDE WON!!!!!! number: " + collider.gameObject.GetComponent<playa>().playaNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
