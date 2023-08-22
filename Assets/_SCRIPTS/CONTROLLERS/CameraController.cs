using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;

    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 m_offset;

    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;
    // Update is called once per frame
    void LateUpdate()
    {
        /*float rounded_x = RoundToNearestPixel(playerPosition.position.x);
        float rounded_y = RoundToNearestPixel(playerPosition.position.y);

        Vector3 new_pos = new Vector3(rounded_x, rounded_y, -10.0f); // this is 2d, so my camera is that far from the screen.
        */
        _desiredPosition = playerPosition.position + m_offset;
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, smoothSpeed);
        transform.position = _smoothedPosition;
    }
}
