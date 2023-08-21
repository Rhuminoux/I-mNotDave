using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAnchor : MonoBehaviour
{
    [Tooltip("Target anchor for the given camera")]
    [SerializeField] private Transform target;

    [Tooltip("Used to lerp the position of the camera to the anchor (0 for instant)")]
    [SerializeField] private float smoothSpeed = 0.125f;

    [Tooltip("Constant offset of the anchor. Don't forget to set the Z coordinate to -10")]
    [SerializeField] private Vector3 constantOffset;

    [Tooltip("Apply a multiplier on temporary offsets. Temporary offset can be set through code.")]
    [SerializeField] private Vector2 offsetMultiplier;

    private Vector3 temporaryOffset;

    private void Start() {
        temporaryOffset = new Vector3(0f, 0f, 0f);
    }

    // --------------- Getters/Setters -------------------

    public void SetTarget(Transform value){
        target = value;
    }

    public Transform GetTarget(){
        return target;
    }

    public void SetTemporaryOffset(Vector2 value){
        temporaryOffset = value;
    }

    public Vector2 GetTemporaryOffset(){
        return temporaryOffset;
    }

    // --------------- Utils -------------------

    IEnumerator DelayedShake(float strenght, float delay){
        yield return new WaitForSeconds(delay);
        Vector3 direction = -new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f) , 0f).normalized;
        transform.position = transform.position + (direction * strenght);
    }

    public void Shake(float strenght, int repeat, float delays){
        float delay = 0f;
        while(repeat > 0){
            repeat--;
            StartCoroutine(DelayedShake(strenght, delay));
            delay += delays;
        }
    }

    private Vector3 ComputeDesiredPosition(){
        Vector3 AddOffset = new Vector3(temporaryOffset.x * offsetMultiplier.x, temporaryOffset.y * offsetMultiplier.y, 0f);

        return target.position + constantOffset + AddOffset;
    }

    // --------------- Update -------------------

    void FixedUpdate()
    {
        if (target != null){
            transform.position = Vector3.Lerp(transform.position, ComputeDesiredPosition(), smoothSpeed);
        }
    }
}
