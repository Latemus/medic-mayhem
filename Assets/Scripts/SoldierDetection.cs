using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDetection : MonoBehaviour
{
    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != transform.parent.tag && (other.tag == "Green" || other.tag == "Tan"))
        {
            transform.parent.GetComponent<SoldierDecision>().EnemyDetected(other.transform.gameObject);
        }
    }
}