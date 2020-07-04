using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundedSoldier : MonoBehaviour
{
    public GameObject ambulance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ambulance" && ambulance.GetComponent<AmbulanceStatus>().HasSpace())
        {
            Destroy(this.gameObject);

        }
    }
}
