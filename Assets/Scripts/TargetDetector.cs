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

    //[HideInInspector]
    public Transform nearestTarget;

    private void Start()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        if(character.IsDead) return;
        DetectTarget();
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
                    character.OnEnemyDetect();
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