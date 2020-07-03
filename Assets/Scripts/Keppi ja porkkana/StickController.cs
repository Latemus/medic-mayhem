using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public float stick_up_height;
    public float stick_down_height;
    public float stick_down_time;
    public float maxDistance; 
    public LayerMask layerMask;
    private float stick_target_height; 
    private float stick_lowered_until;
    private float rotation_target = 0;

    // The gains are chosen experimentally
    [SerializeField] public float Kp = 1;
    [SerializeField] public float Ki = 1;
    [SerializeField] public float Kd = 1;

    private float last_time = 0;
    private float dt = 0;
    private float prevError;
    private float P, I, D;


    // FixedUpdate is called once physics per frame
    void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxDistance, layerMask)) {
            Vector3 target = hit.point;
            target.y = stick_target_height;
            GetComponent<Rigidbody>().AddForce((target - transform.position) * 1000);
            // TODO: Height
        }
        RotateStickTowardsTarget();
    }


    void Update() {
        if (Input.GetMouseButtonDown(0) && StickLoweringIsAvailable()) { 
            //stick_lowered_until = Time.time + stick_down_time;
            PutStickDown();
        }
        else if (Input.GetMouseButtonUp(0)) {
            // TODO: Report to player, why putting stick down is not possible
            PutStickUp();
        }
        // DetermineStickHeight();
        rotation_target += Input.GetAxis("Mouse ScrollWheel") * 75;
        transform.eulerAngles = new Vector3(0, rotation_target, 0);
    }


    void DetermineStickHeight() {
        if (stick_lowered_until < Time.time) { 
            stick_target_height = stick_up_height; 
            GetComponent<Renderer>().material.color = new Color(1.0f, 0, 0, 0.5f);
        }
        else {
            GetComponent<Renderer>().material.color = new Color(1.0f, 0, 0, 1.0f);
            stick_target_height = stick_down_height; 
        }
    }

    void PutStickUp() { 
        stick_target_height = stick_up_height; 
        GetComponent<Renderer>().material.color = new Color(1.0f, 0, 0, 0.5f);
    }

    void PutStickDown() {
            GetComponent<Renderer>().material.color = new Color(1.0f, 0, 0, 1.0f);
            stick_target_height = stick_down_height; 
    }

    bool StickLoweringIsAvailable() {
        return true;
    }

    void RotateStickTowardsTarget() {
        float kulma = rotation_target - transform.rotation.y;
        if (kulma > 1.0f) {
            kulma = -2.0f + kulma;
        }
        float force = PID(kulma);
        Debug.Log(  "   Rotation targe: " + rotation_target + 
                    "   rotation.y: " + transform.rotation.y + 
                    "   Kulma: " + kulma + 
                    "   Force: " + force);
        GetComponent<Rigidbody>().AddTorque(new Vector3(0, force, 0));
    }


    // Returns the torgue
    public float PID(float currentError)
    {
        dt = Time.deltaTime;
        last_time = Time.time;

        P = currentError;
        I += P * dt;
        D = (P - prevError) / dt;
        prevError = currentError;

        return P * Kp + I * Ki + D * Kd;
    }
}
