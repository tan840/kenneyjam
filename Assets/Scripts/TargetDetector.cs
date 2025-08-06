using System.Collections;
using System.IO.Compression;
using System.Linq;
using Unity.Behavior;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public float detectionRadius = 10f;
    public LayerMask targetMask;
    public string targetTag;
    [SerializeField] Character character;
    [SerializeField] static float m_checkFrequency = 0.3f;
    WaitForSeconds checkFrequency = new WaitForSeconds(m_checkFrequency);
    //[HideInInspector]
    public Transform nearestTarget;

    private void Start()
    {
        character = GetComponent<Character>();
        StartCoroutine(CheckForTarget());
    }

    IEnumerator CheckForTarget()
    {
        //if(character.IsDead) return;
        while (true)
        {
            DetectTarget();
            yield return checkFrequency;
        }

    }

    void DetectTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, targetMask);

        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag(targetTag))
            {
                float dist = Vector3.Distance(transform.position, hit.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closest = hit.transform;
                    //character.OnEnemyDetect();
                }
            }
        }

        nearestTarget = closest;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}