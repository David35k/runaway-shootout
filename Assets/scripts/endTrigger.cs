using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endTrigger : MonoBehaviour
{
    public bool escaping = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !collider.gameObject.GetComponent<playa>().ded)
        {
            escaping = true;
            Debug.Log("YO THIS DUDE IS ESCAPING!!!!!! number: " + collider.gameObject.GetComponent<playa>().playaNumber);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            escaping = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
