using System.Collections;
using UnityEngine;

public class ShootMechanics : MonoBehaviour
{
    [SerializeField] float m_RotationSpeed = 3f;
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] static float m_shootDelay = 3f;
    [SerializeField] GameObject m_Bullet;
    //[SerializeField] GameObject m_target;
    TargetDetector m_Detector;
    bool m_canShoot = true;
    [SerializeField] Transform m_FirePoint;
    WaitForSeconds shootDelay = new WaitForSeconds(m_shootDelay);
    private void Start()
    {
        m_Detector = GetComponent<TargetDetector>();
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
        if (m_canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        m_canShoot = false;
        yield return shootDelay;
        if (m_Detector.nearestTarget != null)
        {
            GameObject bullet = Instantiate(m_Bullet, m_FirePoint.position, m_FirePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = m_FirePoint.forward * bulletSpeed;
        }
        m_canShoot = true;
    }
}

