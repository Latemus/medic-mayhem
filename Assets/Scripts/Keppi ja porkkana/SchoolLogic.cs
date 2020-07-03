using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolLogic : MonoBehaviour
{
    
    //Detect collisions between the GameObjects with Colliders attached
    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Child")
        {
            Destroy(collision.gameObject);
            AddMoney();
        }
    }
    private void AddMoney() {
        ScoreCounter.IncreaseScore(1);
    }
}
