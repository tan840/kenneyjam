using UnityEngine;
using UnityEngine.AI;

public class HumanBehaviour : MonoBehaviour
{
    Human Human;
    Animator animator;
    NavMeshAgent agent;
    //Patrol
    [SerializeField] float patrolSpeed = 0.3f;
    [SerializeField] float patrolRadius = 10f;
    [SerializeField] float patrolWaitTime = 2f;
    private float patrolTimer = 0;
    private Vector3 patrolTarget;
    private bool hasPatrolTarget = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Patrol()
    {
        patrolTimer += Time.deltaTime;
        agent.speed = patrolSpeed;
        if (!hasPatrolTarget || agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
            {

            }
        }
    }
    public void Run()
    {

    }
    public void Idle()
    {

    }
    public void Rescued()
    {

    }
    public void Dead()
    {

    }
}
