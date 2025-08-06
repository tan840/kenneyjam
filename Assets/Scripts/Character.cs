using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float health = 100f;
    private bool isDead = false;
    public bool isZombie = false;

    public bool IsDead { get => isDead;}

    protected virtual void Start()
    {

    }

    public virtual void TakeDamage(float amount)
    {
        if (IsDead) return;

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
        //if (isZombie)
            //SoundManager.Instance.PlayZombieKillSound();
        //else
            //SoundManager.Instance.PlayHumanKillSound();
        //GameManager.Instance.UnregisterCharacter(this);
        gameObject.SetActive(false);
    }
    public abstract void OnEnemyDetect();


    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.name);
        if (IsDead) return;

        if (collision.collider.CompareTag("Player"))
        {
            TakeDamage(100f);
        }
    }
}