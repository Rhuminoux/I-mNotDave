using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_availableMoney;

    public void SetAvailableMoney(int availableMoney)
    {
        m_availableMoney.text = availableMoney.ToString() + " $";
    }
}
