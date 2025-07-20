using UnityEngine;

public class Zombie : Character
{
    public bool shouldChase = false;

    protected override void Die()
    {
        base.Die();
        // Play zombie-specific death effect or ragdoll
        Debug.Log("Zombie killed!");
    }

    void Update()
    {
        if (shouldChase)
        {
            // Placeholder for NavMesh chase logic
            Debug.Log("Zombie chasing...");
        }
    }
}
