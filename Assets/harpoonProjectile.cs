using UnityEngine;

public class harpoonProjectile : MonoBehaviour
{
    
    private Camera mainCamera;

    private void Awake()
    {
        
    }

    private void Update()
    {
        Vector2 avancer = new Vector2(0, -1);
        transform.Translate(avancer * Time.deltaTime);
    }
}
