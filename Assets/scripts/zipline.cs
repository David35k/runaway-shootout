using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class zipline : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "zipline collider" && collider.transform.parent.GetComponent<Rigidbody>().velocity.y < 0f)
        {
            GameObject player = collider.transform.parent.gameObject;
            Debug.Log("this feller is ziplining: " + player.GetComponent<playa>().playaNumber);
            player.transform.DOMove(right.transform.position - new Vector3(0f, 1.6f), 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
