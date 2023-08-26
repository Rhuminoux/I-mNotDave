using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiveResumeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_deepnessText;
    [SerializeField] private TextMeshProUGUI m_treasureMoneyText;
    [SerializeField] private TextMeshProUGUI m_fishMoneyText;

    public void SetDiveStats(DiveStats stats)
    {
        m_deepnessText.text = "Deepness : " + stats.finalDeepness.ToString() + " m";
        m_treasureMoneyText.text = "Treasure Collected : " + stats.collectedGold.ToString();
        m_fishMoneyText.text = "Fishes Collected : " + stats.fishGold.ToString();
    }
}
