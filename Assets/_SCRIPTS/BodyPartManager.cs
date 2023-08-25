using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    [SerializeField] private DiverBodyPart m_legs;
    [SerializeField] private DiverBodyPart m_arm;
    [SerializeField] private DiverBodyPart m_mouth;
    //[SerializeField] private DiverBodyPart m_light;
    [SerializeField] private DiverBodyPart m_lamp;
    [SerializeField] private DiverBodyPart m_head;
    [SerializeField] private DiverBodyPart m_back;
    [SerializeField] private DiverBodyPart m_trunk;

    [SerializeField] private List<SwimsuitSO> m_swimsuitSOs;
    
    public void SetNewSuit(int suitLevel)
    {
        LoadNewSuit(m_swimsuitSOs[suitLevel]);
    }
    
    private void LoadNewSuit(SwimsuitSO newSuit)
    {
        m_legs.LoadCurrentSprite(newSuit.legSprites);
        m_arm.LoadCurrentSprite(newSuit.armSprites);
        m_mouth.LoadCurrentSprite(newSuit.mouthSprites);
        //m_light.LoadCurrentSprite(newSuit.lightSprites);
        m_lamp.LoadCurrentSprite(newSuit.lampSprites);
        m_head.LoadCurrentSprite(newSuit.headSprites);
        m_back.LoadCurrentSprite(newSuit.backSprites);
        m_trunk.LoadCurrentSprite(newSuit.trunkSprites);
    }
}
