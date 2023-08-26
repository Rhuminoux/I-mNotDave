using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectibleFishObject", menuName = "ScriptableObjects/CollectibleFishObject", order = 1)]
public class CollectibleFishSO : ScriptableObject
{
    public List<Sprite> animationSprites;
    public Sprite deathSprite;
    public int lvl;
}
