using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody myRigidbody; //rigidBody gives the player physics 
    private Vector3 change; // the value we will use to tell unity where we moved 


    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate;

    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); //add the rigidbody component to this script for use later. 
    }

    // Update is called once per frame
    void Update()
    {
       
        //Zero out the change
        change = Vector3.zero;
         //Get the current x and y as points on a graph
        change.x = Input.GetAxisRaw("Horizontal");
        change.z = Input.GetAxisRaw("Vertical");
        //print the change to the console 
        // Debug.Log(change);
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }

    }
    void MoveCharacter()
    {
        //transform.position is the player position
        // add the "change" (x and y modifications) 
        //Time.Delta is the amount of time that has passed since the previous frame 
        //So what we are saying here is: Move my character TO my current poistion + the changes I asked to make (direction) * my current speed * the amount of time that has passed.
        //this last piece about the time change is to make it look more smooth when your character moves. 
        myRigidbody.MovePosition(transform.position - change * speed * Time.deltaTime);
    }

}