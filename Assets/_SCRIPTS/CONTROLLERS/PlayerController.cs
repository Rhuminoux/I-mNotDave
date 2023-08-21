using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;

    public float speed = 1; //can be changed by an upgrade

    [SerializeField] private DiveStats diveStats;

    [HideInInspector] public Action onPressEscape;

    private float m_xAxis, m_yAxis;
    private Rigidbody2D m_rigidbody2D;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(this);
        Singleton = this;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        diveStats = GetComponent<DiveStats>();
    }
    // Update is called once per frame
    void Update()
    {
        m_xAxis = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Escape))
            onPressEscape.Invoke();
        m_rigidbody2D.velocityX = m_xAxis * speed;
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Collectible"))
        {
            Destroy(collision.gameObject);
            diveStats.AddGold(collision.gameObject.GetComponent<TreasureController>().value);
        }
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            diveStats.RemoveOxygen(6);
        }
    }
}
