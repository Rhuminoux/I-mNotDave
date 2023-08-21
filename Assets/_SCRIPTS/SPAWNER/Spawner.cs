using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField]protected List<GameObject> m_objectsToSpawn =  new List<GameObject>();
    [SerializeField] protected float m_spawnFrequency = 5.0f;
    [SerializeField] protected float m_timeSinceLastSpawn;
    protected abstract void SpawnObject();
}
