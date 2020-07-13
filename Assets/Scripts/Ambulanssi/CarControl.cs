using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CarControl : MonoBehaviour {
  // PUBLIC
  public bool driveable = false;
  public string upAndDownArrowsWithScheme = "Vertical";
  public string leftAndRightArrowsWithScheme = "Horizontal";
  public string hospitalTag = "tan_hospital";

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
  private float angleBetweenTargetAndFacing;
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
  [SerializeField]
  private GameObject target; 

  private float torquePower = 0f;
  private float steerAngle = 0f;
  private GameObject home_hospital; 
  private float playerInactivityTimer = 0f;
  private float aiRepathingTimer = 0f;

  void Start () {
    GetComponent<Rigidbody>().centerOfMass = centerOfMass;
    GetComponent<NavMeshAgent>().updatePosition = false;
    GetComponent<NavMeshAgent>().updateRotation = false;
    home_hospital = GameObject.FindWithTag(hospitalTag);
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
    GetComponent<NavMeshAgent>().nextPosition = transform.position;
    target = null;
    if (Vector3.Distance(home_hospital.transform.position, transform.position) < 5) 
    {
      target = null;
      GetComponent<AmbulanceStatus>().soldierCount = 0;
    }

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

      if (Time.time > aiRepathingTimer)
      {
        aiRepathingTimer += 2; 
        //GetComponent<NavMeshAgent>().enabled = false;
        //GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().destination = target.transform.position;
      }

      angleBetweenTargetAndFacing = Vector3.SignedAngle(transform.position - GetComponent<NavMeshAgent>().steeringTarget, -transform.forward, Vector3.up);
      steerAngle = Mathf.Clamp(-angleBetweenTargetAndFacing, -maxWheelTurnAngle, maxWheelTurnAngle);
      Debug.DrawRay(transform.position, transform.forward * 6, Color.blue);
      Debug.DrawRay(transform.position, GetComponent<NavMeshAgent>().steeringTarget - transform.position, Color.red);
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
    //Use the OverlapBox to detect if there are any other colliders within this box area.
    //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject. 
    Vector3 overlapBoxScale = new Vector3(0.77f, 0.53f, 1.06f); // These values were determined with the editor with eyeballing
    Collider[] hitColliders = Physics.OverlapBox(transform.position + new Vector3(0f, 0.04f, -0.17f), 
                                                Vector3.Scale(transform.localScale, overlapBoxScale) / 2, 
                                                transform.rotation, 
                                                0);
  }
  #if UNITY_EDITOR
  void OnDrawGizmos()
  {
    Vector3 overlapBoxScale = new Vector3(0.77f, 0.53f, 1.06f); // These values were determined with the editor with eyeballing
    Gizmos.color = Color.green;
    Gizmos.matrix = transform.localToWorldMatrix;
    Gizmos.DrawWireCube(new Vector3(0f, 0.04f, -0.17f), overlapBoxScale);
  }
  #endif

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

