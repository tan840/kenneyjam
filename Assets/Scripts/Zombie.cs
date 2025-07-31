using UnityEngine;

public class Zombie : Character
{
    [SerializeField] ZombieState currentState;
    ZombieBehaviour ZombieBehaviour;
    TargetDetector targetDetector;
    protected override void Start()
    {
        base.Start();
        isZombie = true;
        ZombieBehaviour = GetComponent<ZombieBehaviour>();
        targetDetector = GetComponent<TargetDetector>();
        ChangeState(ZombieState.Patrol);
    }
    protected override void Die()
    {
        base.Die();
        ChangeState(ZombieState.Dead);
    }

    void Update()
    {
        switch (currentState)
        {
            case ZombieState.Idle:
                ZombieBehaviour.HandleIdle();
                break;
            case ZombieState.Patrol:
                ZombieBehaviour.HandlePatrol();
                break;
            case ZombieState.Chase:
                ZombieBehaviour.HandleChase();
                break;
            case ZombieState.Attack:
                ZombieBehaviour.HandleAttack();
                break;
            case ZombieState.Dead:
                ZombieBehaviour.HandleDeath();
                break;
            default:
                ZombieBehaviour.HandleIdle();
                break;
        }
    }
    public override void OnEnemyDetect()
    {
        if (targetDetector.nearestTarget != null) return;
        ChangeState(ZombieState.Chase);
    }
    public void ChangeState(ZombieState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
    }
}
