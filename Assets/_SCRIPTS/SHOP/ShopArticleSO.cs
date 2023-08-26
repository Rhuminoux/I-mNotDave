using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemObject", menuName = "ScriptableObjects/ItemObject", order = 1)]
public class ShopArticleSO : ScriptableObject
{
    public string itemName;
    public string description;
    public int price;
    public Sprite image;
}
