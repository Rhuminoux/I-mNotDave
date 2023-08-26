using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonController : MonoBehaviour
{
    public float speed = 10;
    public Transform rifleOrigin;

    private Rigidbody2D m_rigidbody2D;
    private CollectibleFishController m_collectedFish;
    private bool isRetracting;
    private Vector3 m_direction;

    void Update()
    {
        if (!isRetracting)
        {
            transform.position += transform.up * speed * Time.deltaTime;
            if (Vector2.Distance(transform.position, rifleOrigin.position) > 8)
            {
                isRetracting = true;
            }
        }
        else
        {
            m_direction = (rifleOrigin.position - transform.position).normalized;
            transform.position += m_direction * speed * Time.deltaTime;
            if (Vector2.Distance(transform.position, rifleOrigin.position) < 0.2f)
            {
                rifleOrigin.GetComponent<HarpoonRifleController>().ReloadHarpoon(m_collectedFish);
                Destroy(gameObject);
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == LayerMask.GetMask("CollectibleFish"))
        {
            isRetracting = true;
            GetComponent<Collider2D>().enabled = false;
            speed = 4;
            if (collision.TryGetComponent<CollectibleFishController>(out m_collectedFish))
            {
                transform.position = m_collectedFish.transform.position;
                GetComponent<SpriteRenderer>().enabled = false;
                m_collectedFish.GetCaught(transform);
            }
        }
    }
}
