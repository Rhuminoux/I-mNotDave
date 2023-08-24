using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFishController : MovingObject
{
    private Vector2 m_spawnPoint;
    private Vector2 m_direction;

    private void Awake()
    {

        m_spawnPoint.y = Random.Range(PlayerController.Singleton.transform.position.y - 2.5f, PlayerController.Singleton.transform.position.y + 2.5f);
        m_spawnPoint.x = Random.value >= 0.5 ? PlayerController.Singleton.transform.position.x - 8 : PlayerController.Singleton.transform.position.x + 8;

        transform.position = m_spawnPoint;

        if (m_spawnPoint.x < PlayerController.Singleton.transform.position.x)
            m_direction = Vector2.right;
        else
        {
            m_direction = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Singleton.transform.position) > 15)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        transform.position += (Vector3)(m_direction * speed * Time.fixedDeltaTime);
    }
}
