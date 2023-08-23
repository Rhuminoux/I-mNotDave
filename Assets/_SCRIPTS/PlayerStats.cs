using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int chestMoney;
    public int deepnessRecord;

    public void AddMoneyToChess(int money)
    {
        chestMoney += money;
    }
}
