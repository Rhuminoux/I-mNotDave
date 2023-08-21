using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OrderLayerManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int offset;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        spriteRenderer.sortingOrder = -(int)(transform.position.y * 10) + offset;
    }
}
