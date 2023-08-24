using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected CreaturesList m_creaturesList;
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

    public void SetCreaturesList(CreaturesList list)
    {
        m_creaturesList = list;
    }
    protected abstract void SpawnObject();
}
