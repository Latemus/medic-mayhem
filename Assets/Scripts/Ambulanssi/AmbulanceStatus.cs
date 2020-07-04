using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceStatus : MonoBehaviour
{

    public int soldierCount = 0;
    public int maxSoldiers = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HasSpace()
    {
        return soldierCount < maxSoldiers;
    }

    public void AddSoldier()
    {
        if (soldierCount < maxSoldiers)
        {
            soldierCount += 1;
        }
    }
}
