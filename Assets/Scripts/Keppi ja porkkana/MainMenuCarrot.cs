using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCarrot : MonoBehaviour
{

    public float distanceY = 0;
    public float distanceZ = 40;

    public Transform carrotBase;
    public float speed = 5f;

    public bool followMouse = true;
    public bool rotateToMouse = true;

    public float carrotRotXoffset = 0f;
    public float carrotRotYoffset = 0f;
    public float carrotRotZoffset = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePosition = Input.mousePosition;

        if (followMouse)
        {
            mousePosition.y += distanceY;
            mousePosition.z += distanceZ;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        // Porkkanalle rotate kohti hiirtä
        if (rotateToMouse)
        {
            Vector3 direction = mousePosition - carrotBase.position;
            direction.x -= carrotRotXoffset;
            direction.y -= carrotRotYoffset;
            direction.z -= carrotRotZoffset;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        }
    }
}
