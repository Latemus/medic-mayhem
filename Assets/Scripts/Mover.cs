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
    }

}
