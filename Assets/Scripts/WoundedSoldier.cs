using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundedSoldier : MonoBehaviour
{
    GameObject ambulance;

    // Start is called before the first frame update
    void Start()
    {
        ambulance = GameObject.FindWithTag("ambulanceParent");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ambulance" && ambulance.GetComponent<AmbulanceStatus>().HasSpace())
        {
            this.transform.position = ambulance.transform.GetChild(0).transform.position + new Vector3 (0, 0.25f, 0);
            this.GetComponent<BoxCollider>().enabled = false;

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "truckBed")
        {
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
