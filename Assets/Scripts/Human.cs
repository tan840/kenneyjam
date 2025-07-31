using UnityEngine;

public class Human : Character
{
    public HumanState currentState;
    HumanBehaviour HumanBehaviour;

    protected override void Start()
    {
        base.Start();
        HumanBehaviour = GetComponent<HumanBehaviour>();
    }
    public override void OnEnemyDetect()
    {
        print("Run To Safety");
    }

    protected override void Die()
    {
        base.Die();
        GameManager.Instance.AddKill(false);
    }

    void Update()
    {
        switch (currentState)
        {
            case HumanState.Idle:
                break;
            case HumanState.Patrol:
                break;
            case HumanState.Run:
                break;
            case HumanState.Rescued:
                break;
            case HumanState.Dead:
                break;
            default:
                break;
        }
    }
    public void ChangeState(HumanState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
    }
}