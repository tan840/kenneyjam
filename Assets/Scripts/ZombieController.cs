using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Character character;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
    }

    //void Update()
    //{
    //    if (!character.isZombie) return;

    //    if (GameManager.Instance.HumanKills < GameManager.Instance.ZombieKills)
    //    {
    //        print("follow");
    //        MoveToNearestHuman();
    //    }
    //    //print("dontfollow");
    //}

    void MoveToNearestHuman()
    {
        Character nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var human in GameManager.Instance.humans)
        {
            if (!human) continue;

            float dist = Vector3.Distance(transform.position, human.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = human;
            }
        }

        if (nearest)
        {
            agent.SetDestination(nearest.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character otherChar = other.GetComponent<Character>();
        if (otherChar && !otherChar.isZombie)
        {
            otherChar.TakeDamage(100); // kill the human
        }
    }
}