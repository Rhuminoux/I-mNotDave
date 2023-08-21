using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    public static Ticker Instance;
    [SerializeField] private float _tickDuration = 0.5f;
    private bool _growing = true;

    private float _cooldown = 0f;
    private bool _computed = false;
    private float _progress = 0f;

    private void Awake() {
        if (Instance != null)
            Destroy(this);
        Instance = this;
    }

    public float GetProgress(){
        if (_computed)
            return _progress;

        if (_growing)
            _progress = _cooldown / _tickDuration;
        else
            _progress = _tickDuration - (_cooldown / _tickDuration);

        _computed = true;
        return _progress;
    }

    private void FixedUpdate() {
        _cooldown = Mathf.Min(_cooldown + Time.deltaTime, _tickDuration);
        if (_cooldown == _tickDuration){
            _cooldown = 0f;
            _growing = !_growing;
        }
        _computed = false;
    }
}
