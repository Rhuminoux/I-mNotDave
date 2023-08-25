using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreaturesList", menuName = "ScriptableObjects/CreaturesList", order = 1)]
public class CreaturesList : ScriptableObject
{
    [SerializeField] private List<GameObject> m_creaturesList;

    public GameObject GetCreature()
    {
        return m_creaturesList[Random.Range(0, m_creaturesList.Count - 1)];
    }
}
