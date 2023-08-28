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
        m_diveStats.onChangeArea += OnChangeArea;

        m_diveStats.SetNewSuit(0);
        Dive();
        
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
        PauseGame(true);
    }

    private void OnDeepnessChange(int deepness)
    {
        m_gameUI.DeepnessChange(deepness);
    }

    private void OnEmerge()
    {
        AudioManager.Instance.Play("Emerge");
        PauseGame(true);
        m_playerStats.AddMoneyToChess(m_diveStats.collectedGold + m_diveStats.fishGold);
        m_gameUI.Emerge(m_diveStats, m_playerStats);
    }

    public void Dive()
    {
        AudioManager.Instance.Play("DiveAgain");
        PauseGame(false);
        m_gameUI.DiveAgain();
        m_diveStats.DiveAgain();
        m_playerController.DiveAgain();
        m_spawnerManagers.SetSpawnersActive(true);
    }

    public void UpgradeSuit(int price)
    {

        if (price < m_playerStats.chestMoney)
        {
            if (m_diveStats.UpgradeSuit())
            {
                m_playerController.speed += 0.5f;
                m_playerStats.chestMoney -= price;
                m_gameUI.ChestGoldChange(m_playerStats.chestMoney);
            }
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

    private void OnChangeArea(AreaEntrance.AREATYPE areaType)
    {
        m_spawnerManagers.SetNewArea((int)areaType);
        _changingColor = false;
        switch (areaType) {
            case AreaEntrance.AREATYPE.SURFACE:
                StartCoroutine(ChangeBackgroundColor(Camera.main.backgroundColor, (Color)new Color32(80, 185 ,235, 0), 2, 1));
                break;
            case AreaEntrance.AREATYPE.CAVE:
                StartCoroutine(ChangeBackgroundColor(Camera.main.backgroundColor, (Color)new Color32(35, 75, 94, 0), 2, 1));
                break;
        }
    }

    private bool _changingColor = false;
    IEnumerator ChangeBackgroundColor(Color fromColor, Color toColor, float duration, int durationEachPass)
    {
        if (_changingColor)
        {
            yield break;
        }
        _changingColor = true;
        for (float t = 0.0f; t < duration; t += Time.deltaTime)
        {
            Camera.main.backgroundColor = Color.Lerp(fromColor, toColor, t);

            //Wait for a frame
            yield return null;
        }
        _changingColor = false;
    }
    #endregion

    private void PauseGame(bool isPaused)
    {
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
