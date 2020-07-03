using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawner : MonoBehaviour
{
    public GameObject child;
    public float wait_time_between_spawning;
    private float next_spawn_time;
    public SpawnArea spawn_area;

     [System.Serializable]
     public class SpawnArea
     {
         public float max_x;
         public float min_x;
         public float y;
         public float max_z;
         public float min_z;
     };

    // Update is called once per physics frame
    void FixedUpdate() {
        if (next_spawn_time < Time.time) {
            SpawnChild();
            next_spawn_time = Time.time + wait_time_between_spawning;
        }
    }


    private void SpawnChild() {
        Vector3 spawn_location = get_random_location();
        Instantiate(child, spawn_location, Quaternion.identity);
    }

    private Vector3 get_random_location() {
        return new Vector3( Random.Range(spawn_area.min_x, spawn_area.max_x), 
                            spawn_area.y, 
                            Random.Range(spawn_area.min_z, spawn_area.max_z)); 
    }

    private void Start()
    {
        ScoreCounter.scoreValue = 0;
    }
}
