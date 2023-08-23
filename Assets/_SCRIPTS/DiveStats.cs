using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class DiveStats : MonoBehaviour
{
    public float maxOxygen;
    public int collectedGold = 0;

    private float m_currentOxygen;
    private bool m_goingDown = true;
    private bool m_diving = true;
    private int m_finalDeepness;
    private int m_suitLevel;

    private float m_deepness;

    public int finalDeepness { get => m_finalDeepness; }

    [HideInInspector] public Action<int> onGoldChange;
    [HideInInspector] public Action<float> onOxygenChange;
    [HideInInspector] public Action<int> onDeepnessChange;
    [HideInInspector] public Action onDrawn;
    [HideInInspector] public Action onEmerge;

    private void Awake()
    {
        m_currentOxygen = maxOxygen;
    }

    private void Update()
    {
        if (!m_diving)
            return;
        Move();
        m_currentOxygen -= 1 * Time.deltaTime;
        if (m_currentOxygen < 0)
            Drawn();
        ChangeOxygen();

        if (!m_goingDown && m_deepness == 0)
        {
            m_diving = false;
            onEmerge.Invoke();
        }
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
        m_finalDeepness = (int)Math.Ceiling(m_deepness);
        m_goingDown = false;
    }

    internal void BoostAscent(int bonus)
    {
        m_deepness -= bonus;
    }

    //TODO : Relier au reste des scripts
    public void StartDiving()
    {
        m_diving = true;
    }

    internal void DiveAgain()
    {
        m_currentOxygen = maxOxygen;
        collectedGold = 0;
        onGoldChange.Invoke(collectedGold);
        onOxygenChange.Invoke(m_currentOxygen);
        m_goingDown = true;
        m_diving = true;
    }

    public void UpgradeSuit()
    {
        ++m_suitLevel;
    }

    public void ChangeOxygenBottles(float addedOxygen)
    {
        maxOxygen += addedOxygen;
    }
}
