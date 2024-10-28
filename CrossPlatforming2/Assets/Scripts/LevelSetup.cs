using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public GameObject[] spawnPoints;  // Assign 5 empty GameObjects in the Inspector
    public GameObject objectToSpawn;  // Assign the GameObject to spawn

    private void Start()
    {
        SpawnObjects();
        TriggerIntentionalExceptions();
    }

    private void SpawnObjects()
    {
        if (spawnPoints.Length < 5)
        {
            Debug.LogError("Not enough spawn points set up.");
            return;
        }

        HashSet<int> chosenIndices = new HashSet<int>();

        // Randomly select three unique spawn points
        while (chosenIndices.Count < 3)
        {
            int index = Random.Range(0, spawnPoints.Length);
            if (chosenIndices.Add(index))
            {
                Instantiate(objectToSpawn, spawnPoints[index].transform.position, Quaternion.identity);
            }
        }
    }

    private void TriggerIntentionalExceptions()
    {
        try
        {
            // Intentional null reference exception
            GameObject nullObject = null;
            nullObject.transform.position = Vector3.zero;
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogWarning("NullReferenceException triggered: " + e.Message);
        }

        try
        {
            // Intentional out of range exception
            int[] numbers = new int[3];
            int outOfRangeNumber = numbers[5];
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.LogWarning("IndexOutOfRangeException triggered: " + e.Message);
        }
    }
}
