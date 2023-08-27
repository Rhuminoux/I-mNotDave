using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonRifleController : MonoBehaviour
{
    [SerializeField] private HarpoonController m_harpoonPrefab;
    [SerializeField] private Transform m_cursor;
    [SerializeField] private Sprite[] m_sprites = new Sprite[2];

    private LineRenderer m_lineRenderer;

    private SpriteRenderer m_spriteRenderer;
    private bool m_canShoot = true;

    public Action<CollectibleFishController> onFishCatched;

    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_sprites[1];
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private Vector3 _mousePosition2d;
    private HarpoonController m_currentHarpoon;
    // Update is called once per frame
    void Update()
    {
        if (m_canShoot)
        {
            _mousePosition2d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePosition2d.z = 0;
            m_cursor.transform.position = _mousePosition2d;
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.Play("HarpoonShoot");
                 m_canShoot = false;
                m_spriteRenderer.sprite = m_sprites[0];
                m_cursor.GetComponent<SpriteRenderer>().enabled = false;

                Vector2 direction = (m_cursor.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90;
                m_currentHarpoon = Instantiate(m_harpoonPrefab, transform.position, Quaternion.Euler(0, 0, angle));
                m_currentHarpoon.rifleOrigin = transform;
                m_lineRenderer.enabled = true;
            }
        }
        else
        {

            m_lineRenderer.SetPosition(0, transform.position);
            m_lineRenderer.SetPosition(1, m_currentHarpoon.transform.position);
        }
    }

    public void ReloadHarpoon(CollectibleFishController fishCatched)
    {
        m_currentHarpoon = null;
        m_lineRenderer.enabled = false;
        if (fishCatched != null)
            onFishCatched.Invoke(fishCatched);
        m_canShoot = true;
        m_spriteRenderer.sprite = m_sprites[1];
        m_cursor.GetComponent<SpriteRenderer>().enabled = true;
    }
}
