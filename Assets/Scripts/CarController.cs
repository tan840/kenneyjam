using Unity.VisualScripting;
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

    private Rigidbody rb;
    VehicleInputSystem inputSystem;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponent<VehicleInputSystem>();
    }

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheelMeshes();
    }

    private void HandleMotor()
    {
        //float verticalInput = Input.GetAxis("Vertical");
        //print("Horizontal" + inputSystem.GetVerticalAxis());
        float verticalInput = inputSystem.GetVerticalAxis();

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
        //float horizontalInput = Input.GetAxis("Horizontal");
        //print("Horizontal" + inputSystem.GetHorizontalAxis());
        float horizontalInput = inputSystem.GetHorizontalAxis();
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
