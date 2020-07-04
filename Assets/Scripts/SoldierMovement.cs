using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public GameObject targetObject;
    public float baseSpeed = 10f;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = this.targetObject.transform.position;
        Vector3 soldier = gameObject.transform.position;
        Vector3 direction = (targetPosition - soldier);
        float step = baseSpeed * Time.deltaTime;

        transform.position += direction * step;

        /*
        transform.position = Vector3.MoveTowards(
            targetObject.transform.position, gameObject.transform.position, step);
            */
    }
}
