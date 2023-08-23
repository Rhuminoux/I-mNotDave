using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Singleton;

    public float speed = 5; //can be changed by an upgrade

    private DiveStats m_diveStats;

    [HideInInspector] public Action onPressEscape;
    [HideInInspector] public Action onPressSpace;

    private float m_xAxis, m_yAxis;
    private Rigidbody2D m_rigidbody2D;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(this);
        Singleton = this;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_diveStats = GetComponent<DiveStats>();
    }
    // Update is called once per frame
    void Update()
    {
        m_xAxis = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Escape))
            onPressEscape.Invoke();
        if (Input.GetKeyDown(KeyCode.Space))
            onPressSpace.Invoke();
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.velocityX = m_xAxis * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Collectible"))
        {
            Destroy(collision.gameObject);
            m_diveStats.AddGold(collision.gameObject.GetComponent<TreasureController>().value);
        }
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            m_diveStats.RemoveOxygen(6);
        }
    }

    internal void FlipPlayerUp()
    {
        transform.localScale = new Vector3(1, -1, 1);
        speed = 0;
    }

    internal void BoostAscent(int bonus)
    {
        m_diveStats.BoostAscent(bonus);
    }

    public void DiveAgain()
    {
        transform.localScale = new Vector3(1, 1, 1);
        speed = 5;
    }
}
