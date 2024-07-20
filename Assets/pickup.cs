using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    public GameObject pistol;
    public GameObject itemPoint;
    public GameObject gunSlot;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        GameObject gun = Instantiate(pistol, itemPoint.transform.position, itemPoint.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
