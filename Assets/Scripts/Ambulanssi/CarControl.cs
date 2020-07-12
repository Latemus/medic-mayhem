using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CarControl : MonoBehaviour {
  // PUBLIC
  public bool driveable = false;
  public string upAndDownArrowsWithScheme = "Vertical";
  public string leftAndRightArrowsWithScheme = "Horizontal";

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

  private float torquePower = 0f;
  private float steerAngle = 0f;
  private GameObject home_hospital; 
  private float playerInactivityTimer = 0f;

  void Start () {
    GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    GetComponent<NavMeshAgent>().updatePosition = false;
    home_hospital = GameObject.FindWithTag("tan_hospital");
  }

  void FixedUpdate() 
  {
    //GetComponent<AudioSource>().pitch = (torquePower / maxTorque) + 0.5f;
    if (AIisNotActive()) 
    {
      AddPlayerInputForSteering();
    }
    else 
    {
      AddAIinputForSteering();
    }
    ApplyTorgue();
    #if UNITY_EDITOR
    UpdateDebugInfoInEditor();
    #endif
  }

  // Checks if the AI should be activated or not
  private bool AIisNotActive() 
  {
    if(anyOfThePlayerButtonsArePressed())
    {
      playerInactivityTimer = Time.time + 10.0f;
    }
    else if(playerInactivityTimer < Time.time)
    {
      // AI is active, because of player inactivity
      return false;
    }
    // AI is not active, because of player activity
    return true; 
  }

  private bool anyOfThePlayerButtonsArePressed() 
  {
    if (Input.GetAxis(upAndDownArrowsWithScheme) != 0 || Input.GetAxis(leftAndRightArrowsWithScheme) != 0)
    {
      return true;
    }
    else 
    {
      return false;
    }
  }

  private void AddPlayerInputForSteering() 
  {
    torquePower = maxTorque * Mathf.Clamp( Input.GetAxis(upAndDownArrowsWithScheme), -1, 1 );
    steerAngle = maxWheelTurnAngle * Input.GetAxis(leftAndRightArrowsWithScheme);
  }
  private void AddAIinputForSteering() 
  {
    // TODO
    // If I have passenger, target is hospital
    GameObject target = null;
    if (GetComponent<AmbulanceStatus>().soldierCount > 0) 
    {
      target = home_hospital;
    }
    else if (target == null)
    {
      // If there are wounded soldiers, target is wounded soldier
      target = GameObject.FindWithTag("woundedSoldier");
    }
    if (target != null)
    {
      // Toggle this when hospital is target? 
      //GetComponent<NavMeshAgent>().enabled = false;
      //GetComponent<NavMeshAgent>().enabled = true;
      GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
      float angleBetweenTargetAndFacing = Vector3.SignedAngle(transform.position - GetComponent<NavMeshAgent>().steeringTarget, transform.forward, Vector3.up);
      steerAngle = Mathf.Clamp(angleBetweenTargetAndFacing, -maxWheelTurnAngle, maxWheelTurnAngle);
      Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
      Debug.DrawRay(transform.position, GetComponent<NavMeshAgent>().steeringTarget - transform.position, Color.blue);
      torquePower = maxTorque;
    }
    else
    {
      steerAngle = 0;
      torquePower = 0;
    }
  }

  // Check how many wounded soldiers are inside the cargobay
  private void CheckNumberOfPassengers() 
  {
    // TODO: 
    //Use the OverlapBox to detect if there are any other colliders within this box area.
    //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject. 
    //Vector3 overlapBoxScale = new Vector3(transform.localScale.)
    //Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + new Vector3(0, 0.04, -0.17), transform.localScale, Quaternion.identity, m_LayerMask);
  }

  // Updates the values visible in the Unity Editor for debug purposes
  private void UpdateDebugInfoInEditor()
  {
    RO_SteeringAngleFL = wheelFL.steerAngle;
    RO_SteeringAngleFR = wheelFR.steerAngle;
    RO_EngineTorque = torquePower;
    RO_speed = GetComponent<Rigidbody>().velocity.magnitude;
  }
  
  // Apply torque to the wheels
  private void ApplyTorgue() 
  {
    wheelFR.motorTorque = torquePower;
    wheelFL.motorTorque = torquePower;
    wheelRR.motorTorque = torquePower;
    wheelRL.motorTorque = torquePower;
    wheelFL.steerAngle = steerAngle;
    wheelFR.steerAngle = steerAngle;
    wheelRL.steerAngle = -steerAngle/4f;
    wheelRR.steerAngle = -steerAngle/4f;
  }
}

