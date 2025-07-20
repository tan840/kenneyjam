using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 10f;
    public float deceleration = 5f;
    public float turnSpeed = 50f;
    public float damping = 3f;

    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public float maxWheelTurnAngle = 30f; // Visual turn angle for wheels

    private float currentSpeed = 0f;
    private float currentSteerInput = 0f;

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");   // W/S or Up/Down
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right

        // Acceleration and Deceleration
        if (verticalInput > 0f)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (verticalInput < 0f)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else
        {
            if (currentSpeed > 0f)
                currentSpeed -= damping * Time.deltaTime;
            else if (currentSpeed < 0f)
                currentSpeed += damping * Time.deltaTime;

            if (Mathf.Abs(currentSpeed) < 0.1f)
                currentSpeed = 0f;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2f, maxSpeed);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnAmount = horizontalInput * turnSpeed * Time.deltaTime * Mathf.Sign(currentSpeed);
            transform.Rotate(0f, turnAmount, 0f);
        }

        RotateWheels(currentSteerInput);
    }

    void RotateWheels(float steerInput)
    {
        float targetAngle = steerInput * maxWheelTurnAngle;
        if (frontLeftWheel != null)
            frontLeftWheel.localRotation = Quaternion.Euler(0f, targetAngle, 0f);
        if (frontRightWheel != null)
            frontRightWheel.localRotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
}