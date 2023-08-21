using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DivingGameManager : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private InGameUIManager m_gameUI;
    [SerializeField]private DiveStats m_gameStats;


    // Start is called before the first frame update
    void Start()
    {
        m_playerController.onPressEscape += OnEscapeKeyPressed;
        m_gameStats.onGoldChange += OnGoldChanged;
        m_gameStats.onOxygenChange += OnOxygenChanged;
    }

    private void OnEscapeKeyPressed()
    {
        m_gameUI.EscapeKeyPressed();
        Time.timeScale = Time.timeScale ==  1 ? 0 : 1;
    }

    private void OnGoldChanged(int currentGold)
    {
        m_gameUI.GoldChange(currentGold);
    }

    private void OnOxygenChanged(float currentOxygen)
    {
        m_gameUI.OxygenChange(currentOxygen);
    }
}
