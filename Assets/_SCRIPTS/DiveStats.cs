using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static AreaEntrance;

public class DiveStats : MonoBehaviour
{
    public float maxOxygen;
    public int collectedGold = 0;
    [SerializeField] private BodyPartManager m_bodyPartManager;

    private float m_currentOxygen;
    private bool m_goingDown = true;
    private bool m_diving = true;
    private int m_finalDeepness;
    private int m_suitLevel;
    private AREATYPE m_currentArea;

    private float m_deepness;

    public int finalDeepness { get => m_finalDeepness; }

    [HideInInspector] public Action<int> onGoldChange;
    [HideInInspector] public Action<float> onOxygenChange;
    [HideInInspector] public Action<int> onDeepnessChange;
    [HideInInspector] public Action<AREATYPE> onChangeArea;
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
    }

    private int _intDeepness = 0;
    private void Move()
    {
        m_deepness = transform.position.y;

        if (!m_goingDown && m_deepness >= 0)
        {
            m_deepness = 0;
            m_diving = false;
            onEmerge.Invoke();
        }

        if (m_goingDown)
        {
            _intDeepness = (int)Math.Ceiling(m_deepness) - 1;
            onDeepnessChange.Invoke(-_intDeepness);
        }
        else
        {
            _intDeepness = (int)Math.Ceiling(m_deepness);
            onDeepnessChange.Invoke(-_intDeepness);
        }
    }

    private void Drawn()
    {
        onDrawn.Invoke();
    }

    public void AddGold(int goldToAdd)
    {
        goldToAdd *= (int)Math.Ceiling((transform.position.y * -1) / 10);
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
        m_finalDeepness = (int)Math.Ceiling(m_deepness) - 1;
        m_goingDown = false;
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

    public bool UpgradeSuit()
    {
        if (m_suitLevel < 7)
        {
            ++m_suitLevel;
            SetNewSuit(m_suitLevel);
            return true;
        }
        return false;
    }

    public void SetNewSuit(int suitLevel)
    {
        m_bodyPartManager.SetNewSuit(suitLevel);
    }

    public void ChangeOxygenBottles(float addedOxygen)
    {
        maxOxygen += addedOxygen;
    }

    public void ChangeCurrentArea(AREATYPE newArea)
    {
        if (newArea == m_currentArea) // si on est déjà dans cette zonne alors c'est qu'on la quitte
            m_currentArea = AREATYPE.SURFACE;
        else //Sinon on entre dans cette zone
            m_currentArea = newArea;
         onChangeArea.Invoke(m_currentArea);
    }
}
