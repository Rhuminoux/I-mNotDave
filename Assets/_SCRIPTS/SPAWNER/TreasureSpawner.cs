using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : Spawner
{

    // Update is called once per frame
    void Update()
    {
        if (m_timeSinceLastSpawn > m_spawnFrequency)
            SpawnObject();
        else
            m_timeSinceLastSpawn += Time.deltaTime;
    }

    private Vector3 _spawnPosition;
    //TODO improve spawn function
    protected override void SpawnObject()
    {
        _spawnPosition.x = PlayerController.Singleton.transform.position.x + Random.Range(-5.8f, 5.8f);
        _spawnPosition.y = PlayerController.Singleton.transform.position.y - 5.5f;
        Instantiate(m_objectsToSpawn[0], _spawnPosition, Quaternion.identity, transform);
        m_timeSinceLastSpawn = 0;
        GetRandomSpawnFrequency();
    }
}
