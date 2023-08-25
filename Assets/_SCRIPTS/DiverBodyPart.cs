using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverBodyPart : MonoBehaviour
{
    [SerializeField] private List<Sprite> m_referenceSprites;
    [SerializeField] private List<Sprite> m_currentSprites;

    [SerializeField] private SpriteRenderer m_spriteRrenderer;
    // Start is called before the first frame update
    void Awake()
    {
        m_spriteRrenderer = GetComponent<SpriteRenderer>();
    }

    public void LoadCurrentSprite(List<Sprite> newSprites)
    {
        m_currentSprites = newSprites;
    }

    private void LateUpdate()
    {
        for (int i = 0; i < m_referenceSprites.Count; i++)
        {
            if (m_spriteRrenderer.sprite == m_referenceSprites[i])
            {
                m_spriteRrenderer.sprite = m_currentSprites[i];
                return;
            }
        }
    }
}
