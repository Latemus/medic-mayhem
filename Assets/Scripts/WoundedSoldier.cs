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
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "ambulance")
        {
            this.transform.position = collider.transform.parent.gameObject.transform.GetChild(0).transform.position + new Vector3 (0, 0.25f, 0);
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "truckBed")
        {
            StartCoroutine(ActivateColliderWithAmbulance());
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public IEnumerator ActivateColliderWithAmbulance()
    {
        yield return new WaitForSeconds(2);
        this.GetComponent<BoxCollider>().enabled = true;
    } 

    public void FixedUpdate() 
    {
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(0, 5, 0);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
