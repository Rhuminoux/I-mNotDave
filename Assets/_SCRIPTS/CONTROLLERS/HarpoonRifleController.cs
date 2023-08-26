using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonRifleController : MonoBehaviour
{
    [SerializeField] private HarpoonController m_harpoonPrefab;
    [SerializeField] private Transform m_cursor;

    private bool canShoot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(m_harpoonPrefab);
        }   
    }
}
