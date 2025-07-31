using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    Zombie Zombie;
    TargetDetector detector;
    NavMeshAgent agent;
    [SerializeField] float m_attackRange = 1f;
    //bool isDead = false;

    Animator m_anim;
    //partrol
    [SerializeField] float patrolRadius = 10f;
    [SerializeField] float patrolWaitTime = 2f;
    [SerializeField] float patrolSpeed = 0.03f;
    [SerializeField] float chaseSpeed = 0.5f;

    private float patrolTimer = 0f;
    private Vector3 patrolTarget;
    private bool hasPatrolTarget = false;
    private void Start()
    {
        Zombie = GetComponent<Zombie>();
        detector = GetComponent<TargetDetector>();
        agent = GetComponent<NavMeshAgent>();
        m_anim = GetComponentInChildren<Animator>();
    }
    public void HandleIdle()
    {
        SetAnim("isIdle", true);
    }
    public void HandleChase()
    {
        if (detector.nearestTarget == null) return;
        float distance = Vector3.Distance(transform.position, detector.nearestTarget.position);
        if (distance <= m_attackRange)
        {
            Zombie.ChangeState(ZombieState.Attack);
        }
        SetAnim("isRunning", true);
        agent.speed = chaseSpeed;
        agent.SetDestination(detector.nearestTarget.position);
    }
    public void HandlePatrol()
    {
        patrolTimer += Time.deltaTime;
        agent.speed = patrolSpeed;
        if (!hasPatrolTarget || agent.remainingDistance <= agent.stoppingDistance)
        {
            if (patrolTimer >= patrolWaitTime)
            {
                Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
                randomDirection += transform.position;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
                {
                    patrolTarget = hit.position;
                    agent.SetDestination(patrolTarget);
                    SetAnim("isWalking", true);
                    hasPatrolTarget = true;
                }

                patrolTimer = 0f;
            }
        }
    }
    public void HandleAttack()
    {
        if (detector.nearestTarget == null)
        {
            Zombie.ChangeState(ZombieState.Idle);
        }
        else
        {
            float distance = Vector3.Distance(transform.position, detector.nearestTarget.position);
            if (distance > m_attackRange)
            {
                Zombie.ChangeState(ZombieState.Chase);
            }
            Zombie.ChangeState(ZombieState.Attack);
            SetAnim("isAttacking", true);
        }
    }
    public void HandleDeath()
    {
        if (Zombie.IsDead) return;
        SetAnim("isDead", true);
        GameManager.Instance.AddKill(true);
    }
    void SetAnim(string animName, bool val = true)
    {
        m_anim.SetBool("isRunning", false);
        m_anim.SetBool("isAttacking", false);
        m_anim.SetBool("isWalking", false);
        m_anim.SetBool(animName, val);
    }
}
