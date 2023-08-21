using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnTick : MonoBehaviour
{
    private Ticker _ticker;
    [SerializeField] private Vector2 _sizeChange;

    private void Start() {
        _ticker = Ticker.Instance;
    }

    private void Update() {
        float progress = _ticker.GetProgress();
        transform.localScale = new Vector3(1f + (_sizeChange.x * progress), 1f + (_sizeChange.y * progress), 1f);
    }
}
