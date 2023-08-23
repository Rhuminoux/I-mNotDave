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

    private bool m_isGoingUp = false;
    private float m_ascentBoost = 1;
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
        m_yAxis = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Escape))
            onPressEscape.Invoke();
        if (!m_isGoingUp && Input.GetKeyDown(KeyCode.Space))
            onPressSpace.Invoke();
        if (m_ascentBoost > 1)
        {
            m_ascentBoost -= m_ascentBoost * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!m_isGoingUp)
        {
            m_rigidbody2D.velocityX = m_xAxis * speed;
            m_rigidbody2D.velocityY = m_yAxis * speed;
            if (transform.position.y >= 0)
            {
                transform.position = new Vector2(transform.position.x, 0);
            }
        }
        else
        {
            m_rigidbody2D.velocityY = 1 * speed * m_ascentBoost;
        }
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
    }

    internal void BoostAscent(int bonus)
    {
        m_ascentBoost = bonus;
    }

    public void DiveAgain()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = Vector3.down;
        speed = 5;
    }

    public void GoingUp() {
        m_isGoingUp = true;
        FlipPlayerUp();
    }
}
