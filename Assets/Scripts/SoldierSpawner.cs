using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public GameObject soldier;
    public float wait_time_between_spawning;
    private float next_spawn_time;
    public GameObject spawnObject;
    public SpawnArea spawn_area;

    [System.Serializable]
    public class SpawnArea
    {
        public float offset_x;
        public float y;
        public float offset_z;
    };

    // Update is called once per physics frame
    void FixedUpdate()
    {
        if (next_spawn_time < Time.time)
        {
            SpawnSoldier();
            next_spawn_time = Time.time + wait_time_between_spawning;
        }
    }


    private void SpawnSoldier()
    {
        Vector3 spawn_location = get_location();
        Instantiate(soldier, spawn_location, Quaternion.identity);
    }

    private Vector3 get_location()
    {
        Vector3 spawnLocation = spawnObject.transform.position;
        float xPos = spawnLocation.x + Random.Range(-1 * spawn_area.offset_x, spawn_area.offset_x);
        float zPos = spawnLocation.z + Random.Range(-1 * spawn_area.offset_z, spawn_area.offset_z);

        return new Vector3(xPos, spawn_area.y, zPos);
    }
}
