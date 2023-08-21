using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField] private EscapeMenu m_escapeMenu;
    [SerializeField] private TextMeshProUGUI m_goldUI;
    [SerializeField] private Slider m_oxygenUI;

    public void EscapeKeyPressed()
    {
        m_escapeMenu.gameObject.SetActive(!m_escapeMenu.gameObject.activeSelf);
    }

    internal void GoldChange(int currentGold)
    {
        m_goldUI.text = currentGold.ToString();
    }

    internal void OxygenChange(float currentOxygen)
    {
        m_oxygenUI.value = currentOxygen;
    }
}
