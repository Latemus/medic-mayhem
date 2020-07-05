using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierDecision : MonoBehaviour
{
    public GameObject shot;
    public GameObject shot_start_transform;
    public float fireRate;
    public float piuRate = 7;

    // navigation
    private UnityEngine.AI.NavMeshAgent agent;

    private float nextFire;
    private float nextPiupiu;

    private GameObject currentEnemy;
    public GameObject enemyBase;

    public float detectionRadius = 5;

    private Rigidbody myRigidbody; //rigidBody gives the player physics 
    public AudioClip[] piupiu;
    private AudioSource piuSource;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); //add the rigidbody component to this script for use later. 
        piuSource = GetComponent<AudioSource>();
        StartCoroutine(BounceBounce());
        NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public IEnumerator BounceBounce()
    {
        // Wait that the soldier drops to the ground
        yield return new WaitForSeconds(2);
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(0.7f,0.8f));
            myRigidbody.velocity = new Vector3(0, 3, 0);
        }
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

                Vector3 relativePos = currentEnemy.transform.position - transform.position;
                // the second argument, upwards, defaults to Vector3.up
                Quaternion my_rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = my_rotation;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(toEnemy, Vector3.up);

                Instantiate(shot, shot_start_transform.transform.position, rotation);

                GetComponent<NavMeshAgent>().destination = transform.position;

                if (Time.time > nextPiupiu) 
                {
                    nextPiupiu = Time.time + piuRate;
                    piuSource.clip = piupiu[Random.Range(0, piupiu.Length)];
                    piuSource.Play();
                }
            }
        }
        else 
        {
            if (Check_if_enemies_are_nearby())
            {
                return;
            }
            // Move towards enemy base
            MoveCharacter(enemyBase.transform.position);
        }
    }

    void MoveCharacter(Vector3 vectorToMoveTo)
    {
        //Debug.Log("character moving");

        GetComponent<NavMeshAgent>().destination = vectorToMoveTo;

        /*float step =  1 * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, enemyBase.transform.position, step);

        Vector3 relativePos = enemyBase.transform.position - transform.position;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion my_rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = my_rotation;*/
    }

    bool Check_if_enemies_are_nearby()
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
                return true;
            }
            i++;
        }
        return false;
    }
}
