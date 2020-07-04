using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDecision : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    private GameObject currentEnemy;

    public float detectionRadius = 5;

    private Rigidbody myRigidbody; //rigidBody gives the player physics 

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); //add the rigidbody component to this script for use later. 
    }

    public void EnemyDetected(GameObject newEnemy)
    {
        Debug.Log("Green tag detected and ready to decide");
        currentEnemy = newEnemy;
    }

    void FixedUpdate()
    {
        if (currentEnemy != null && Time.time > nextFire)
        {
            
            Vector3 toEnemy = currentEnemy.transform.position - transform.position;

            Debug.Log(toEnemy.sqrMagnitude);
            if (toEnemy.sqrMagnitude <= detectionRadius * detectionRadius)
            {
                nextFire = Time.time + fireRate;
                Quaternion quaternionToPlayer = Quaternion.Euler(toEnemy);

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(toEnemy, Vector3.up);

                Instantiate(shot, shotSpawn.position, rotation);

                if (toEnemy != Vector3.zero)
                {
                    MoveCharacter(-toEnemy);
                }
            }
        }
    }

    void MoveCharacter(Vector3 vectorToMoveTo)
    {
        //transform.position is the player position
        // add the "change" (x and y modifications) 
        //Time.Delta is the amount of time that has passed since the previous frame 
        //So what we are saying here is: Move my character TO my current poistion + the changes I asked to make (direction) * my current speed * the amount of time that has passed.
        //this last piece about the time change is to make it look more smooth when your character moves. 
        myRigidbody.MovePosition(transform.position - vectorToMoveTo * 10 * Time.deltaTime);
    }


}
