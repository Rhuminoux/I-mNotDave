using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DivingGameManager : MonoBehaviour
{
    [SerializeField] private PlayerController m_playerController;
    [SerializeField] private InGameUIManager m_gameUI;
    [SerializeField] private DiveStats m_diveStats;
    [SerializeField] private SpawnerManager m_spawnerManagers;
    [SerializeField] private PlayerStats m_playerStats;


    // Start is called before the first frame update
    void Start()
    {
        m_playerController.onPressEscape += OnEscapeKeyPressed;
        m_playerController.onPressSpace += OnSpaceKeyPressed;

        m_diveStats.onGoldChange += OnGoldChanged;
        m_diveStats.onOxygenChange += OnOxygenChanged;
        m_diveStats.onDrawn += OnDrawn;
        m_diveStats.onDeepnessChange += OnDeepnessChange;
        m_diveStats.onEmerge += OnEmerge;
    }

    private void StartGoingUp()
    {
        m_playerController.GoingUp();
        m_diveStats.StartGoingUp();
        m_spawnerManagers.SetSpawnersActive(false);
        m_gameUI.SetAscentWheelActive();
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
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
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

    private void OnEmerge()
    {
        Time.timeScale = 0;
        m_playerStats.AddMoneyToChess(m_diveStats.collectedGold);
        m_gameUI.Emerge(m_diveStats, m_playerStats);
    }

    public void Dive()
    {
        Time.timeScale = 1;
        m_gameUI.DiveAgain();
        m_diveStats.DiveAgain();
        m_playerController.DiveAgain();
        m_spawnerManagers.SetSpawnersActive(true);
    }

    public void UpgradeSuit(int price)
    {
        if (price < m_playerStats.chestMoney)
        {
            m_playerStats.chestMoney -= price;
            m_gameUI.ChestGoldChange(m_playerStats.chestMoney);
            m_diveStats.UpgradeSuit();
        }
    }

    public void UpgradeOxygenBottles(int price)
    {
        if (price < m_playerStats.chestMoney)
        {
            m_playerStats.chestMoney -= price;
            m_gameUI.ChestGoldChange(m_playerStats.chestMoney);
            m_diveStats.ChangeOxygenBottles(30);
        }
    }
    #endregion
}
