using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleInputSystem : MonoBehaviour
{
    [SerializeField] InputActionAsset inputAction;
    InputAction m_Movement;
    Vector2 m_Velocity;

    private void Awake()
    {
        m_Movement = inputAction.FindAction("Movement");
    }
    private void OnEnable()
    {
        inputAction.FindActionMap("VehicalControl").Enable(); ;
    }
    private void OnDisable()
    {
        inputAction.FindActionMap("VehicalControl").Disable(); ;
    }
    public float GetHorizontalAxis()
    {
        return m_Movement.ReadValue<Vector2>().x;
    }
    public float GetVerticalAxis()
    {
        return m_Movement.ReadValue<Vector2>().y;
    }
}
