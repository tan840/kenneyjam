using UnityEngine;

public class Human : Character
{
    protected override void Die()
    {
        base.Die();
        // Play human-specific death effect or sound
        GameManager.Instance.AddKill(false);
        //Debug.Log("Human died!");
    }

    void Update()
    {
        // Later: Idle behavior or reaction logic
    }
}