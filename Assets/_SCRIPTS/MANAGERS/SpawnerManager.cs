using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] private List<CreaturesList> m_enemyLists;

    [SerializeField] private List<Spawner> m_spawners = new List<Spawner>();

    [SerializeField] private ChestSpawner m_chestSpawner;

    public void SetSpawnersActive(bool isActive)
    {
        foreach (Spawner spawner in m_spawners)
        {
            spawner.enabled = isActive;
        }
        if (isActive)
            m_chestSpawner.MakeChestSpawn();
    }

    public void SetNewArea(int listId)
    {
        SetEnemyList(listId);
    }

    private CreaturesSpawner _enemySpawner;
    private void SetEnemyList(int listId)
    {
        foreach (Spawner s in m_spawners)
        {
            if (s.TryGetComponent<CreaturesSpawner>(out _enemySpawner))
            {
                _enemySpawner.SetCreaturesList(m_enemyLists[listId]);
            }
        }
        m_chestSpawner.CleanSpawnPoints();
    }
}
