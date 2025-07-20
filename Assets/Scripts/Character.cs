using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float health = 100f;
    protected bool isDead = false;
    public bool isZombie = false;
    protected virtual void Start()
    {
        // Optional: Set up any common things
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        GameManager.Instance.UnregisterCharacter(this);
        // Disable physics/colliders or play death animation
        gameObject.SetActive(false); // Quick and easy for now
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Dead");
            TakeDamage(100f); // Instant death
        }
    }
}