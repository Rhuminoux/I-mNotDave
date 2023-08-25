using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> m_spawners = new List<Spawner>();

    public void SetSpawnersActive(bool isActive)
    {
        foreach(Spawner spawner in m_spawners)
        {
            spawner.enabled = isActive;
        }
    }
}
