using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField]protected List<GameObject> m_objectsToSpawn =  new List<GameObject>();
    [SerializeField] protected float m_spawnFrequencyMin = 5.0f, m_spawnFrequencyMax = 10.0f, m_spawnFrequency;
    [SerializeField] protected float m_timeSinceLastSpawn;

    private void Awake()
    {
        GetRandomSpawnFrequency();
    }

    protected void GetRandomSpawnFrequency()
    {
        m_spawnFrequency = Random.Range(m_spawnFrequencyMin, m_spawnFrequencyMax);
    }
    protected abstract void SpawnObject();
}
