using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    private Rigidbody myRigidbody; //rigidBody gives the player physics 
    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody>(); //add the rigidbody component to this script for use later. 
        myRigidbody.velocity = transform.forward * speed;
        Object.Destroy(gameObject, 3.0f);   
    }

    private void OnTriggerEnter(Collider col) 
    {   
        if (col.transform.gameObject.tag == "Green" || col.transform.gameObject.tag == "Tan")
        {
            col.transform.gameObject.GetComponent<SoldierHealth>().curHealth -= 1;
            if (col.transform.gameObject.GetComponent<SoldierHealth>().curHealth < 1) 
            {
                Destroy(col.transform.gameObject);
            }
            Destroy(gameObject); 
        }
    }
}
