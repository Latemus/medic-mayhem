using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {

  // PUBLIC

  public bool driveable = false;

  public string upAndDownArrowsWithScheme = "Vertical";

  public string leftAndRightArrowsWithScheme = "Horizontal";

  public KeyCode brakeKey;


  // Wheel Colliders
  // Front
  public WheelCollider wheelFL;
  public WheelCollider wheelFR;
  // Rear
  public WheelCollider wheelRL;
  public WheelCollider wheelRR;

  public float maxTorque = 20f;
  public float brakeTorque = 100f;

  // max wheel turn angle;
  public float maxWheelTurnAngle = 30f; // degrees

  // car's center of mass
  public Vector3 centerOfMass = new Vector3(0f, 0f, 0f); // unchanged

  // PRIVATE
  // GUI
  //...
  [SerializeField]
  private float RO_speed;
  [SerializeField]
  private float RO_EngineTorque; 
  [SerializeField]
  private float RO_SteeringAngleFL; 
  [SerializeField]
  private float RO_SteeringAngleFR; 
  [SerializeField]
  private float RO_BrakeTorque; 

  // acceleration increment counter
  private float torquePower = 0f;

  // turn increment counter
  private float steerAngle = 0f;


  void Start () {
    GetComponent<Rigidbody>().centerOfMass = centerOfMass;
  }


  // Visual updates
  void Update () {
    if (! driveable) {
      return;
    }

    // Audio
    GetComponent<AudioSource>().pitch = (torquePower / maxTorque) + 0.5f;
  }

  // Physics updates
  void FixedUpdate () {
    if (! driveable) {
      return;
    }

    // CONTROLS - FORWARD & RearWARD
    if ( Input.GetKey(brakeKey) ) {
      // BRAKE
      torquePower = 0f;
      wheelRL.brakeTorque = brakeTorque;
      wheelRR.brakeTorque = brakeTorque;
    } else {
      // SPEED
      torquePower = maxTorque * Mathf.Clamp( Input.GetAxis(upAndDownArrowsWithScheme), -1, 1 );
      wheelRL.brakeTorque = 0f;
      wheelRR.brakeTorque = 0f;

    }
    // Apply torque
    wheelFR.motorTorque = torquePower;
    wheelFL.motorTorque = torquePower;
    wheelRR.motorTorque = torquePower;
    wheelRL.motorTorque = torquePower;


    /*// Debug.Log(Input.GetAxis("Vertical"));
    Debug.Log("torquePower: " + torquePower);
    Debug.Log("brakeTorque RL: " + wheelRL.brakeTorque);
    Debug.Log("brakeTorque RR: " + wheelRR.brakeTorque);
    Debug.Log("steerAngle: " + steerAngle);*/

    // CONTROLS - LEFT & RIGHT
    // apply steering to front wheels
    steerAngle = maxWheelTurnAngle * Input.GetAxis(leftAndRightArrowsWithScheme);
    wheelFL.steerAngle = steerAngle;
    wheelFR.steerAngle = steerAngle;
    wheelRL.steerAngle = -steerAngle/4f;
    wheelRR.steerAngle = -steerAngle/4f;

    // Debug info
    RO_BrakeTorque = wheelRL.brakeTorque;
    RO_SteeringAngleFL = wheelFL.steerAngle;
    RO_SteeringAngleFR = wheelFR.steerAngle;
    RO_EngineTorque = torquePower;

    // SPEED
    // debug info
    RO_speed = GetComponent<Rigidbody>().velocity.magnitude;

    // KEYBOARD INPUT

    // FORWARD
    if ( Input.GetKey(KeyCode.W) ) {
      // Debug.Log("FORWARD");
    }

    // BACKWARD
    if ( Input.GetKey(KeyCode.S) ) {
      // Debug.Log("BACKWARD");
    }

    // LEFT
    if ( Input.GetKey(KeyCode.A) ) {
      // Debug.Log("LEFT");
    }

    // RIGHT
    if ( Input.GetKey(KeyCode.D) ) {
      // Debug.Log("RIGHT");
    }

    // BRAKE
    if ( Input.GetKey(KeyCode.Space) ) {
      // Debug.Log("SPACE");
    }

  }
}