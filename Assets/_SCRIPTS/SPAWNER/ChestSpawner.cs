using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject chestPrefab;

    private List<Transform> pickedSpawnPoint = new List<Transform>();
    public void MakeChestSpawn()
    {
        int j = 0;

        for (int i = 0; i < 10; ++i)
        {
            j = Random.Range(0, spawnPoints.Count);
            pickedSpawnPoint.Add(spawnPoints[j]);
            spawnPoints.RemoveAt(j);
            Instantiate(chestPrefab, pickedSpawnPoint[i].position, Quaternion.identity, pickedSpawnPoint[i]);
        }

        foreach(Transform t in pickedSpawnPoint)
            spawnPoints.Add(t);
    }

    public void CleanSpawnPoints()
    {
        for (int i = 0; i < pickedSpawnPoint.Count; ++i)
        {
            if (pickedSpawnPoint[i].childCount > 0)
                Destroy(pickedSpawnPoint[i].GetChild(0));
        }

        pickedSpawnPoint.Clear();
    }
}
