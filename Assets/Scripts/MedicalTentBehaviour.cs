using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalTentBehaviour : MonoBehaviour
{
    private int SOLDIER_SPAWN_TIME = 30;
    public GameObject spawnObject;
    public SpawnArea spawnArea;
    public GameObject soldier;
    public GameObject enemyBase;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnFromReserve()
    {
        yield return new WaitForSeconds(SOLDIER_SPAWN_TIME);
        Vector3 spawnLocation = getLocation();
        GameObject newSoldier = Instantiate(soldier, spawnLocation, Quaternion.identity);
        newSoldier.GetComponent<SoldierDecision>().enemyBase = enemyBase;

    }

    private Vector3 getLocation() {
        Vector3 spawnLocation = spawnObject.transform.position;
        float xPos = spawnLocation.x + Random.Range(-1 * spawnArea.offset_x, spawnArea.offset_x);
        float zPos = spawnLocation.z + Random.Range(-1 * spawnArea.offset_z, spawnArea.offset_z);

        return new Vector3(xPos, spawnArea.y, zPos);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "woundedSoldier")
        {
            Destroy(collider.gameObject);
            StartCoroutine(SpawnFromReserve());
        }
    }

    [System.Serializable]
    public class SpawnArea
    {
        public float offset_x;
        public float y;
        public float offset_z;
    };
}
