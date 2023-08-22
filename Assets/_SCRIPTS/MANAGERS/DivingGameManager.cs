using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DivingGameManager : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private InGameUIManager m_gameUI;
    [SerializeField]private DiveStats m_playerStats;
    [SerializeField] private SpawnerManager m_spawnerManagers;


    // Start is called before the first frame update
    void Start()
    {
        m_playerController.onPressEscape += OnEscapeKeyPressed;
        m_playerController.onPressSpace += OnSpaceKeyPressed;

        m_playerStats.onGoldChange += OnGoldChanged;
        m_playerStats.onOxygenChange += OnOxygenChanged;
        m_playerStats.onDrawn += OnDrawn;
        m_playerStats.onDeepnessChange += OnDeepnessChange;
    }

    private void StartGoingUp()
    {
        m_playerStats.StartGoingUp();
        m_playerController.FlipPlayerUp();
        m_spawnerManagers.SetSpawnersActive(false);
        //TODO stop spawners
    }

    #region EVENTS
    private void OnSpaceKeyPressed()
    {
        StartGoingUp();
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

    private void OnDrawn()
    {
        m_gameUI.ActivateDrawnScreen();
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    private void OnDeepnessChange(int deepness)
    {
        m_gameUI.DeepnessChange(deepness);
    }
    #endregion
}
