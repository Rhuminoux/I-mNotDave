using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private DivingGameManager m_gameManager;

    [SerializeField] private TextMeshProUGUI m_availableMoney;


    [SerializeField] private Button m_improveSuitButton;
    [SerializeField] private Button m_improveBottlesButton;
    [SerializeField] private Button m_buyButton;

    [Header("Description")]
    [SerializeField] private Image m_picture;
    [SerializeField] private TextMeshProUGUI m_ItemName;
    [SerializeField] private TextMeshProUGUI m_ItemDescription;
    [SerializeField] private TextMeshProUGUI m_ItemCost;

    private void Start()
    {
        SetUI(null);
        m_buyButton.interactable = false;
    }

    public void SetSuit(ShopArticleSO article)
    {
        m_buyButton.onClick.RemoveAllListeners();
        SetUI(article);
        m_buyButton.onClick.AddListener(() => { m_gameManager.UpgradeSuit(article.price); });
    }

    public void SetBottles(ShopArticleSO article)
    {
        m_buyButton.onClick.RemoveAllListeners();
        SetUI(article);
        m_buyButton.onClick.AddListener(() => { m_gameManager.UpgradeOxygenBottles(article.price); });
    }

    private void SetUI(ShopArticleSO article)
    {
        m_buyButton.interactable = true;
        if (article == null)
        {
            m_picture.sprite = null;
            m_ItemName.text = "";
            m_ItemCost.text = "";
            m_ItemDescription.text = "";
        }
        else
        {
            m_picture.sprite = article.image;
            m_ItemName.text = article.name;
            m_ItemCost.text = article.price.ToString();
            m_ItemDescription.text = article.description;
        }
    }

    public void SetAvailableMoney(int availableMoney)
    {
        m_availableMoney.text = availableMoney.ToString();
    }

}
