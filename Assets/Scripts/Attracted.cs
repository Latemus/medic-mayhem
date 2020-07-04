 using UnityEngine;
 using System.Collections;

public class Attracted : MonoBehaviour
{
    public GameObject attractedTo;
    public float strengthOfAttraction = 0.5f;
    void Start()
    {
        // For some reason couldnt drag & drop in unity
        attractedTo = GameObject.FindGameObjectWithTag("LifeSchool");
    }
    void Update()
    {
        Vector3 direction = attractedTo.transform.position - transform.position;
        GetComponent<Rigidbody>().AddForce(strengthOfAttraction * direction);

    }
}
