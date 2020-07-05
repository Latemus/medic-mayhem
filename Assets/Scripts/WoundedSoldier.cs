using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundedSoldier : MonoBehaviour
{
    GameObject ambulance;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        ambulance = GameObject.FindWithTag("ambulanceParent");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ambulance" && collider.transform.parent.gameObject.GetComponent<AmbulanceStatus>().HasSpace())
        {
            this.transform.position = collider.transform.parent.gameObject.transform.GetChild(0).transform.position + new Vector3 (0, 0.25f, 0);
            this.GetComponent<BoxCollider>().enabled = false;

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "truckBed")
        {
            this.GetComponent<BoxCollider>().enabled = true;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
