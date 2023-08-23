using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    
    public LayerMask layersToHit;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition3D.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition3D;

        Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero,0.8f, layersToHit);
        
        if (hit.collider != null)
        {
            animator.SetBool("cursorOnCollider", true);
        }
        else
        {
            animator.SetBool("cursorOnCollider", false);
        }
    }

}
