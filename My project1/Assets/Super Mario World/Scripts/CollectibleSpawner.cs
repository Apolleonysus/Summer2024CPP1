using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public List<GameObject> collectibles; 
    public List<Transform> spawnPoints;   

    void Start()
    {
        if (spawnPoints.Count < 5 || collectibles.Count == 0)
        {
            Debug.LogError("Not enough spawn points or collectibles defined.");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnRandomCollectible(spawnPoint);
        }
    }

    void SpawnRandomCollectible(Transform spawnLocation)
    {
        int randomIndex = Random.Range(0, collectibles.Count);
        GameObject collectibleToSpawn = collectibles[randomIndex];

        Instantiate(collectibleToSpawn, spawnLocation.position, Quaternion.identity);
    }
}
