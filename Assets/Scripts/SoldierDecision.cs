using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDecision : MonoBehaviour
{
    public GameObject shot;
    public float fireRate;

    private float nextFire;

    private GameObject currentEnemy;
    public GameObject enemyBase;

    public float detectionRadius = 5;

    private Rigidbody myRigidbody; //rigidBody gives the player physics 

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); //add the rigidbody component to this script for use later. 
    }

    public void EnemyDetected(GameObject newEnemy)
    {
        //Debug.Log("Green tag detected and ready to decide");
        currentEnemy = newEnemy;
    }

    void FixedUpdate()
    {
        Vector3 toEnemy = new Vector3(0,0,0);
        if (currentEnemy != null) 
        {
            toEnemy = currentEnemy.transform.position - transform.position;
        }
        if (currentEnemy != null && toEnemy.sqrMagnitude <= detectionRadius * detectionRadius)
        {
            if (toEnemy.sqrMagnitude <= detectionRadius * detectionRadius && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Quaternion quaternionToPlayer = Quaternion.Euler(toEnemy);

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(toEnemy, Vector3.up);

                Instantiate(shot, transform.position + Vector3.forward, rotation);
            }
        }
        else 
        {
            Check_if_enemies_are_nearby();
            // Move towards enemy base
            MoveCharacter(enemyBase.transform.position);
        }
    }

    void MoveCharacter(Vector3 vectorToMoveTo)
    {
        float step =  1 * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, enemyBase.transform.position, step);
    }

    void Check_if_enemies_are_nearby()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, new Vector3(5,5,5), Quaternion.identity, 0);
        int i = 0;
        while (i < hitColliders.Length)
        {
            //Debug.Log("Hit : " + hitColliders[i].name + i);
            if (hitColliders[i].tag != transform.tag && (hitColliders[i].tag == "Green" || hitColliders[i].tag == "Tan"))
            {
                currentEnemy = hitColliders[i].transform.gameObject;
            }
            i++;
        }
    }
}
