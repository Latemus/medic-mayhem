using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceStatus : MonoBehaviour
{
    public int soldierCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "woundedSoldier")
        {
            soldierCount += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "woundedSoldier")
        {
            soldierCount -= 1;
        }
    }
}
