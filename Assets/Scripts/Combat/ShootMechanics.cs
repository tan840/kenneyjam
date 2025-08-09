using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ShootMechanics : MonoBehaviour
{
    [SerializeField] float m_RotationSpeed = 3f;
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] static float m_shootDelay = 3f;
    [SerializeField] GameObject m_Bullet;
    [SerializeField] LayerMask m_LayerMask;
    //[SerializeField] GameObject m_target;
    TargetDetector m_Detector;
    bool m_canShoot = true;
    [SerializeField] Transform m_FirePoint;
    WaitForSeconds shootDelay = new WaitForSeconds(m_shootDelay);
    CinemachineImpulseSource m_Source;
    CameraShakeManager m_ShakeManager;
    private void Start()
    {
        m_Detector = GetComponent<TargetDetector>();
        m_Source = GetComponentInParent<CinemachineImpulseSource>();
        m_ShakeManager = CameraShakeManager.instance;
    }
    private void Update()
    {
        if (m_Detector != null && m_Detector.nearestTarget != null)
            PointAtTarget();
    }
    void PointAtTarget()
    {
        //if (target == null) return;

        Vector3 direction = m_Detector.nearestTarget.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * m_RotationSpeed
        );
        RaycastHit hit;
        if (Physics.Raycast(m_FirePoint.position, m_FirePoint.forward,out hit, m_Detector.detectionRadius, m_LayerMask))
        {
            if (m_canShoot && hit.collider)
            {
                StartCoroutine(Shoot());
            }
        }

    }
    IEnumerator Shoot()
    {
        m_canShoot = false;
        if (m_Detector.nearestTarget != null)
        {
            GameObject bullet = Instantiate(m_Bullet, m_FirePoint.position, m_FirePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = m_FirePoint.forward * bulletSpeed;
            m_ShakeManager.Shake(m_Source);
        }
        yield return shootDelay;
        m_canShoot = true;
    }
}

