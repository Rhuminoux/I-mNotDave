using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class DiveStats : MonoBehaviour
{
    [SerializeField] private float m_currentOxygen;
    public float maxOxygen;
    public int collectedGold = 0;

    private bool m_goingDown = true;
    [SerializeField]private float m_deepness;

    [HideInInspector] public Action<int> onGoldChange;
    [HideInInspector] public Action<float> onOxygenChange;
    [HideInInspector] public Action<int> onDeepnessChange;
    [HideInInspector] public Action onDrawn;

    private void Awake()
    {
        m_currentOxygen = maxOxygen;
    }

    private void Update()
    {
        Move();
        m_currentOxygen -= 1 * Time.deltaTime;
        if (m_currentOxygen < 0)
            Drawn();
        ChangeOxygen();
    }

    private int _intDeepness = 0;
    private void Move()
    {
        if (m_goingDown)
        {
            m_deepness += 1 * Time.deltaTime;
            
            if (m_deepness > _intDeepness + 1)
            {
                _intDeepness = (int)Math.Ceiling(m_deepness) - 1;
                onDeepnessChange.Invoke(_intDeepness);
            }
        }
        else
        {
            m_deepness -= 2 * Time.deltaTime;
            
            if (m_deepness <= 0)
                m_deepness = 0;

            if (m_deepness <= _intDeepness - 1)
            {
                _intDeepness = (int)Math.Ceiling(m_deepness);
                onDeepnessChange.Invoke(_intDeepness);
            }

        }
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
        onOxygenChange.Invoke((m_currentOxygen * 100) / maxOxygen);
    }

    public void RemoveOxygen(float oxygenToRemove)
    {
        m_currentOxygen -= oxygenToRemove;
        ChangeOxygen();
    }

    public void StartGoingUp()
    {
        m_goingDown = false;
    }

    internal void BoostAscent(int bonus)
    {
        m_deepness -= bonus;
    }
}
