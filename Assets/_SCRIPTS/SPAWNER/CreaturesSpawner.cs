using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesSpawner : Spawner
{
    // Update is called once per frame
    void Update()
    {
        if (m_timeSinceLastSpawn > m_spawnFrequency)
        {
            SpawnObject();
        }
        else
            m_timeSinceLastSpawn += Time.deltaTime;
    }

    protected override void SpawnObject()
    {
        Instantiate(m_creaturesList.GetCreature(), transform);
        m_timeSinceLastSpawn = 0;
        GetRandomSpawnFrequency();
    }
}
