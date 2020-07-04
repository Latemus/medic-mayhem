using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalTentBehaviour : MonoBehaviour
{
    public int soldierReserve;
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
        if (collider.tag == "woundedSoldier")
        {
            Destroy(collider.gameObject);
            soldierReserve ++;
        }
    }
}
