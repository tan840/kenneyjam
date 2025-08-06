using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && other.TryGetComponent(out Character character))
        {
            character.TakeDamage(50);
        }   
    }
}
