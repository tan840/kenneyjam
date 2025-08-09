using UnityEngine;
using Unity.Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    //[SerializeField] CinemachineImpulseListener _listener;
    [SerializeField] float _impulse;

    public static CameraShakeManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void Shake(CinemachineImpulseSource _Source)
    {
        _Source.GenerateImpulse(_impulse);
    }
}
