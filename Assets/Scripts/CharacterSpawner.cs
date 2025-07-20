using UnityEngine;
using System.Collections.Generic;

public class CharacterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Vector3 position;
        public bool isZombie; // true = zombie, false = human
    }

    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [Header("Prefabs")]
    [SerializeField] GameObject[] zombiePrefabs;
    [SerializeField] GameObject[] humanPrefabs;


    [Header("Debug Settings")]
    public float gizmoRadius = 0.5f;



    void Start()
    {
        SpawnAll();
        GameManager.Instance.UpdateUI();
    }

    public void SpawnAll()
    {
        foreach (var point in spawnPoints)
        {
            if (point.isZombie)
            {
                GameManager.Instance.ZombieKills++;
            }
            else
            {
                GameManager.Instance.HumanKills++;
            }
            GameObject prefab = point.isZombie
                ? GetRandomPrefab(zombiePrefabs)
                : GetRandomPrefab(humanPrefabs);

            Instantiate(prefab, point.position, Quaternion.identity);
        }
    }

    GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        if (prefabs == null || prefabs.Length == 0) return null;
        return prefabs[Random.Range(0, prefabs.Length)];
    }

    void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;

        foreach (var point in spawnPoints)
        {
            Gizmos.color = point.isZombie ? Color.red : Color.green;
            Gizmos.DrawSphere(point.position, gizmoRadius);
        }
    }
}