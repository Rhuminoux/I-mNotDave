using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveStats : MonoBehaviour
{
    [SerializeField] private float m_currentOxygen;
    public float maxOxygen;

    public int collectedGold = 0;

    public Action<int> onGoldChange;
    public Action<float> onOxygenChange;
    public Action onDrawn;

    private void Awake()
    {
        m_currentOxygen = maxOxygen;
    }

    private void Update()
    {
        m_currentOxygen -= 1 * Time.deltaTime;
        if (m_currentOxygen < 0)
            Drawn();
        ChangeOxygen();
    }

    private void Drawn()
    {
        onDrawn.Invoke();
    }

    public void AddGold(int goldToAdd)
    {
        collectedGold += goldToAdd;
        onGoldChange.Invoke(collectedGold);
    }

    private void ChangeOxygen()
    {
        onOxygenChange((m_currentOxygen * 100) / maxOxygen);
    }

    public void RemoveOxygen(float oxygenToRemove)
    {
        m_currentOxygen -= oxygenToRemove;
        ChangeOxygen();
    }
}
