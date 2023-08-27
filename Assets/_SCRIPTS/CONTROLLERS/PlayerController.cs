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
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (Singleton != null)
            Destroy(this);
        Singleton = this;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_diveStats = GetComponent<DiveStats>();
        animator = GetComponent<Animator>();
    }

    private RaycastHit2D _hit;
    // Update is called once per frame
    void Update()
    {
        m_xAxis = Input.GetAxis("Horizontal");
        m_yAxis = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Escape))
            onPressEscape.Invoke();
        if (!m_isGoingUp && Input.GetKeyDown(KeyCode.Space))
        {
            _hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, LayerMask.GetMask("Default"));

            if (_hit.collider == null)
                onPressSpace.Invoke();
        }
        if (m_ascentBoost > 1)
        {
            m_ascentBoost -= m_ascentBoost * Time.deltaTime;
            if (m_ascentBoost < 1)
                m_ascentBoost = 1;
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
            SetSpriteFlip();
        }
        else
        {
            m_rigidbody2D.velocityY = 1 * speed * m_ascentBoost;
        }
    }

    private void SetSpriteFlip()
    {
        animator.SetBool("Turning", m_xAxis != 0);
        Vector3 newScale = new Vector3(1, 1, 1);
        if (m_xAxis < 0)
        {
            newScale.x = -1;
        }
        if (m_yAxis > 0)
        {
            newScale.y = -1;
        }
        transform.localScale = newScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Collectible"))
        {
            m_diveStats.AddGold(collision.gameObject.GetComponent<TreasureController>().value);
            collision.gameObject.GetComponent<TreasureController>().DestroyTreasure();
        }
        else if (1 << collision.gameObject.layer == LayerMask.GetMask("Enemy"))
        {
            m_diveStats.RemoveOxygen(6);
            animator.SetTrigger("Hurt");
        }
    }

    /// OnTriggerExit2D
    /// 
    /// Je change l'area dans le OnTrigger exit pour eviter que le joueur ne se retourne alors qu'il est toujours dans le trigger
    /// Sinon il pourrait juste d�clencher le trigger de la zonne, se retourner et continuer � jouer dans la zonne normale avec un autre setting
    /// 
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("Entrance"))
        {
            m_diveStats.ChangeCurrentArea(collision.GetComponent<AreaEntrance>().areType);
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

        m_isGoingUp = false;
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = Vector3.down;
    }

    public void GoingUp()
    {
        m_rigidbody2D.velocityX = 0;
        m_isGoingUp = true;
        FlipPlayerUp();
    }
}
