using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private EscapeMenu m_escapeMenu;
    [SerializeField] private GameObject m_drawnUI;

    [Header("In Water Elements")]
    [SerializeField] private TextMeshProUGUI m_goldText;
    [SerializeField] private TextMeshProUGUI m_deepnessText;
    [SerializeField] private Slider m_oxygenUI;
    [SerializeField] private GameObject m_ascentWheel;

    [Header("Out of Water Elements")]
    [SerializeField] private GameObject m_resume;
    [SerializeField] private GameObject m_shop;

    public void EscapeKeyPressed()
    {
        m_escapeMenu.gameObject.SetActive(!m_escapeMenu.gameObject.activeSelf);
    }

    internal void GoldChange(int currentGold)
    {
        m_goldText.text = currentGold.ToString();
    }

    internal void DeepnessChange(int currentDeepness)
    {
        m_deepnessText.text = currentDeepness.ToString() + " m";
    }

    internal void OxygenChange(float currentOxygen)
    {
        m_oxygenUI.value = currentOxygen;
    }

    internal void ActivateDrawnScreen()
    {
        m_drawnUI.SetActive(true);
    }

    internal void SetAscentWheelActive()
    {
        m_ascentWheel.SetActive(true);
    }
}
