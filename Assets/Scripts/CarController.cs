using JetBrains.Annotations;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    [Header("Car Settings")]
    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;

    private float currentSteerAngle;
    private float currentBrakeForce;
    private float currentAcceleration;

    /// <summary>
    /// 
    /// </summary>
    //[SerializeField] float m_moveSpeed;
    //[SerializeField] float m_turnSpeed;
    
    Rigidbody m_RB;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        //UpdateWheelMeshes();
        /////
        /////
        //float move = Input.GetAxis("Vertical") * m_moveSpeed * Time.fixedDeltaTime;
        //float turnInput = Input.GetAxis("Horizontal");
        //float turn = 0;
        //if (Mathf.Abs(move) > 0.0001f) // prevent turning when idle
        //{
            
        //    turn = turnInput * m_turnSpeed * Time.fixedDeltaTime;
        //}
        //Debug.Log(move);
        //Vector3 forward = transform.forward * move;
        //Quaternion turnRot = Quaternion.Euler(0, turn, 0);

        //m_RB.MovePosition(m_RB.position + forward);
        //m_RB.MoveRotation(m_RB.rotation * turnRot);
    }

    private void HandleMotor()
    {
        float verticalInput = Input.GetAxis("Vertical");

        currentAcceleration = verticalInput * motorForce;
        currentBrakeForce = Input.GetKey(KeyCode.Space) ? brakeForce : 0f;

        rearLeftCollider.motorTorque = currentAcceleration;
        rearRightCollider.motorTorque = currentAcceleration;

        ApplyBrakes();
    }

    private void ApplyBrakes()
    {
        frontLeftCollider.brakeTorque = currentBrakeForce;
        frontRightCollider.brakeTorque = currentBrakeForce;
        rearLeftCollider.brakeTorque = currentBrakeForce;
        rearRightCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        currentSteerAngle = maxSteerAngle * horizontalInput;

        frontLeftCollider.steerAngle = currentSteerAngle;
        frontRightCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheelMeshes()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftMesh);
        UpdateSingleWheel(frontRightCollider, frontRightMesh);
        UpdateSingleWheel(rearLeftCollider, rearLeftMesh);
        UpdateSingleWheel(rearRightCollider, rearRightMesh);
    }

    private void UpdateSingleWheel(WheelCollider col, Transform wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);
        wheelMesh.position = pos;
        wheelMesh.rotation = rot;
    }
}
