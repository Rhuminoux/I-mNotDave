using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteReplacer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> fromSprites;
    [SerializeField] private List<Sprite> toSprites;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetToSprites(List<Sprite> value){
        toSprites = value;
    }

    private void LateUpdate() {
        for (int i = 0; i < fromSprites.Count; i++){
            if (spriteRenderer.sprite == fromSprites[i]){
                spriteRenderer.sprite = toSprites[i];
                return;
            }
        }
    }
}
