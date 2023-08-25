using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Swimsuit", menuName = "ScriptableObjects/Swimsuit", order = 1)]
public class SwimsuitSO : ScriptableObject
{
    public int SwimsuitLevel;

    public List<Sprite> legSprites;
    public List<Sprite> armSprites;
    public List<Sprite> mouthSprites;
    public List<Sprite> lightSprites;
    public List<Sprite> lampSprites;
    public List<Sprite> headSprites;
    public List<Sprite> backSprites;
    public List<Sprite> trunkSprites;
}
