using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class harpoonController : MonoBehaviour
{
    public Animator animator;
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Instancier la flèche lors du clic gauche
        {
            animator.SetBool("shoot", true);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ajuster l'angle pour qu'il soit dans le bon intervalle (0 - 360 degrés)
            if (angle < 0)
            {
                angle += 360;
            }

            // Ajuster l'angle pour que le harpon pointe correctement en fonction de sa position de base
            angle += 90f;

            GameObject harpoonArrow = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, angle));
        }
       
    }
}
